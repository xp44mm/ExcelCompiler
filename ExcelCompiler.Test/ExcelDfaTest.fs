namespace ExcelCompiler

open System
open System.IO

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit
open FslexFsyacc.Fslex

type ExcelDfaTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    let solutionPath = DirectoryInfo(__SOURCE_DIRECTORY__).Parent.FullName
    let sourcePath = Path.Combine(solutionPath, @"ExcelCompiler")
    let filePath = Path.Combine(sourcePath, @"excel.fslex")
    let text = File.ReadAllText(filePath)
    let fslex = FslexFile.parse text

    [<Fact(Skip="once and for all!")>] // 
    member this.``1 - generate DFA``() =
        let name = "ExcelDFA"
        let moduleName = $"ExcelCompiler.{name}"

        let y = fslex.toFslexDFA()
        let result = y.generate(moduleName)

        let outputDir = Path.Combine(sourcePath, $"{name}.fs")
        File.WriteAllText(outputDir, result)
        output.WriteLine("output lex:" + outputDir)

    [<Fact>]
    member this.``2 - valid DFA``() =
        let y = fslex.toFslexDFA()

        Should.equal y.dfa.nextStates       ExcelDFA.nextStates
        Should.equal y.dfa.lexemesFromFinal ExcelDFA.lexemesFromFinal
        Should.equal y.dfa.universalFinals  ExcelDFA.universalFinals
        Should.equal y.dfa.indicesFromFinal ExcelDFA.indicesFromFinal
        Should.equal y.header               ExcelDFA.header
        Should.equal y.semantics            ExcelDFA.semantics
