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
    member _.``1 - generate DFA``() =
        let name = "ExcelDFA"
        let moduleName = $"ExcelCompiler.{name}"

        let y = fslex.toFslexDFAFile()
        let result = y.generate(moduleName)

        let outputDir = Path.Combine(sourcePath, $"{name}.fs")
        File.WriteAllText(outputDir, result)
        output.WriteLine("output lex:" + outputDir)

    [<Fact>]
    member _.``2 - valid DFA``() =
        let y = fslex.toFslexDFAFile()
        Should.equal y.nextStates ExcelDFA.nextStates
        Should.equal y.header     ExcelDFA.header
        Should.equal y.rules      ExcelDFA.rules
