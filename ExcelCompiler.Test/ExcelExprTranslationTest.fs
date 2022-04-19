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
    member _.``func test``() =
        let x = "na()"
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Func("na",[])

    [<Fact>]
    member _.``ref test``() =
        let x = " a1 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Reference([],["a1"])

    [<Fact>]
    member _.``number test``() =
        let x = " 1 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Number "1"

    [<Fact>]
    member _.``quote test``() =
        let x = " \"\" "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Quote "\"\""

    [<Fact>]
    member _.``false test``() =
        let x = " false "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| False

    [<Fact>]
    member _.``true test``() =
        let x = " true "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| True

    [<Fact>]
    member _.``group test``() =
        let x = " (1) "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Number "1"

    [<Fact>]
    member _.``eq test``() =
        let x = " 1 = 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Eq(Number "1", Number "2")

    [<Fact>]
    member _.``gt test``() =
        let x = " 1 > 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Gt(Number "1", Number "2")

    [<Fact>]
    member _.``ge test``() =
        let x = "1 >= 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Ge(Number "1", Number "2")

    [<Fact>]
    member _.``lt test``() =
        let x = " 1 < 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Lt(Number "1", Number "2")

    [<Fact>]
    member _.``le test``() =
        let x = " 1 <= 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Le(Number "1", Number "2")

    [<Fact>]
    member _.``ne test``() =
        let x = " 1 <> 2 "
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Ne(Number "1", Number "2")

    [<Fact>]
    member _.``concat test``() =
        let x = """ "1" & 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Concat(Quote "\"1\"",Number "2")

    [<Fact>]
    member _.``add test``() =
        let x = """ 1 + 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Add(Number "1",Number "2")

    [<Fact>]
    member _.``sub test``() =
        let x = """ 1 - 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Sub(Number "1",Number "2")

    [<Fact>]
    member _.``mul test``() =
        let x = """ 1 * 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Mul(Number "1",Number "2")

    [<Fact>]
    member _.``div test``() =
        let x = """ 1 / 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Div(Number "1",Number "2")

    [<Fact>]
    member _.``pow test``() =
        let x = """ 1 ^ 2 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Pow(Number "1",Number "2")

    [<Fact>]
    member _.``percent test``() =
        let x = """ 1 % """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Percent(Number "1")

    [<Fact>]
    member _.``posi test``() =
        let x = """ + a """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Positive(Reference([],["a"]))

    [<Fact>]
    member _.``negative test``() =
        let x = """ - a """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Negative(Reference([],["a"]))

    [<Theory>]
    [<InlineData("()")>]
    [<InlineData("(,)")>]
    [<InlineData("(,1,2)")>]
    member _.``arguments test``(x) =
        let x = $"sum{x}"
        let y = ExcelExprDriver.parse x

        show y
        //Should.equal y <| Func("sum",[Some(Number "1");Some(Number "2")])

    [<Fact>]
    member _.``prec test``() =
        let x = """ 1 & 2 = 3 """
        let y = ExcelExprDriver.parse x

        //show y
        Should.equal y <| Eq(Concat(Number "1",Number "2"), Number "3")
