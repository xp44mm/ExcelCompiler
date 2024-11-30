module ExcelCompiler.ExcelExprCompiler

open FslexFsyacc
open FSharp.Idioms.Literal
open System
open ExcelCompiler.ExcelParsingTable

let analyze tokens = ExcelDFA.analyzer.analyze(tokens,ExcelTokenUtils.getTag)

let parser = 
    app.getParser<PositionWith<ExcelToken>>(
        //ExcelParsingTable.rules,
        //ExcelParsingTable.actions,
        //ExcelParsingTable.closures,
        
        ExcelTokenUtils.getTag,
        ExcelTokenUtils.getLexeme)

let table = app.getTable parser

let parseTokens(tokens:seq<PositionWith<ExcelToken>>) =
    tokens
    |> parser.parse
    |> ExcelParsingTable.unboxRoot


///解析公式
let compile (txt:string) =
    let mutable tokens = []
    let mutable states = [0,null]

    txt
    |> ExcelTokenUtils.tokenize 0
    |> analyze
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

