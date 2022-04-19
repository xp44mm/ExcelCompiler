namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals
open FSharp.xUnit

type ValidationTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    [<Fact>]
    member _.``message test 1``() =
        let x = "='C:\\Users\\cuishengli\\Desktop\\[工作簿1.xlsx]Sheet1'!$A$1"
        let tokens = ExcelTokenUtils.tokenize x |> List.ofSeq
        let y = Validation.message tokens
        Should.equal y "外部引用文件"

    [<Fact>]
    member _.``message test 2``() =
        let x = "=[工作簿1.xlsx]Sheet1!$D$15"
        let tokens = 
            ExcelTokenUtils.tokenize x 
            |> List.ofSeq
        let y = Validation.message tokens
        Should.equal y "中括号"

    [<Fact>]
    member _.``message test 3``() =
        let x = "='C:\\Program Files\\Microsoft Office\\root\\Office16\\LIBRARY\\Analysis\\ATPVBAEN.XLA'!mround(C36,0.25)"
        let tokens = ExcelTokenUtils.tokenize x |> List.ofSeq
        let y = Validation.message tokens
        Should.equal y "外部函数"

    [<Fact>]
    member _.``message test 4``() =
        let x = """=format(\"x{0:00}\",K2)"""
        let tokens = ExcelTokenUtils.tokenize x |> List.ofSeq
        let y = Validation.message tokens
        Should.equal y ""

    [<Fact>]
    member _.``message test 5``() =
        let x = "=[工作簿1.xlsx]Sheet1!$A$1"
        let tokens = ExcelTokenUtils.tokenize x |> List.ofSeq
        let y = Validation.message tokens
        Should.equal y "中括号"
