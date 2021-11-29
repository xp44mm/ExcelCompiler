namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open System
open System.IO
open System.Text.RegularExpressions

open FSharp.Literals
open FSharp.xUnit

open FslexFsyacc.Fsyacc
open FslexFsyacc.Yacc

type ExcelParsingTableTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    let solutionPath = DirectoryInfo(__SOURCE_DIRECTORY__).Parent.FullName
    let sourcePath = Path.Combine(solutionPath, @"ExcelCompiler")
    let filePath = Path.Combine(sourcePath, @"excel.fsyacc")
    let text = File.ReadAllText(filePath)
    let fsyacc = FsyaccFile.parse text
    let parseTbl = fsyacc.toFsyaccParseTable()


    [<Fact>]
    member this.``3 - print the template of type annotaitions``() =
        
        let grammar = Grammar.from fsyacc.mainProductions

        let symbols = 
            grammar.symbols 
            |> Set.filter(fun x -> Regex.IsMatch(x,@"^\w+$"))

        let sourceCode = 
            [
                for i in symbols do
                    i + " \"\";"
            ] |> String.concat "\r\n"
        output.WriteLine(sourceCode)

    [<Fact>]
    member this.``2-产生式冲突``() =
        let tbl = AmbiguousTable.create fsyacc.mainProductions
        let pconflicts = ConflictFactory.productionConflict tbl.ambiguousTable
        //show pconflicts
        Assert.True(pconflicts.IsEmpty)

    [<Fact>]
    member this.``3-符号多用警告``() =
        let tbl = AmbiguousTable.create fsyacc.mainProductions
        let warning = ConflictFactory.overloadsWarning tbl
        //show warning
        Assert.True(warning.IsEmpty)

    [<Fact>]
    member this.``4-优先级冲突``() =
        let tbl = AmbiguousTable.create fsyacc.mainProductions
        let srconflicts = ConflictFactory.shiftReduceConflict tbl

        //show srconflicts
        let y = set [set [["expr";"NEGATIVE";"expr"];["expr";"expr";"%"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"&";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"*";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";">";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"%"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"&";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"*";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";">";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"&";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"*";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"+";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"%"];["expr";"expr";">";"expr"]];set [["expr";"expr";"%"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"&";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"*";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"*";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"+";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"-";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"/";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"<";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"<=";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"<>";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"=";"expr"]];set [["expr";"expr";"=";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"=";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"=";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";">";"expr"]];set [["expr";"expr";">";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";">";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";">=";"expr"]];set [["expr";"expr";">=";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"^";"expr"]]]
        Should.equal y srconflicts


    [<Fact(Skip="once and for all!")>] // 
    member this.``5 - generate parsing table``() =
        let name = "ExcelParsingTable"
        let moduleName = $"ExcelCompiler.{name}"

        //解析表数据
        let fsharpCode = parseTbl.generateParseTable(moduleName)

        let outputDir = Path.Combine(sourcePath, $"{name}.fs")
        File.WriteAllText(outputDir,fsharpCode)
        output.WriteLine("output path:"+outputDir)

    [<Fact>]
    member this.``6 - valid ParseTable``() =
        let t = fsyacc.toFsyaccParseTable()

        Should.equal t.header        ExcelParsingTable.header
        Should.equal t.productions   ExcelParsingTable.productions
        Should.equal t.actions       ExcelParsingTable.actions
        Should.equal t.kernelSymbols ExcelParsingTable.kernelSymbols
        Should.equal t.semantics     ExcelParsingTable.semantics
        Should.equal t.declarations  ExcelParsingTable.declarations

