namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open System
open System.IO
open System.Text
open System.Text.RegularExpressions

open FSharp.Idioms
open FSharp.xUnit

open FslexFsyacc.Fsyacc
open FslexFsyacc.Yacc
open FslexFsyacc.Runtime

type ExcelParsingTableTest(output: ITestOutputHelper) =
    let show res =
        res |> Literal.stringify |> output.WriteLine

    let solutionPath =
        DirectoryInfo(
            __SOURCE_DIRECTORY__
        )
            .Parent
            .FullName

    let sourcePath = Path.Combine(solutionPath, @"ExcelCompiler")
    let filePath = Path.Combine(sourcePath, @"excel.fsyacc")

    let parseTblName = "ExcelParsingTable"
    let parseTblModule = $"ExcelCompiler.{parseTblName}"
    let parseTblPath = Path.Combine(sourcePath, $"{parseTblName}.fs")

    let text = File.ReadAllText(filePath,Encoding.UTF8)

    let fsyaccCrew =
        text
        |> RawFsyaccFileCrewUtils.parse
        |> FlatedFsyaccFileCrewUtils.fromRawFsyaccFileCrew

    let inputProductionList =
        fsyaccCrew.flatedRules
        |> List.map Triple.first

    let collectionCrew = 
        inputProductionList
        |> AmbiguousCollectionCrewUtils.newAmbiguousCollectionCrew

    let tblCrew =
        fsyaccCrew
        |> FlatedFsyaccFileCrewUtils.getSemanticParseTableCrew

    [<Fact>]
    member _.``01 - norm fsyacc file``() =
        let s0 = tblCrew.startSymbol
        let flatedFsyacc =
            fsyaccCrew
            |> FlatedFsyaccFileCrewUtils.toFlatFsyaccFile

        let src = 
            flatedFsyacc 
            |> FlatFsyaccFileUtils.start s0
            |> RawFsyaccFileUtils.fromFlat
            |> RawFsyaccFileUtils.render

        output.WriteLine(src)

    [<Fact>]
    member _.``02 - list all tokens``() =
        let y = set ["%";"&";"(";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE";"^"]
        //show collectionCrew.terminals
        Should.equal y collectionCrew.terminals

    [<Fact>]
    member _.``03 - list all states``() =
        let src = 
            AmbiguousCollectionUtils.render
                tblCrew.terminals
                tblCrew.conflictedItemCores
                (tblCrew.kernels |> Seq.mapi(fun i k -> k,i) |> Map.ofSeq)
        output.WriteLine(src)

    [<Fact>]
    member _.``04 - precedence Of Productions`` () =
        let productions = 
            AmbiguousCollectionUtils.collectConflictedProductions tblCrew.conflictedItemCores

        // production -> %prec
        let pprods =
            ProductionSetUtils.precedenceOfProductions tblCrew.terminals productions

        //优先级应该据此结果给出，不能少，也不应该多。
        let y = [["expr";"expr";"%"],"%";["expr";"expr";"&";"expr"],"&";["expr";"expr";"*";"expr"],"*";["expr";"expr";"+";"expr"],"+";["expr";"expr";"-";"expr"],"-";["expr";"expr";"/";"expr"],"/";["expr";"expr";"<";"expr"],"<";["expr";"expr";"<=";"expr"],"<=";["expr";"expr";"<>";"expr"],"<>";["expr";"expr";"=";"expr"],"=";["expr";"expr";">";"expr"],">";["expr";"expr";">=";"expr"],">=";["expr";"NEGATIVE";"expr"],"NEGATIVE";["expr";"POSITIVE";"expr"],"POSITIVE";["expr";"expr";"^";"expr"],"^"]

        Should.equal y pprods


    [<Fact>]
    member _.``05 - list declarations``() =
        let terminals =
            tblCrew.terminals
            |> Seq.map RenderUtils.renderSymbol
            |> String.concat " "

        let nonterminals =
            tblCrew.nonterminals
            |> Seq.map RenderUtils.renderSymbol
            |> String.concat " "

        let sourceCode =
            [
                "// Do not list symbols whose return value is always `null`"
                ""
                "// terminals: ref to the returned type of `getLexeme`"
                "%type<> " + terminals
                ""
                "// nonterminals"
                "%type<> " + nonterminals
            ] 
            |> String.concat "\r\n"

        output.WriteLine(sourceCode)


    [<Fact(
    Skip = "写源代码文件"
    )>]
    member _.``06 - generate parsing table``() =
        let fsharpCode = 
            tblCrew
            |> FsyaccParseTableFileUtils.ofSemanticParseTableCrew
            |> FsyaccParseTableFileUtils.generateModule(parseTblModule)
        File.WriteAllText(parseTblPath,fsharpCode,Encoding.UTF8)
        output.WriteLine("output fsyacc:"+parseTblPath)


    [<Fact>]
    member _.``07 - valid ParseTable``() =
        Should.equal tblCrew.encodedActions FsyaccParseTable.actions
        Should.equal tblCrew.encodedClosures FsyaccParseTable.closures

        let prodsFsyacc =
            List.map fst tblCrew.rules

        let prodsParseTable =
            List.map fst FsyaccParseTable.rules
        Should.equal prodsFsyacc prodsParseTable

        let header,semans =
            File.ReadAllText(parseTblPath, Encoding.UTF8)
            |> FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 2

        let headerFromFsyacc =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",tblCrew.header)

        Should.equal headerFromFsyacc header

        let semansFsyacc =
            let mappers = 
                tblCrew
                |> FsyaccParseTableFileUtils.ofSemanticParseTableCrew
                |> FsyaccParseTableFileUtils.generateMappers
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        Should.equal semansFsyacc semans
