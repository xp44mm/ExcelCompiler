﻿module ExcelCompiler.ExcelTokenUtils
open FSharp.Idioms

let getTag = function
    | NUMBER                _ -> "NUMBER"
    | INTEGER               _ -> "INTEGER"
    | QUOTE                 _ -> "QUOTE"
    | APOSTROPHE            _ -> "APOSTROPHE"
    | DOLLAR                _ -> "DOLLAR"
    | ID                    _ -> "ID"
    | FUNCTION              _ -> "FUNCTION"
    | ERROR                 _ -> "ERROR"
    | FALSE -> "FALSE"
    | TRUE  -> "TRUE"
    | EXCLAM -> "!"
    | COLON -> ":"
    | COMMA -> ","
    | LPAREN -> "("
    | RPAREN -> ")"
    | EQ -> "="
    | NE -> "<>"
    | LT -> "<"
    | LE -> "<="
    | GT -> ">"
    | GE -> ">="
    | AMPERSAND -> "&"
    | ADD -> "+"
    | SUB -> "-"
    | MUL -> "*"
    | DIV -> "/"
    | CARET -> "^"
    | PERCENT -> "%"
    | POSITIVE -> "POSITIVE"
    | NEGATIVE -> "NEGATIVE"
    | LBRACE -> "{"
    | RBRACE -> "}"
    | LBRACKET -> "["
    | RBRACKET -> "]"
    /// 二次分析
    | REFERENCE             _ -> "REFERENCE"

let getLexeme = function
    | REFERENCE (x,y) -> box (x,y)
    | NUMBER     x -> box x
    | INTEGER    x -> box x
    | QUOTE      x -> box x
    | APOSTROPHE x -> box x
    | DOLLAR     x -> box x
    | ID         x -> box x
    | FUNCTION   x -> box x
    | ERROR      x -> box x
    | _ -> null

let tokenize(inp:string) =
    ///isUnary=true表示当前元素的下一个元素是一元正负。
    let rec loop isUnary (inp:string) =
        seq {
            match inp with
            | "" -> ()

            | Prefix @"\s+" (_,rest) ->
                yield! loop isUnary rest

            | Prefix @"(?i)false\b" (_,rest) ->
                yield FALSE
                yield! loop isUnary rest

            | Prefix @"(?i)true\b" (_,rest) ->
                yield TRUE
                yield! loop isUnary rest

            | PrefixChar '!' rest ->
                yield EXCLAM
                yield! loop false rest

            | PrefixChar ':' rest ->
                yield COLON
                yield! loop false rest

            | PrefixChar ',' rest ->
                yield COMMA
                yield! loop true rest

            | PrefixChar '(' rest ->
                yield LPAREN
                yield! loop true rest

            | PrefixChar ')' rest ->
                yield RPAREN
                yield! loop false rest

            | PrefixChar '=' rest ->
                yield EQ
                yield! loop true rest

            | PrefixChar '>' rest ->
                match rest with
                | PrefixChar '=' rest ->
                    yield GE
                    yield! loop true rest
                | _ ->
                    yield GT
                    yield! loop true rest

            | PrefixChar '<' rest ->
                match rest with
                | PrefixChar '=' rest ->
                    yield LE
                    yield! loop true rest
                | PrefixChar '>' rest ->
                    yield NE
                    yield! loop true rest
                | _ ->
                    yield LT
                    yield! loop true rest

            | PrefixChar '&' rest ->
                yield AMPERSAND
                yield! loop true rest

            | PrefixChar '+' rest ->
                yield if isUnary then POSITIVE else ADD
                yield! loop true rest

            | PrefixChar '-' rest ->
                yield if isUnary then NEGATIVE else SUB
                yield! loop true rest

            | PrefixChar '*' rest ->
                yield MUL
                yield! loop true rest

            | PrefixChar '/' rest ->
                yield DIV
                yield! loop true rest

            | PrefixChar '^' rest ->
                yield CARET
                yield! loop true rest

            | PrefixChar '%' rest ->
                yield PERCENT
                yield! loop false rest

            | PrefixChar '{' rest ->
                yield LBRACE
                yield! loop false rest

            | PrefixChar '}' rest ->
                yield RBRACE
                yield! loop false rest

            | PrefixChar '[' rest ->
                yield LBRACKET
                yield! loop false rest

            | PrefixChar ']' rest ->
                yield RBRACKET
                yield! loop false rest

            | Prefix """(?:"([^"]|"")*"(?!"))""" (lexeme,rest) ->
                yield QUOTE lexeme
                yield! loop false rest

            | Prefix "'([^']|'')*'(?!')" (lexeme,rest) ->
                yield APOSTROPHE lexeme
                yield! loop false rest

            | Prefix @"#DIV/0!|#N/A\b|#NAME\?|#NULL!|#NUM!|#REF!|#VALUE!" (lexeme,rest) ->
                yield ERROR lexeme
                yield! loop false rest

            //至少有一個$的標識符
            | Prefix @"(?i)[0-9A-Z$]*\$[0-9A-Z]+" (lexeme,rest) ->
                yield DOLLAR lexeme
                yield! loop false rest

            //整数
            | Prefix @"\d+\b(?![.])" (lexeme,rest) ->
                yield INTEGER lexeme
                yield! loop false rest

            //小數
            | Prefix @"\d+(\.\d+)?([eE][-+]?\d+)?" (lexeme,rest) ->
                yield NUMBER lexeme
                yield! loop false rest

            //https://support.office.com/en-us/article/names-in-formulas-fc2935f9-115d-4bef-a370-3aa8bb4c91f1?omkt=en-US&ui=en-US&rs=en-US&ad=US
            | Prefix @"[\w\\.]+" (lexeme,rest) ->
                yield ID lexeme
                yield! loop false rest

            | never -> failwith never
        }

    loop true inp

let signNumber = function
    | [ NEGATIVE; NUMBER x] -> [NUMBER $"-{x}"]
    | [ NEGATIVE; INTEGER x] -> [NUMBER $"-{x}"]
    | [ POSITIVE; NUMBER x] -> [NUMBER $"+{x}"]
    | [ POSITIVE; INTEGER x] -> [NUMBER $"+{x}"]
    | lexbuf -> failwithf "%A" lexbuf

let functionFromId = function
    | ID x -> FUNCTION x
    | tok -> failwithf "%A" tok

let numberFromInteger = function
    | INTEGER x -> NUMBER x
    | tok -> failwithf "%A" tok

let appendNA = function
    | [tok] -> [tok;FUNCTION "NA";LPAREN;RPAREN]
    | lexbuf -> failwithf "%A" lexbuf

let getRange lexbuf = 
    match lexbuf with
    | [DOLLAR x | ID x ] -> ([],[x])
    | [(DOLLAR x | ID x | INTEGER x);COLON;(DOLLAR y | ID y | INTEGER y)] -> ([],[x;y])
    | [(ID p | APOSTROPHE p);EXCLAM;(DOLLAR x | ID x)] -> 
        ([p],[x])
    | [(ID p | APOSTROPHE p);EXCLAM;(DOLLAR x | ID x | INTEGER x);COLON;(DOLLAR y | ID y | INTEGER y)] -> 
        ([p],[x;y])
    | [(ID p | APOSTROPHE p);COLON;(ID q | APOSTROPHE q);EXCLAM;(DOLLAR x | ID x)] -> 
        ([p;q],[x])
    | [(ID p | APOSTROPHE p);COLON;(ID q | APOSTROPHE q);EXCLAM;(DOLLAR x | ID x | INTEGER x);COLON;(DOLLAR y | ID y | INTEGER y)] -> 
        ([p;q],[x;y])
    | lexbuf -> failwithf "%A" lexbuf
