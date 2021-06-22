namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.xUnit

type NameParserTest(output : ITestOutputHelper) =

    [<Fact>]
    member this.``parse names worksheet``() =
        let x = "sheet1!x"
        let y = NameParser.split x
        let z = "sheet1","x"
        Should.equal y z

    [<Fact>]
    member this.``parse names workbook``() =
        let x = "x"
        let y = NameParser.split x
        let z = "","x"
        Should.equal y z

