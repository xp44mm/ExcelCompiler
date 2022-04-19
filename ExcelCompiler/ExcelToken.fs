namespace ExcelCompiler

open FSharp.Idioms
open System.Text.RegularExpressions

type ExcelToken =
    | NUMBER of string
    | INTEGER of string
    | QUOTE of string // "
    | APOSTROPHE of string // '
    | DOLLAR of string
    | ID of string
    | FUNCTION of string // 后面跟着参数表
    | ERROR of string
    | FALSE
    | TRUE
    | EXCLAM
    | COLON
    | COMMA
    | LPAREN
    | RPAREN
    | EQ
    | NE
    | LT
    | LE
    | GT
    | GE
    | AMPERSAND
    | ADD
    | SUB
    | MUL
    | DIV
    | CARET
    | PERCENT
    | POSITIVE
    | NEGATIVE
    | LBRACE
    | RBRACE
    | LBRACKET
    | RBRACKET

    //整理一次
    | REFERENCE of string list * string list // range or name or bool

    //member this.tag =
    //    match this with
    //    | NUMBER _ -> "NUMBER"
    //    | INTEGER _ -> "INTEGER"
    //    | QUOTE _ -> "QUOTE"
    //    | APOSTROPHE _ -> "APOSTROPHE"
    //    | DOLLAR _ -> "DOLLAR"
    //    | ID _ -> "ID"
    //    | FUNCTION _ -> "FUNCTION"
    //    | ERROR _ -> "ERROR"
    //    | FALSE -> "FALSE"
    //    | TRUE  -> "TRUE"
    //    | EXCLAM -> "!"
    //    | COLON -> ":"
    //    | COMMA -> ","
    //    | LPAREN -> "("
    //    | RPAREN -> ")"
    //    | EQ -> "="
    //    | NE -> "<>"
    //    | LT -> "<"
    //    | LE -> "<="
    //    | GT -> ">"
    //    | GE -> ">="
    //    | AMPERSAND -> "&"
    //    | ADD -> "+"
    //    | SUB -> "-"
    //    | MUL -> "*"
    //    | DIV -> "/"
    //    | CARET -> "^"
    //    | PERCENT -> "%"
    //    | POSITIVE -> "POSITIVE"
    //    | NEGATIVE -> "NEGATIVE"
    //    | LBRACE -> "{"
    //    | RBRACE -> "}"
    //    | LBRACKET -> "["
    //    | RBRACKET -> "]"
    //    /// 二次分析
    //    | REFERENCE _ -> "REFERENCE"

    //member this.lexeme =
    //    match this with
    //    | NUMBER     x
    //    | INTEGER    x
    //    | QUOTE      x
    //    | APOSTROPHE x
    //    | DOLLAR     x
    //    | ID         x
    //    | FUNCTION   x
    //    | ERROR      x -> x
    //    | POSITIVE -> "+"
    //    | NEGATIVE -> "-"

    //    /// 二次分析
    //    | REFERENCE (ws,rg) ->  failwith "(ws,rg)"
    //    | x -> x.tag

    //static member tokenize(inp:string) =
    //    ///isUnary=true表示当前元素的下一个元素是一元正负。
    //    let rec loop isUnary (inp:string) =
    //        seq {
    //            match inp with
    //            | "" -> ()

    //            | On(tryMatch(Regex @"^\s+")) (_,rest) ->
    //                yield! loop isUnary rest

    //            | On(tryMatch(Regex(@"^false\b",RegexOptions.IgnoreCase))) (_,rest) ->
    //                yield FALSE
    //                yield! loop isUnary rest

    //            | On(tryMatch(Regex(@"^true\b",RegexOptions.IgnoreCase))) (_,rest) ->
    //                yield TRUE
    //                yield! loop isUnary rest

    //            | On(tryFirst '!') rest ->
    //                yield EXCLAM
    //                yield! loop false rest

    //            | On(tryFirst ':') rest ->
    //                yield COLON
    //                yield! loop false rest

    //            | On(tryFirst ',') rest ->
    //                yield COMMA
    //                yield! loop true rest

    //            | On(tryFirst '(') rest ->
    //                yield LPAREN
    //                yield! loop true rest

    //            | On(tryFirst ')') rest ->
    //                yield RPAREN
    //                yield! loop false rest

    //            | On(tryFirst '=') rest ->
    //                yield EQ
    //                yield! loop true rest

    //            | On(tryFirst '>') rest ->
    //                match rest with
    //                | On(tryFirst '=') rest ->
    //                    yield GE
    //                    yield! loop true rest
    //                | _ ->
    //                    yield GT
    //                    yield! loop true rest

    //            | On(tryFirst '<') rest ->
    //                match rest with
    //                | On(tryFirst '=') rest ->
    //                    yield LE
    //                    yield! loop true rest
    //                | On(tryFirst '>') rest ->
    //                    yield NE
    //                    yield! loop true rest
    //                | _ ->
    //                    yield LT
    //                    yield! loop true rest

    //            | On(tryFirst '&') rest ->
    //                yield AMPERSAND
    //                yield! loop true rest

    //            | On(tryFirst '+') rest ->
    //                yield if isUnary then POSITIVE else ADD
    //                yield! loop true rest

    //            | On(tryFirst '-') rest ->
    //                yield if isUnary then NEGATIVE else SUB
    //                yield! loop true rest

    //            | On(tryFirst '*') rest ->
    //                yield MUL
    //                yield! loop true rest

    //            | On(tryFirst '/') rest ->
    //                yield DIV
    //                yield! loop true rest

    //            | On(tryFirst '^') rest ->
    //                yield CARET
    //                yield! loop true rest

    //            | On(tryFirst '%') rest ->
    //                yield PERCENT
    //                yield! loop false rest

    //            | On(tryFirst '{') rest ->
    //                yield LBRACE
    //                yield! loop false rest

    //            | On(tryFirst '}') rest ->
    //                yield RBRACE
    //                yield! loop false rest

    //            | On(tryFirst '[') rest ->
    //                yield LBRACKET
    //                yield! loop false rest

    //            | On(tryFirst ']') rest ->
    //                yield RBRACKET
    //                yield! loop false rest

    //            | On(tryMatch(Regex("""^"([^"]|"")*"(?!")"""))) (lexeme,rest) ->
    //                yield QUOTE lexeme
    //                yield! loop false rest

    //            | On(tryMatch(Regex "^'([^']|'')*'(?!')")) (lexeme,rest) ->
    //                yield APOSTROPHE lexeme
    //                yield! loop false rest

    //            | On(tryMatch(Regex @"^#DIV/0!|#N/A\b|#NAME\?|#NULL!|#NUM!|#REF!|#VALUE!")) (lexeme,rest) ->
    //                yield ERROR lexeme
    //                yield! loop false rest

    //            //至少有一個$的標識符
    //            | On(tryMatch(Regex(@"^[0-9A-Z$]*\$[0-9A-Z]+",RegexOptions.IgnoreCase))) (lexeme,rest) ->
    //                yield DOLLAR lexeme
    //                yield! loop false rest

    //            //整数
    //            | On(tryMatch(Regex @"^\d+\b(?![.])")) (lexeme,rest) ->
    //                yield INTEGER lexeme
    //                yield! loop false rest

    //            //小數
    //            | On(tryMatch(Regex @"^\d+(\.\d+)?([eE][-+]?\d+)?")) (lexeme,rest) ->
    //                yield NUMBER lexeme
    //                yield! loop false rest

    //            //https://support.office.com/en-us/article/names-in-formulas-fc2935f9-115d-4bef-a370-3aa8bb4c91f1?omkt=en-US&ui=en-US&rs=en-US&ad=US
    //            | On(tryMatch(Regex @"^[\w\\.]+")) (lexeme,rest) ->
    //                yield ID lexeme
    //                yield! loop false rest

    //            | never -> failwith never
    //        }

    //    loop true inp
