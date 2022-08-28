namespace ExcelCompiler

open System
open System.IO
open System.Text

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
    member _.``10 - valid DFA``() =
        let src = fslex.toFslexDFAFile()
        Should.equal src.nextStates ExcelDFA.nextStates

        let headerFslex =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",src.header)

        let semansFslex =
            let mappers = src.generateMappers()
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        let header,semans =
            let filePath = Path.Combine(sourcePath, "ExcelDFA.fs")
            File.ReadAllText(filePath, Encoding.UTF8)
            |> FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 1

        Should.equal headerFslex header
        Should.equal semansFslex semans


