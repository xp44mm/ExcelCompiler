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
    let rawFsyacc = RawFsyaccFile.parse text
    let fsyacc = FlatFsyaccFile.fromRaw rawFsyacc

    let parseTblName = "ExcelParsingTable"
    let parseTblPath = Path.Combine(sourcePath, $"{parseTblName}.fs")

    [<Fact(Skip="Run manually when required")>]
    member _.``01 - format fsyacc file``() =
        let startSymbol = 
            fsyacc.rules.[0] 
            |> Triple.first 
            |> List.head
        let fsyacc = fsyacc.start(startSymbol, Set.empty).toRaw()
        output.WriteLine(fsyacc.render ())

    [<Fact>]
    member _.``02 - list all tokens``() =
        let grammar =
            fsyacc.getMainProductions()
            |> Grammar.from

        let tokens = grammar.terminals
        let res = set ["%";"&";"(";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE";"^"]

        //show tokens
        Should.equal tokens res

    [<Fact>]
    member _.``03 - precedence Of Productions``() =
        let collection = 
            fsyacc.getMainProductions() 
            |> AmbiguousCollection.create

        let terminals = 
            collection.grammar.terminals

        let productions =
            collection.collectConflictedProductions()

        let pprods = 
            ProductionUtils.precedenceOfProductions terminals productions

        let e = [["expr";"expr";"%"],"%";["expr";"expr";"&";"expr"],"&";["expr";"expr";"*";"expr"],"*";["expr";"expr";"+";"expr"],"+";["expr";"expr";"-";"expr"],"-";["expr";"expr";"/";"expr"],"/";["expr";"expr";"<";"expr"],"<";["expr";"expr";"<=";"expr"],"<=";["expr";"expr";"<>";"expr"],"<>";["expr";"expr";"=";"expr"],"=";["expr";"expr";">";"expr"],">";["expr";"expr";">=";"expr"],">=";["expr";"NEGATIVE";"expr"],"NEGATIVE";["expr";"POSITIVE";"expr"],"POSITIVE";["expr";"expr";"^";"expr"],"^"]
        Should.equal e pprods

    [<Fact>]
    member _.``04 - list all states``() =
        let collection =
            fsyacc.getMainProductions()
            |> AmbiguousCollection.create
        
        let text = collection.render()
        output.WriteLine(text)

    [<Fact>]
    member _.``05 - list the type annotaitions``() =
        let grammar =
            fsyacc.getMainProductions()
            |> Grammar.from

        let sourceCode =
            [
                "// Do not list symbols whose return value is always `null`"
                "// terminals: ref to the returned type of getLexeme"
                for i in grammar.terminals do
                    let i = RenderUtils.renderSymbol i
                    i + " : \"\""
                "\r\n// nonterminals"
                for i in grammar.nonterminals do
                    let i = RenderUtils.renderSymbol i
                    i + " : \"\""
            ] 
            |> String.concat "\r\n"

        output.WriteLine(sourceCode)


    [<Fact(Skip="once and for all!")>] // 
    member _.``06 - generate parsing table``() =
        let moduleName = $"ExcelCompiler.{parseTblName}"

        let parseTbl = fsyacc.toFsyaccParseTableFile ()
        //解析表数据
        let fsharpCode = parseTbl.generateModule (moduleName)

        File.WriteAllText(parseTblPath, fsharpCode)
        output.WriteLine("output path:" + parseTblPath)


    [<Fact>]
    member _.``07 - valid ParseTable``() =
        let src = fsyacc.toFsyaccParseTableFile()

        Should.equal src.actions ExcelParsingTable.actions
        Should.equal src.closures ExcelParsingTable.closures

        let headerFromFsyacc =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",src.header)

        let semansFsyacc =
            let mappers = src.generateMappers()
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers


        let header,semans =
            let txt = File.ReadAllText(parseTblPath, Encoding.UTF8)
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 2 txt

        Should.equal headerFromFsyacc header
        Should.equal semansFsyacc semans



