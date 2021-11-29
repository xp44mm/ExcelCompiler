namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit

type ExcelTokenNormalizerTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    [<Fact>]
    member this.``0 test``() =
        let x = " - 1:1 "
        let y = 
            ExcelToken.tokenize x 
            |> ExcelDFA.split
            |> Seq.concat
            |> List.ofSeq

        Should.equal y [NEGATIVE;REFERENCE([],["1";"1"])]

    [<Fact>]
    member this.``1 test``() =
        let x = " + 1 + + 1.1 1 "

        let y = 
            ExcelToken.tokenize x 
            |> ExcelDFA.split
            |> Seq.concat
            |> List.ofSeq

        Should.equal y [NUMBER "+1";ADD;NUMBER "+1.1";NUMBER "1"]

    [<Fact>]
    member this.``2 test``() =
        let x = " na() "
        let y = 
            ExcelToken.tokenize x 
            |> ExcelDFA.split
            |> Seq.concat
            |> List.ofSeq


        Should.equal y [FUNCTION "na";LPAREN;RPAREN]

    [<Fact>]
    member this.``345 test``() =
        let x = " (,,) "
        let y = 
            ExcelToken.tokenize x 
            |> ExcelDFA.split
            |> Seq.concat
            |> List.ofSeq


        Should.equal y [LPAREN;FUNCTION "NA";LPAREN;RPAREN;COMMA;FUNCTION "NA";LPAREN;RPAREN;COMMA;FUNCTION "NA";LPAREN;RPAREN;RPAREN]

    [<Fact>]
    member this.``6 test``() =
        let x = " xyz a1 $a1 b2:c3 5:6"
        let y = 
            ExcelToken.tokenize x 
            |> ExcelDFA.split
            |> Seq.concat
            |> List.ofSeq


        Should.equal y [REFERENCE([],["xyz"]);REFERENCE([],["a1"]);REFERENCE([],["$a1"]);REFERENCE([],["b2";"c3"]);REFERENCE([],["5";"6"])]

    [<Fact>]
    member this.``6 worksheet test``() =
        let x = " xyz:wed!a1:b3 'a1'!c1  "
        let y = 
            ExcelToken.tokenize x 
            |> ExcelDFA.split
            |> Seq.concat
            |> List.ofSeq


        Should.equal y [REFERENCE(["xyz";"wed"],["a1";"b3"]);REFERENCE(["'a1'"],["c1"])]

    [<Fact>]
    member this.``7 worksheet test``() =
        let x = """ "" #N/A false true  """
        let y = 
            ExcelToken.tokenize x 
            |> ExcelDFA.split
            |> Seq.concat
            |> List.ofSeq


        Should.equal y [QUOTE "\"\"";ERROR "#N/A";FALSE;TRUE]
