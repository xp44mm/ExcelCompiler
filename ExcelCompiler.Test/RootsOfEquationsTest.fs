namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals

type RootsOfEquationsTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    [<Fact>]
    member this.``fixed point test``() =
        let formula = "=A2-A1"
        let expr = ExcelExprDriver.parse formula
        show expr

    [<Fact>]
    member this.``bisect test``() =
        let formula = "=(A1+A2)/2"
        let expr = ExcelExprDriver.parse formula
        show expr


