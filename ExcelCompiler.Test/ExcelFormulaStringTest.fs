namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Idioms
open FSharp.xUnit

type ExcelFormulaStringTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    [<Fact>]
    member _.``tokenize test``() =
        let x = "=(A1+A2)/2"
        let y = ExcelFormulaString.tokenize x |> Seq.map(fun x -> x.value) |> Seq.toList
        let z = [EQ;LPAREN;ID "A1";ADD;ID "A2";RPAREN;DIV;INTEGER "2"]
        Should.equal y z

    [<Fact>]
    member _.``normToken test``() =
        let x = "=sheet1!A2"
        let y = ExcelFormulaString.normToken x |> List.ofSeq  |> List.map(fun x -> x.value)
        let z = [EQ;REFERENCE(["sheet1"],["A2"])]
        Should.equal y z

    [<Fact>]
    member _.``parseToExpr test``() =
        let x = "(A1+A2)/2"
        let y = ExcelFormulaString.parseToExpr x
        //show y
        let z = Div(Add(Reference([],["A1"]),Reference([],["A2"])),Number "2")
        Should.equal y z

    [<Fact>]
    member _.``split name``() =
        let x = "sheet1!x"
        let y = ExcelFormulaString.splitName x
        let z = "sheet1","x"
        Should.equal y z

    [<Fact>]
    member _.``message test``() =
        let tokens = [EQ;LBRACKET;ID "工作簿1.xlsx";RBRACKET;ID "Sheet1";EXCLAM;DOLLAR "$A$1"]
        let y = ExcelFormulaString.varifyMessage tokens
        Should.equal y "中括号"

    [<Fact>]
    member _.``pi test``() =
        let expr = Mul(Func("pi",[]),Number "3")
        let y = ExcelFormulaString.fsharpString expr
        Should.equal y "Math.PI*3.0"
