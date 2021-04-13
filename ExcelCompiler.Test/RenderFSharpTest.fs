namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals

type RenderFSharpTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    [<Fact>]
    member this.``smoke test``() =
        let formula = "=SQRT($C$9/$C$8)"
        let expr = ExcelExprDriver.parse formula
        let fsharpExpr = 
            RenderFSharp.norm expr
        show fsharpExpr

    [<Fact>]
    member this.``pi test``() =
        let formula = "=pi()*3"
        let expr = ExcelExprDriver.parse formula
        let fsharpExpr = 
            RenderFSharp.norm expr
        show fsharpExpr


