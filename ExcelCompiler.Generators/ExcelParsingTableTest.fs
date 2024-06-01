namespace ExcelCompiler

open FSharp.Idioms
open FSharp.Idioms.Literal
open FSharp.xUnit
open FslexFsyacc
open FslexFsyacc.Fsyacc
open FslexFsyacc.Runtime
open FslexFsyacc.Runtime.Precedences
open FslexFsyacc.Runtime.YACCs

open System.IO
open System.Text
open Xunit
open Xunit.Abstractions


type ExcelParsingTableTest(output: ITestOutputHelper) =

    let parseTblName = "ExcelParsingTable"
    let parseTblModule = $"ExcelCompiler.{parseTblName}"
    let parseTblPath = Path.Combine(Dir.sourcePath, $"{parseTblName}.fs")

    let filePath = Path.Combine( __SOURCE_DIRECTORY__ , @"excel.fsyacc")
    let text = File.ReadAllText(filePath,Encoding.UTF8)

    let rawFsyacc =
        text
        |> FsyaccCompiler.compile

    let fsyacc =
        rawFsyacc
        |> FslexFsyacc.Runtime.YACCs.FlatFsyaccFile.from

    let coder = FsyaccParseTableCoder.from fsyacc

    let tbl =
        fsyacc.getYacc()

    let bnf = tbl.bnf

    //[<Fact>]
    //member _.``01 - norm fsyacc file``() =
    //    let s0 = tblCrew.startSymbol
    //    let flatedFsyacc =
    //        fsyaccCrew
    //        |> FlatedFsyaccFileCrewUtils.toFlatFsyaccFile

    //    let src = 
    //        flatedFsyacc 
    //        |> FlatFsyaccFileUtils.start s0
    //        |> RawFsyaccFileUtils.fromFlat
    //        |> RawFsyaccFileUtils.render

    //    output.WriteLine(src)

    //[<Fact>]
    //member _.``02 - list all tokens``() =
    //    let y = set ["%";"&";"(";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE";"^"]
    //    //show collectionCrew.terminals
    //    Should.equal y collectionCrew.terminals


    [<Fact>]
    member _.``02 - print conflict productions``() =
        let st = ConflictedProduction.from fsyacc.rules
        for cp in st do
        output.WriteLine($"{stringify cp}")


    //[<Fact>]
    //member _.``05 - list declarations``() =
    //    let terminals =
    //        tblCrew.terminals
    //        |> Seq.map RenderUtils.renderSymbol
    //        |> String.concat " "

    //    let nonterminals =
    //        tblCrew.nonterminals
    //        |> Seq.map RenderUtils.renderSymbol
    //        |> String.concat " "

    //    let sourceCode =
    //        [
    //            "// Do not list symbols whose return value is always `null`"
    //            ""
    //            "// terminals: ref to the returned type of `getLexeme`"
    //            "%type<> " + terminals
    //            ""
    //            "// nonterminals"
    //            "%type<> " + nonterminals
    //        ] 
    //        |> String.concat "\r\n"

    //    output.WriteLine(sourceCode)


    [<Fact(
    Skip="按需更新源代码"
    )>]
    member _.``02 - generate Parse Table``() =
        let outp = coder.generateModule(parseTblModule)
        File.WriteAllText(parseTblPath, outp, Encoding.UTF8)
        output.WriteLine("output yacc:")
        output.WriteLine(parseTblPath)
        
    [<Fact>]
    member _.``07 - valid ParseTable``() =
        Should.equal coder.tokens ExcelParsingTable.tokens
        Should.equal coder.kernels ExcelParsingTable.kernels
        Should.equal coder.actions ExcelParsingTable.actions

        //产生式比较
        let prodsFsyacc =
            fsyacc.rules
            |> Seq.map (fun rule -> rule.production)
            |> Seq.toList

        let prodsParseTable =
            ExcelParsingTable.rules
            |> List.map fst

        Should.equal prodsFsyacc prodsParseTable

        //header,semantic代码比较
        let headerFromFsyacc =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",fsyacc.header)

        let semansFsyacc =
            let mappers = coder.generateMappers()
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        let header,semans =
            let text = File.ReadAllText(parseTblPath, Encoding.UTF8)
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 4 text

        Should.equal headerFromFsyacc header
        Should.equal semansFsyacc semans
