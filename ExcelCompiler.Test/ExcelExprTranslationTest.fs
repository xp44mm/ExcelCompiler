namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit

type ExcelExprTranslationTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine


    [<Fact>]
    member this.``func test``() =
        let x = "=na()"
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Func("na",[])

    [<Fact>]
    member this.``ref test``() =
        let x = "= a1 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Reference([],["a1"])

    [<Fact>]
    member this.``number test``() =
        let x = "= 1 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Number "1"

    [<Fact>]
    member this.``quote test``() =
        let x = "= \"\" "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Quote "\"\""

    [<Fact>]
    member this.``false test``() =
        let x = "= false "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| False

    [<Fact>]
    member this.``true test``() =
        let x = "= true "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| True

    [<Fact>]
    member this.``group test``() =
        let x = "= (1) "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Number "1"

    [<Fact>]
    member this.``eq test``() =
        let x = "= 1 = 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Eq(Number "1", Number "2")

    [<Fact>]
    member this.``gt test``() =
        let x = "= 1 > 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Gt(Number "1", Number "2")

    [<Fact>]
    member this.``ge test``() =
        let x = "= 1 >= 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Ge(Number "1", Number "2")

    [<Fact>]
    member this.``lt test``() =
        let x = "= 1 < 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Lt(Number "1", Number "2")

    [<Fact>]
    member this.``le test``() =
        let x = "= 1 <= 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Le(Number "1", Number "2")

    [<Fact>]
    member this.``ne test``() =
        let x = " = 1 <> 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Ne(Number "1", Number "2")

    [<Fact>]
    member this.``concat test``() =
        let x = """ = "1" & 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Concat(Quote "\"1\"",Number "2")

    [<Fact>]
    member this.``add test``() =
        let x = """ = 1 + 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Add(Number "1",Number "2")

    [<Fact>]
    member this.``sub test``() =
        let x = """ = 1 - 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Sub(Number "1",Number "2")

    [<Fact>]
    member this.``mul test``() =
        let x = """ = 1 * 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Mul(Number "1",Number "2")

    [<Fact>]
    member this.``div test``() =
        let x = """ = 1 / 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Div(Number "1",Number "2")

    [<Fact>]
    member this.``pow test``() =
        let x = """ = 1 ^ 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Pow(Number "1",Number "2")

    [<Fact>]
    member this.``percent test``() =
        let x = """ = 1 % """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Percent(Number "1")

    [<Fact>]
    member this.``posi test``() =
        let x = """ = + a """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Positive(Reference([],["a"]))

    [<Fact>]
    member this.``negative test``() =
        let x = """ = - a """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Negative(Reference([],["a"]))

    [<Fact>]
    member this.``arguments test``() =
        let x = """ = sum(1,2) """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Func("sum",[Number "1";Number "2"])

    [<Fact>]
    member this.``prec test``() =
        let x = """ = 1 & 2 = 3 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Eq(Concat(Number "1",Number "2"), Number "3")
