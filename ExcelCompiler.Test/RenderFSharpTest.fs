namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit

type RenderFSharpTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine
            
    //[<Fact>]
    //member _.``smoke test``() =
    //    //let formula = "SQRT($C$9/$C$8)"
    //    //let expr = parse formula

    //    let y = RenderFSharp.norm expr
    //    Should.equal y "(C9/C8)**0.5"

    //[<Fact>]
    //member _.``pi test``() =
    //    //let formula = "pi()*3"
    //    //let expr = parse formula
    //    //show expr

    //    let y = RenderFSharp.norm expr
    //    Should.equal y "Math.PI*3.0"


