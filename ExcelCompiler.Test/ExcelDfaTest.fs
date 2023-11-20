namespace ExcelCompiler

open System
open System.IO
open System.Text

open Xunit
open Xunit.Abstractions

open FSharp.Idioms
open FSharp.xUnit

open FslexFsyacc.Fslex

type ExcelDfaTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    let solutionPath = DirectoryInfo(__SOURCE_DIRECTORY__).Parent.FullName
    let sourcePath = Path.Combine(solutionPath, @"ExcelCompiler")
    let filePath = Path.Combine(sourcePath, @"excel.fslex")
    let text = File.ReadAllText(filePath)
    let fslex = FslexFileUtils.parse text

    let name = "ExcelDFA"
    let moduleName = $"ExcelCompiler.{name}"
    let modulePath = Path.Combine(sourcePath, $"{name}.fs")

    [<Fact(
    Skip = "once and for all!"
    )>]
    member _.``1 - generate DFA``() =
        let fileObj = fslex |> FslexFileUtils.toFslexDFAFile
        let result = fileObj |> FslexDFAFileUtils.generate(moduleName)

        File.WriteAllText(modulePath, result, Encoding.UTF8)
        output.WriteLine("output lex:" + modulePath)

    [<Fact>]
    member _.``10 - valid DFA``() =
        let src = fslex|>FslexFileUtils.toFslexDFAFile
        Should.equal src.nextStates FslexDFA.nextStates

        let headerFslex =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",src.header)

        let semansFslex =
            let mappers = src|>FslexDFAFileUtils.generateMappers
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        let header,semans =
            let src = File.ReadAllText(modulePath, Encoding.UTF8)
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 1 src

        Should.equal headerFslex header
        Should.equal semansFslex semans


