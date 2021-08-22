namespace ExcelCompiler

open System
open System.IO

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit
open FSharpCompiler.Lex

type ExcelDfaTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    let locatePath = 
        Path.Combine(
            DirectoryInfo(__SOURCE_DIRECTORY__).Parent.FullName, 
            @"ExcelCompiler"
            )
    let filePath = Path.Combine(locatePath, "excel.lex")
    let text = File.ReadAllText(filePath)
    let dfa = Lex.generateDFA text

    [<Fact(Skip="once and for all")>] // 
    member this.``generate DFA``() =
        let result =
            [
                "module ExcelCompiler.ExcelDFA"
                "let nextStates = " + Literal.stringify dfa.nextStates
                "let lexemesFromFinal = " + Literal.stringify dfa.lexemesFromFinal
                "let universalFinals = " + Literal.stringify dfa.universalFinals
                "let indicesFromFinal = " + Literal.stringify dfa.indicesFromFinal
                "open FSharpCompiler.Analyzing"
                "let analyzer = LexicalAnalyzer( nextStates, lexemesFromFinal, universalFinals, indicesFromFinal )"
            ]
            |> String.concat Environment.NewLine

        let outputDir = Path.Combine(locatePath, "ExcelDFA.fs")
        File.WriteAllText(outputDir,result)
        output.WriteLine("output lex:" + outputDir)

    [<Fact>]
    member this.``verify DFA``() =
        Should.equal dfa.nextStates  ExcelDFA.nextStates
        Should.equal dfa.lexemesFromFinal ExcelDFA.lexemesFromFinal
        Should.equal dfa.universalFinals  ExcelDFA.universalFinals
        Should.equal dfa.indicesFromFinal ExcelDFA.indicesFromFinal

