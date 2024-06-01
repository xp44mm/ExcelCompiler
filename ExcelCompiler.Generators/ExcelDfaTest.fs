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
    let name = "ExcelDFA"
    let moduleName = $"ExcelCompiler.{name}"
    let modulePath = Path.Combine(Dir.sourcePath, $"{name}.fs")

    let filePath = Path.Combine( __SOURCE_DIRECTORY__ , @"excel.fslex")
    let text = File.ReadAllText(filePath)
    let fslex = FslexFileUtils.parse text

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
        let src = fslex |> FslexFileUtils.toFslexDFAFile
        Should.equal src.nextStates ExcelDFA.nextStates

        let headerFslex =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",src.header)

        let semansFslex =
            let mappers = src |> FslexDFAFileUtils.generateMappers
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        let header,semans =
            let src = File.ReadAllText(modulePath, Encoding.UTF8)
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 1 src

        Should.equal headerFslex header
        Should.equal semansFslex semans


