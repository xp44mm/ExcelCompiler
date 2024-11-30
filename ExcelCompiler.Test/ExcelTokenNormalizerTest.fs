namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Idioms
open FSharp.xUnit
open FslexFsyacc
open ExcelCompiler.ExcelDFA

type ExcelTokenNormalizerTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine


    let norm x = 
        x
        |> ExcelTokenUtils.tokenize 0
        |> ExcelExprCompiler.analyze
        //|> Seq.concat
        |> List.ofSeq

    [<Fact>]
    member _.``0 test``() =
        let x = " - 1:1 "
        let y = norm x |> List.map(fun x -> x.value)

        Should.equal y [NEGATIVE;REFERENCE([],["1";"1"])]

    [<Fact>]
    member _.``1 test``() =
        let x = " + 1 + + 1.1 1 "
        let y = norm x |> List.map(fun x -> x.value)

        Should.equal y [NUMBER "+1";ADD;NUMBER "+1.1";NUMBER "1"]

    [<Fact>]
    member _.``2 test``() =
        let x = " na() "
        let y = norm x |> List.map(fun x -> x.value)
        Should.equal y [FUNCTION "na";LPAREN;RPAREN]

    [<Fact>]
    member _.``345 test``() =
        let x = " (,,) "
        let y = norm x |> List.map(fun x -> x.value)
        Should.equal y [LPAREN;COMMA;COMMA;RPAREN]

    [<Fact>]
    member _.``6 test``() =
        let x = " xyz a1 $a1 b2:c3 5:6"
        let y = norm x |> List.map(fun x -> x.value)
        Should.equal y [REFERENCE([],["xyz"]);REFERENCE([],["a1"]);REFERENCE([],["$a1"]);REFERENCE([],["b2";"c3"]);REFERENCE([],["5";"6"])]

    [<Fact>]
    member _.``6 worksheet test``() =
        let x = " xyz:wed!a1:b3 'a1'!c1  "
        let y = norm x |> List.map(fun x -> x.value)
        Should.equal y [REFERENCE(["xyz";"wed"],["a1";"b3"]);REFERENCE(["'a1'"],["c1"])]

    [<Fact>]
    member _.``7 worksheet test``() =
        let x = """ "" #N/A false true  """
        let y = norm x |> List.map(fun x -> x.value)
        Should.equal y [QUOTE "\"\"";ERROR "#N/A";FALSE;TRUE]
