namespace ExcelCompiler

open Xunit
open Xunit.Abstractions

type NameParserTest(output : ITestOutputHelper) =

    [<Fact>]
    member this.``parse names``() =
        let data =
            [
                "sheet1!x","sheet1","x"
                "sheet2!x","sheet2","x"
                "x","","x"
            ]
        for name,prefix,nick in data do
            let p,n = ExcelCompiler.NameParser.split name
            Assert.Equal(p,prefix)
            Assert.Equal(n,nick)
