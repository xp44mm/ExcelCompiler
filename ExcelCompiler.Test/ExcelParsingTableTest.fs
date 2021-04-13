namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open System
open System.IO

open FSharpCompiler.Yacc
open FSharp.Literals
open FSharp.xUnit

type ExcelParsingTableTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    let locatePath = Path.Combine(
                        DirectoryInfo(__SOURCE_DIRECTORY__).Parent.FullName,
                        @"ExcelCompiler")
    let filePath = Path.Combine(locatePath, @"excel.yacc")
    let text = File.ReadAllText(filePath)
    let yaccFile = YaccFile.parse text

    [<Fact>]
    member this.``1-input data``() =
        //show yaccFile.mainRules
        let y = [["formula";"=";"expr"];["expr";"FUNCTION";"(";"arguments";")"];["expr";"REFERENCE"];["expr";"NUMBER"];["expr";"QUOTE"];["expr";"FALSE"];["expr";"TRUE"];["expr";"(";"expr";")"];["expr";"expr";"=";"expr"];["expr";"expr";"<";"expr"];["expr";"expr";"<=";"expr"];["expr";"expr";">";"expr"];["expr";"expr";">=";"expr"];["expr";"expr";"<>";"expr"];["expr";"expr";"&";"expr"];["expr";"expr";"+";"expr"];["expr";"expr";"-";"expr"];["expr";"expr";"*";"expr"];["expr";"expr";"/";"expr"];["expr";"expr";"^";"expr"];["expr";"expr";"%"];["expr";"POSITIVE";"expr"];["expr";"NEGATIVE";"expr"];["arguments"];["arguments";"expr"];["arguments";"arguments";",";"expr"]]
        
        Should.equal y yaccFile.mainRules

        //show yaccFile.precedences
        let z = [NonAssoc,[TerminalKey "<";TerminalKey "<=";TerminalKey "<>";TerminalKey "=";TerminalKey ">";TerminalKey ">="];LeftAssoc,[TerminalKey "&"];LeftAssoc,[TerminalKey "+";TerminalKey "-"];LeftAssoc,[TerminalKey "*";TerminalKey "/"];LeftAssoc,[TerminalKey "^"];LeftAssoc,[TerminalKey "%"];RightAssoc,[TerminalKey "POSITIVE";TerminalKey "NEGATIVE"]]
        Should.equal z yaccFile.precedences

    [<Fact>]
    member this.``2-产生式冲突``() =
        let tbl = AmbiguousTable.create yaccFile.mainRules
        let pconflicts = ConflictFactory.productionConflict tbl.ambiguousTable
        //show pconflicts
        Assert.True(pconflicts.IsEmpty)

    [<Fact>]
    member this.``3-符号多用警告``() =
        let tbl = AmbiguousTable.create yaccFile.mainRules
        let warning = ConflictFactory.overloadsWarning tbl
        //show warning
        Assert.True(warning.IsEmpty)

    [<Fact>]
    member this.``4-优先级冲突``() =
        let tbl = AmbiguousTable.create yaccFile.mainRules
        let srconflicts = ConflictFactory.shiftReduceConflict tbl

        //show srconflicts
        let y = set [set [["expr";"NEGATIVE";"expr"];["expr";"expr";"%"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"&";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"*";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";">";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"NEGATIVE";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"%"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"&";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"*";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";">";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"POSITIVE";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"&";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"*";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"+";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"%"];["expr";"expr";">";"expr"]];set [["expr";"expr";"%"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"%"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"&";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"*";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"&";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"*";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"+";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"*";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"+";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"-";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"+";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"-";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"/";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"-";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"/";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"<";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"/";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"<";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"<=";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"<";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"<=";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";"<>";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"<=";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"<>";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";"=";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"<>";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"=";"expr"]];set [["expr";"expr";"=";"expr"];["expr";"expr";">";"expr"]];set [["expr";"expr";"=";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";"=";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";">";"expr"]];set [["expr";"expr";">";"expr"];["expr";"expr";">=";"expr"]];set [["expr";"expr";">";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";">=";"expr"]];set [["expr";"expr";">=";"expr"];["expr";"expr";"^";"expr"]];set [["expr";"expr";"^";"expr"]]]
        Should.equal y srconflicts

    [<Fact(Skip="done")>] // 
    member this.``5-generate parsing table``() =
        let yacc = ParseTable.create(yaccFile.mainRules, yaccFile.precedences)

        //解析表数据
        let result =
            [
                "module ExcelCompiler.ExcelParsingTable"
                "let rules = " + Render.stringify yacc.rules
                "let kernelSymbols = " + Render.stringify yacc.kernelSymbols
                "let parsingTable = " + Render.stringify yacc.parsingTable
            ] |> String.concat Environment.NewLine
        let outputDir = Path.Combine(locatePath, "ExcelParsingTable.fs")
        File.WriteAllText(outputDir,result)
        output.WriteLine("output yacc:"+outputDir)

    [<Fact>]
    member this.``5-verify parsing table``() =
        let yacc = ParseTable.create(yaccFile.mainRules, yaccFile.precedences)

        Should.equal yacc.rules         ExcelParsingTable.rules
        Should.equal yacc.kernelSymbols ExcelParsingTable.kernelSymbols
        Should.equal yacc.parsingTable  ExcelParsingTable.parsingTable









