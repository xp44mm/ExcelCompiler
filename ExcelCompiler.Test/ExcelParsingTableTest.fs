namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open System
open System.IO
open System.Text
open System.Text.RegularExpressions

open FSharp.Idioms
open FSharp.Literals
open FSharp.xUnit

open FslexFsyacc.Fsyacc
open FslexFsyacc.Yacc
open FslexFsyacc.Runtime

type ExcelParsingTableTest(output: ITestOutputHelper) =
    let show res =
        res |> Render.stringify |> output.WriteLine

    let solutionPath =
        DirectoryInfo(
            __SOURCE_DIRECTORY__
        )
            .Parent
            .FullName

    let sourcePath = Path.Combine(solutionPath, @"ExcelCompiler")
    let filePath = Path.Combine(sourcePath, @"excel.fsyacc")
    let text = File.ReadAllText(filePath)
    //let rawFsyacc = RawFsyaccFile.parse text
    //let fsyacc = FlatFsyaccFile.fromRaw rawFsyacc

    let parseTblName = "ExcelParsingTable"
    let parseTblModule = $"ExcelCompiler.{parseTblName}"
    let parseTblPath = Path.Combine(sourcePath, $"{parseTblName}.fs")

    let grammar text =
        text
        |> FlatFsyaccFileUtils.parse
        |> FlatFsyaccFileUtils.toGrammar

    let ambiguousCollection text =
        text
        |> FlatFsyaccFileUtils.parse
        |> FlatFsyaccFileUtils.toAmbiguousCollection

    //解析表数据
    let parseTbl text = 
        text
        |> FlatFsyaccFileUtils.parse
        |> FlatFsyaccFileUtils.toFsyaccParseTableFile

    [<Fact>]
    member _.``01 - norm fsyacc file``() =
        let fsyacc = 
            text
            |> FlatFsyaccFileUtils.parse

        let s0 = 
            fsyacc.rules
            |> FlatFsyaccFileRule.getStartSymbol

        let src = 
            fsyacc.start(s0, Set.empty)
            |> RawFsyaccFileUtils.fromFlat
            |> RawFsyaccFileUtils.render

        output.WriteLine(src)

    [<Fact>]
    member _.``02 - list all tokens``() =
        let grammar = grammar text
        let y = set ["%";"&";"(";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE";"^"]
        show grammar.terminals
        Should.equal y grammar.terminals

    [<Fact>]
    member _.``03 - list all states``() =
        let collection = ambiguousCollection text
        
        let src = collection.render()
        output.WriteLine(src)

    [<Fact>]
    member _.``04 - precedence Of Productions`` () =
        let collection = ambiguousCollection text

        let productions = 
            collection.collectConflictedProductions()

        // production -> %prec
        let pprods =
            ProductionUtils.precedenceOfProductions collection.grammar.terminals productions

        //优先级应该据此结果给出，不能少，也不应该多。
        let y = [["expr";"expr";"%"],"%";["expr";"expr";"&";"expr"],"&";["expr";"expr";"*";"expr"],"*";["expr";"expr";"+";"expr"],"+";["expr";"expr";"-";"expr"],"-";["expr";"expr";"/";"expr"],"/";["expr";"expr";"<";"expr"],"<";["expr";"expr";"<=";"expr"],"<=";["expr";"expr";"<>";"expr"],"<>";["expr";"expr";"=";"expr"],"=";["expr";"expr";">";"expr"],">";["expr";"expr";">=";"expr"],">=";["expr";"NEGATIVE";"expr"],"NEGATIVE";["expr";"POSITIVE";"expr"],"POSITIVE";["expr";"expr";"^";"expr"],"^"]

        Should.equal y pprods

    [<Fact>]
    member _.``05 - list declarations``() =
        let grammar = grammar text

        let terminals =
            grammar.terminals
            |> Seq.map RenderUtils.renderSymbol
            |> String.concat " "

        let nonterminals =
            grammar.nonterminals
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


    [<Fact()>] // Skip="once and for all!"
    member _.``06 - generate parsing table``() =

        let parseTbl = parseTbl text
        //解析表数据
        let fsharpCode = parseTbl.generateModule (parseTblModule)

        File.WriteAllText(parseTblPath, fsharpCode)
        output.WriteLine("output path:" + parseTblPath)


    [<Fact>]
    member _.``07 - valid ParseTable``() =
        let parseTbl = parseTbl text

        Should.equal parseTbl.actions ExcelParsingTable.actions
        Should.equal parseTbl.closures ExcelParsingTable.closures

        let headerFromFsyacc =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",parseTbl.header)

        let semansFsyacc =
            let mappers = parseTbl.generateMappers()
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers


        let header,semans =
            let txt = File.ReadAllText(parseTblPath, Encoding.UTF8)
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 2 txt

        Should.equal headerFromFsyacc header
        Should.equal semansFsyacc semans



