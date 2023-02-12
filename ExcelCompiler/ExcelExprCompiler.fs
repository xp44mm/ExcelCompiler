module ExcelCompiler.ExcelExprCompiler

open FslexFsyacc.Runtime
open FSharp.Literals.Literal
open System

let parser = 
    Parser<Position<ExcelToken>>(
        ExcelParsingTable.rules,
        ExcelParsingTable.actions,
        ExcelParsingTable.closures,
        
        ExcelTokenUtils.getTag,
        ExcelTokenUtils.getLexeme)

let parseTokens(tokens:seq<Position<ExcelToken>>) =
    tokens
    |> parser.parse
    |> ExcelParsingTable.unboxRoot

//[<Obsolete("compile")>]
//let parse (formula:string) =
//    formula
//    |> ExcelTokenUtils.tokenize 0
//    |> ExcelDFA.analyze
//    |> parseTokens

///解析公式
let compile (txt:string) =
    let mutable tokens = []
    let mutable states = [0,null]
    //let mutable result = defaultValue<_>

    txt
    |> ExcelTokenUtils.tokenize 0
    |> ExcelDFA.analyze
    |> Seq.map(fun tok ->
        tokens <- tok::tokens
        tok
    )
    |> Seq.iter(fun lookahead ->
        match parser.tryReduce(states,lookahead) with
        | Some x -> states <- x
        | None -> ()

        states <- parser.shift(states,lookahead)
    )

    match parser.tryReduce(states) with
    | Some x -> states <- x
    | None -> ()

    match states with
    |[1,lxm; 0,null] ->
        ExcelParsingTable.unboxRoot lxm
    | _ ->
        failwith $"{stringify states}"

