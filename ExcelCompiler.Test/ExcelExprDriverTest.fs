namespace ExcelCompiler
open ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals

type ExcelExprDriverTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    //[<Fact>]
    //member this.``parse names test``() =
    //    let data =
    //        [
    //            "=sheet1!x","sheet1","x"
    //            "=sheet2!x","sheet2","x"
    //            "=x","","x"
    //        ]
    //    for name,prefix,nick in data do
    //        let res = ExcelExprDriver.parse name
    //        show res

