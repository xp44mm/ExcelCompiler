﻿namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit

type ExcelTokenTest (output: ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    [<Fact>]
    member this.``space test``() =
        let x = " "
        let y = ExcelToken.tokenize x
        Assert.True(Seq.isEmpty y)

    [<Fact>]
    member this.``simple operators test``() =
        let x = "!:,()=>=><<=<>&+-*/^%{}[]"

        let y = ExcelToken.tokenize x |> List.ofSeq
        //show y
        Should.equal y [EXCLAM;COLON;COMMA;LPAREN;RPAREN;EQ;GE;GT;LT;LE;NE;AMPERSAND;POSITIVE;NEGATIVE;MUL;DIV;CARET;PERCENT;LBRACE;RBRACE;LBRACKET;RBRACKET]

    [<Fact>]
    member this.``quete test``() =
        let x = """ "a""b" """

        let y = ExcelToken.tokenize x |> Seq.exactlyOne
        //show y
        Should.equal y (QUOTE "\"a\"\"b\"")

    [<Fact>]
    member this.``APOSTROPHE test``() =
        let x = """ 'a''b' """

        let y = ExcelToken.tokenize x |> Seq.exactlyOne
        //show y
        Should.equal y (APOSTROPHE "'a''b'")

    [<Fact>]
    member this.``error test``() =
        let x = """ #DIV/0! #N/A #NAME? #NULL! #NUM! #REF! #VALUE! """

        let y = ExcelToken.tokenize x |> List.ofSeq
        //show y
        Should.equal y [ERROR "#DIV/0!";ERROR "#N/A";ERROR "#NAME?";ERROR "#NULL!";ERROR "#NUM!";ERROR "#REF!";ERROR "#VALUE!"]

    [<Fact>]
    member this.``dollar test``() =
        let x = """ $a$1 $a $1 $a1 a$1 """

        let y = ExcelToken.tokenize x |> List.ofSeq
        //show y
        Should.equal y [DOLLAR "$a$1";DOLLAR "$a";DOLLAR "$1";DOLLAR "$a1";DOLLAR "a$1"]

    [<Fact>]
    member this.``number test``() =
        let x = """ 1.1 1e5 1e-5 2e+8 12 """

        let y = ExcelToken.tokenize x |> List.ofSeq
        //show y
        Should.equal y [NUMBER "1.1";NUMBER "1e5";NUMBER "1e-5";NUMBER "2e+8";INTEGER "12"]

    [<Fact>]
    member this.``bool test``() =
        let x = """ True FALSE true false """

        let y = ExcelToken.tokenize x |> List.ofSeq
        //show y
        Should.equal y [TRUE;FALSE;TRUE;FALSE]

    [<Fact>]
    member this.``id test``() =
        let x = """ xyz a1 w.u """

        let y = ExcelToken.tokenize x |> List.ofSeq
        //show y
        Should.equal y [ID "xyz";ID "a1";ID "w.u"]







