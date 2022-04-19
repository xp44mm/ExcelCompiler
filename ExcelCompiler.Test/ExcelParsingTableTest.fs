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
    let rawFsyacc = FsyaccFile.parse text
    let fsyacc = NormFsyaccFile.fromRaw rawFsyacc
        
    [<Fact>]
    member _.``1 - 显示冲突状态的冲突项目``() =
        let collection =
            AmbiguousCollection.create <| fsyacc.getMainProductions()
        let conflicts =
            collection.filterConflictedClosures()
        show conflicts

        //Should.equal y conflicts

    [<Fact>]
    member _.``2 - 汇总冲突的产生式``() =
        let collection =
            AmbiguousCollection.create <| fsyacc.getMainProductions()
        let conflicts =
            collection.filterConflictedClosures()

        let productions =
            AmbiguousCollection.gatherProductions conflicts
        // production -> %prec
        let pprods =
            ProductionUtils.precedenceOfProductions 
                collection.grammar.terminals 
                productions
            |> List.ofArray
        //优先级应该据此结果给出，不能少，也不应该多。
        let y = [
            ["expr";"expr";"%"],"%";
            ["expr";"expr";"&";"expr"],"&";
            ["expr";"expr";"*";"expr"],"*";
            ["expr";"expr";"+";"expr"],"+";
            ["expr";"expr";"-";"expr"],"-";
            ["expr";"expr";"/";"expr"],"/";
            ["expr";"expr";"<";"expr"],"<";
            ["expr";"expr";"<=";"expr"],"<=";
            ["expr";"expr";"<>";"expr"],"<>";
            ["expr";"expr";"=";"expr"],"=";
            ["expr";"expr";">";"expr"],">";
            ["expr";"expr";">=";"expr"],">=";
            ["expr";"NEGATIVE";"expr"],"NEGATIVE";
            ["expr";"POSITIVE";"expr"],"POSITIVE";
            ["expr";"expr";"^";"expr"],"^"]
        
        Should.equal y pprods

    [<Fact>]
    member _.``3 - print the template of type annotaitions``() =
        let grammar = Grammar.from <| fsyacc.getMainProductions()
        let symbols = 
            grammar.symbols 
            |> Set.filter(fun x -> Regex.IsMatch(x,@"^\w+$"))
        let sourceCode = 
            [
                for i in symbols do
                    i + " : \"\""
            ] |> String.concat "\r\n"
        output.WriteLine(sourceCode)

    [<Fact(Skip="once and for all!")>] // 
    member _.``5 - generate parsing table``() =
        let name = "ExcelParsingTable"
        let moduleName = $"ExcelCompiler.{name}"

        let parseTbl = fsyacc.toFsyaccParseTableFile()
        //解析表数据
        let fsharpCode = parseTbl.generate(moduleName)
        let outputDir = Path.Combine(sourcePath, $"{name}.fs")

        File.WriteAllText(outputDir,fsharpCode)
        output.WriteLine("output path:"+outputDir)

    [<Fact>]
    member _.``6 - valid ParseTable``() =
        let t = fsyacc.toFsyaccParseTableFile()

        Should.equal t.header        ExcelParsingTable.header
        Should.equal t.rules   ExcelParsingTable.rules
        Should.equal t.actions       ExcelParsingTable.actions
        Should.equal t.closures ExcelParsingTable.closures
        Should.equal t.declarations  ExcelParsingTable.declarations

