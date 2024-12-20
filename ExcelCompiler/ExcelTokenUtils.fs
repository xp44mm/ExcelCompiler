﻿module ExcelCompiler.ExcelTokenUtils

open System.Text.RegularExpressions
open FslexFsyacc
open FSharp.Idioms
open FSharp.Idioms.ActivePatterns
open FSharp.Idioms.RegularExpressions

let getTag (token:PositionWith<ExcelToken>) = 
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

let getLexeme (token:PositionWith<ExcelToken>) = 
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


let tokenize (offset:int) (input:string) =
    ///isUnary=true表示当前元素的下一个元素是一元正负。
    let rec loop isUnary (pos:int) (rest:string) =
        seq {
            match rest with // input.[pos-offset..]
            | "" -> ()

            | Rgx @"^\s+" capt ->
                let pos = pos + capt.Length
                let rest = rest.[capt.Length..]
                yield! loop isUnary pos rest

            | Search(Regex(@"^false\b",RegexOptions.IgnoreCase)) x ->
                let postok = {
                    index = pos
                    length = x.Length
                    value = FALSE
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | Search(Regex(@"^true\b",RegexOptions.IgnoreCase)) x ->
                let postok = {
                    index = pos
                    length = x.Length
                    value = TRUE
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First '!' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = EXCLAM
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First ':' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = COLON
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First ',' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = COMMA
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First '(' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = LPAREN
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First ')' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = RPAREN
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First '=' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = EQ
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First '>' _ ->
                match input.[pos-offset+1..] with
                | First '=' _ ->
                    let postok = {
                        index = pos
                        length = 2
                        value = GE
                    }
                    yield postok
                    yield! loop isUnary postok.adjacent rest.[postok.length..]
                | _ ->
                    let postok = {
                        index = pos
                        length = 1
                        value = GT
                    }
                    yield postok
                    yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First '<' _ ->
                match input.[pos-offset+1..] with
                | First '=' _ ->
                    let postok = {
                        index = pos
                        length = 2
                        value = LE
                    }
                    yield postok
                    yield! loop isUnary postok.adjacent rest.[postok.length..]

                | First '>' _ ->
                    let postok = {
                        index = pos
                        length = 2
                        value = NE
                    }
                    yield postok
                    yield! loop isUnary postok.adjacent rest.[postok.length..]
                | _ ->
                    let postok = {
                        index = pos
                        length = 1
                        value = LT
                    }
                    yield postok
                    yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First '&' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = AMPERSAND
                }
                yield postok
                yield! loop isUnary postok.adjacent rest.[postok.length..]

            | First '+' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = if isUnary then POSITIVE else ADD
                }
                yield postok
                yield! loop true postok.adjacent rest.[postok.length..]

            | First '-' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = if isUnary then NEGATIVE else SUB
                }
                yield postok
                yield! loop true postok.adjacent rest.[postok.length..]

            | First '*' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = MUL
                }
                yield postok
                yield! loop true postok.adjacent rest.[postok.length..]

            | First '/' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = DIV
                }
                yield postok
                yield! loop true postok.adjacent rest.[postok.length..]

            | First '^' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = CARET
                }
                yield postok
                yield! loop true postok.adjacent rest.[postok.length..]

            | First '%' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = PERCENT
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | First '{' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = LBRACE
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | First '}' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = RBRACE
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | First '[' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = LBRACKET
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | First ']' _ ->
                let postok = {
                    index = pos
                    length = 1
                    value = RBRACKET
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | Rgx """^"([^"]|"")*"(?!")""" lexeme ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = QUOTE lexeme.Value // todo:parse
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | Rgx "^'([^']|'')*'(?!')" lexeme ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = APOSTROPHE lexeme.Value // todo:parse
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | Search(Regex(@"^#DIV/0!|#N/A\b|#NAME\?|#NULL!|#NUM!|#REF!|#VALUE!",RegexOptions.IgnoreCase)) lexeme ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = ERROR lexeme.Value // todo:parse
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            //至少有一個$的標識符
            | Search(Regex(@"^[0-9A-Z$]*\$[0-9A-Z]+",RegexOptions.IgnoreCase)) lexeme ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = DOLLAR lexeme.Value // todo:parse
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            //整数
            | Rgx @"^\d+\b(?![.])" lexeme ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = INTEGER lexeme.Value // todo:parse
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            //小數
            | Rgx @"^\d+(\.\d+)?([eE][-+]?\d+)?" (lexeme) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = NUMBER lexeme.Value // todo:parse
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            //https://support.office.com/en-us/article/names-in-formulas-fc2935f9-115d-4bef-a370-3aa8bb4c91f1?omkt=en-US&ui=en-US&rs=en-US&ad=US
            | Rgx @"^[\w\\.]+" (lexeme) ->
                let postok = {
                    index = pos
                    length = lexeme.Length
                    value = ID lexeme.Value // todo:parse
                }
                yield postok
                yield! loop false postok.adjacent rest.[postok.length..]

            | _ -> failwith $"tokenize unmatched:{rest}"
        }

    loop true offset input

let signNumber (tokens:PositionWith<ExcelToken> list) =
    {
        index = tokens.Head.index
        length = PositionWith.totalLength tokens
        value =
            match tokens |> List.map(fun pos -> pos.value) with
            | [ NEGATIVE; (NUMBER x|INTEGER x)] -> NUMBER $"-{x}"
            | [ POSITIVE; (NUMBER x|INTEGER x)] -> NUMBER $"+{x}"
            | lexbuf -> failwith $"{lexbuf}"
    }

let functionFromId (tokens:PositionWith<ExcelToken> list) =
    let postok = tokens |> List.exactlyOne
    {
        postok with
            value =
                match postok.value with
                | ID x -> FUNCTION x
                | lexbuf -> failwith $"{lexbuf}" 
    }

let numberFromInteger (tokens:PositionWith<ExcelToken> list) =
    let postok = tokens |> List.exactlyOne
    {
        postok with
            value =
                match postok.value with
                | INTEGER x -> NUMBER x
                | lexbuf -> failwith $"{lexbuf}" 
    }
    
let getReference (tokens:PositionWith<ExcelToken> list) =
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
        length = PositionWith.totalLength tokens
        value = REFERENCE range
    }
    
