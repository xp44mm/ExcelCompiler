namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit

type RootsOfEquationsTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    [<Fact>]
    member _.``fixed point test``() =
        let formula = "A2-A1"
        let expr = ExcelExprCompiler.compile formula
        //show expr
        let y = Sub(Reference([],["A2"]),Reference([],["A1"]))
        Should.equal expr y
    [<Fact>]
    member _.``bisect test``() =
        let formula = "(A1+A2)/2"
        let expr = ExcelExprCompiler.compile formula
        //show expr
        let y = Div(Add(Reference([],["A1"]),Reference([],["A2"])),Number "2")
        Should.equal expr y


