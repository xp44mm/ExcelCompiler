namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals

type ExcelTokenizerTest (output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    [<Fact>]
    member this.``tokenize test``() =
        let data =
            [
                ("Sheet1","$A$1"),"='C:\\Users\\cuishengli\\Desktop\\[工作簿1.xlsx]Sheet1'!$A$1";
                ("Sheet1","$A$2"),"=[工作簿1.xlsx]Sheet1!$D$15";
                ("Tower","$M$36"),"='C:\\Program Files\\Microsoft Office\\root\\Office16\\LIBRARY\\Analysis\\ATPVBAEN.XLA'!mround(C36,0.25)";
                ("shee2","a1"),"=format(\"x{0:00}\",K2)";
            ]

        let inps = data |> List.map snd

        for f in inps do
            let tokens = ExcelToken.tokenize f |> List.ofSeq
            show tokens

            //let y = Validation.message tokens
            //show y


    [<Fact>]
    member this.``deprecate test``() =
        let data =
            [
                ("Sheet1","$A$1"),"='C:\\Users\\cuishengli\\Desktop\\[工作簿1.xlsx]Sheet1'!$A$1";
                ("Sheet1","$A$2"),"=[工作簿1.xlsx]Sheet1!$D$15";
                ("Tower","$M$36"),"='C:\\Program Files\\Microsoft Office\\root\\Office16\\LIBRARY\\Analysis\\ATPVBAEN.XLA'!mround(C36,0.25)";
            ]

        let inps = data |> List.map snd

        for f in inps do
            let tokens = ExcelToken.tokenize f |> List.ofSeq
            show tokens

            let y = Validation.message tokens
            show y
