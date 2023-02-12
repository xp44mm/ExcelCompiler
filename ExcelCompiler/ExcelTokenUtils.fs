﻿module ExcelCompiler.ExcelTokenUtils

open System.Text.RegularExpressions
open FslexFsyacc.Runtime

let getTag (token:Position<ExcelToken>) = 
    match token.value with
    | NUMBER     _ -> "NUMBER"
    | INTEGER    _ -> "INTEGER"
    | QUOTE      _ -> "QUOTE"
    | APOSTROPHE _ -> "APOSTROPHE"
    | DOLLAR     _ -> "DOLLAR"
    | ID         _ -> "ID"
    | FUNCTION   _ -> "FUNCTION"
    | ERROR      _ -> "ERROR"
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
    | REFERENCE _ -> "REFERENCE"

let getLexeme (token:Position<ExcelToken>) = 
    match token.value with
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

open FSharp.Idioms

let tokenize (i:int) (inp:string) =
    ///isUnary=true表示当前元素的下一个元素是一元正负。
    let rec loop isUnary (pos:int) (rest:string) =
        seq {
            match rest with
            | "" -> ()

            | On(tryMatch(Regex @"^\s+")) (_,rest) ->
                yield! loop isUnary pos rest

            | On(tryMatch(Regex(@"^false\b",RegexOptions.IgnoreCase))) (x,rest) ->
                let postok = {
                    index = pos
                    length = x.Length
                    value = FALSE
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryMatch(Regex(@"^true\b",RegexOptions.IgnoreCase))) (x,rest) ->
                let postok = {
                    index = pos
                    length = x.Length
                    value = TRUE
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst '!') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = EXCLAM
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst ':') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = COLON
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst ',') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = COMMA
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst '(') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = LPAREN
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst ')') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = RPAREN
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst '=') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = EQ
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst '>') rest ->
                match rest with
                | On(tryFirst '=') rest ->
                    let postok = {
                        index = pos
                        length = 2
                        value = GE
                    }
                    yield postok
                    yield! loop isUnary postok.nextIndex rest
                | _ ->
                    let postok = {
                        index = pos
                        length = 1
                        value = GT
                    }
                    yield postok
                    yield! loop isUnary postok.nextIndex rest

            | On(tryFirst '<') rest ->
                match rest with
                | On(tryFirst '=') rest ->
                    let postok = {
                        index = pos
                        length = 2
                        value = LE
                    }
                    yield postok
                    yield! loop isUnary postok.nextIndex rest

                | On(tryFirst '>') rest ->
                    let postok = {
                        index = pos
                        length = 2
                        value = NE
                    }
                    yield postok
                    yield! loop isUnary postok.nextIndex rest
                | _ ->
                    let postok = {
                        index = pos
                        length = 1
                        value = LT
                    }
                    yield postok
                    yield! loop isUnary postok.nextIndex rest

            | On(tryFirst '&') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = AMPERSAND
                }
                yield postok
                yield! loop isUnary postok.nextIndex rest

            | On(tryFirst '+') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = if isUnary then POSITIVE else ADD
                }
                yield postok
                yield! loop true postok.nextIndex rest

            | On(tryFirst '-') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = if isUnary then NEGATIVE else SUB
                }
                yield postok
                yield! loop true postok.nextIndex rest

            | On(tryFirst '*') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = MUL
                }
                yield postok
                yield! loop true postok.nextIndex rest

            | On(tryFirst '/') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = DIV
                }
                yield postok
                yield! loop true postok.nextIndex rest

            | On(tryFirst '^') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = CARET
                }
                yield postok
                yield! loop true postok.nextIndex rest

            | On(tryFirst '%') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = PERCENT
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | On(tryFirst '{') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = LBRACE
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | On(tryFirst '}') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = RBRACE
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | On(tryFirst '[') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = LBRACKET
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | On(tryFirst ']') rest ->
                let postok = {
                    index = pos
                    length = 1
                    value = RBRACKET
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | On(tryMatch(Regex """^"([^"]|"")*"(?!")""")) (lexeme,rest) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = QUOTE lexeme // todo:parse
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | On(tryMatch(Regex "^'([^']|'')*'(?!')")) (lexeme,rest) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = APOSTROPHE lexeme // todo:parse
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | On(tryMatch(Regex(@"^#DIV/0!|#N/A\b|#NAME\?|#NULL!|#NUM!|#REF!|#VALUE!",RegexOptions.IgnoreCase))) (lexeme,rest) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = ERROR lexeme // todo:parse
                }
                yield postok
                yield! loop false postok.nextIndex rest

            //至少有一個$的標識符
            | On(tryMatch(Regex(@"^[0-9A-Z$]*\$[0-9A-Z]+",RegexOptions.IgnoreCase))) (lexeme,rest) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = DOLLAR lexeme // todo:parse
                }
                yield postok
                yield! loop false postok.nextIndex rest

            //整数
            | On(tryMatch(Regex @"^\d+\b(?![.])")) (lexeme,rest) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = INTEGER lexeme // todo:parse
                }
                yield postok
                yield! loop false postok.nextIndex rest

            //小數
            | On(tryMatch(Regex @"^\d+(\.\d+)?([eE][-+]?\d+)?")) (lexeme,rest) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = NUMBER lexeme // todo:parse
                }
                yield postok
                yield! loop false postok.nextIndex rest

            //https://support.office.com/en-us/article/names-in-formulas-fc2935f9-115d-4bef-a370-3aa8bb4c91f1?omkt=en-US&ui=en-US&rs=en-US&ad=US
            | On(tryMatch(Regex @"^[\w\\.]+")) (lexeme,rest) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = ID lexeme // todo:parse
                }
                yield postok
                yield! loop false postok.nextIndex rest

            | _ -> failwith $"tokenize unmatched:{rest}"
        }

    loop true i inp

let signNumber (tokens:Position<ExcelToken> list) =
    {
        index = tokens.Head.index
        length = Position.totalLength tokens
        value =
            match tokens |> List.map(fun pos -> pos.value) with
            | [ NEGATIVE; (NUMBER x|INTEGER x)] -> NUMBER $"-{x}"
            | [ POSITIVE; (NUMBER x|INTEGER x)] -> NUMBER $"+{x}"
            | lexbuf -> failwith $"{lexbuf}"
    }

let functionFromId (tokens:Position<ExcelToken> list) =
    let postok = tokens |> List.exactlyOne
    {
        postok with
            value =
                match postok.value with
                | ID x -> FUNCTION x
                | lexbuf -> failwith $"{lexbuf}" 
    }

let numberFromInteger (tokens:Position<ExcelToken> list) =
    let postok = tokens |> List.exactlyOne
    {
        postok with
            value =
                match postok.value with
                | INTEGER x -> NUMBER x
                | lexbuf -> failwith $"{lexbuf}" 
    }
    
let getReference (tokens:Position<ExcelToken> list) =
    let range =
        match tokens |> List.map(fun pos -> pos.value) with
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
        | lexbuf -> failwith $"{lexbuf}"
    {
        index = tokens.Head.index
        length = Position.totalLength tokens
        value = REFERENCE range
    }
    
