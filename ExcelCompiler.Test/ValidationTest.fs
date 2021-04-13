namespace ExcelCompiler

open Xunit
open Xunit.Abstractions
open FSharp.Literals

type ValidationTest(output: ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    [<Fact>]
    member this.``message test``() =
        let data =
            [
                ("Sheet1","$A$1"),"='C:\\Users\\cuishengli\\Desktop\\[工作簿1.xlsx]Sheet1'!$A$1";
                ("Sheet1","$A$2"),"=[工作簿1.xlsx]Sheet1!$D$15";
                ("Tower","$M$36"),"='C:\\Program Files\\Microsoft Office\\root\\Office16\\LIBRARY\\Analysis\\ATPVBAEN.XLA'!mround(C36,0.25)";
                ("shee2","A1"),"=format(\"x{0:00}\",K2)";
                ("shee2","B2"),"=[工作簿1.xlsx]Sheet1!$A$1";
            ]


        for (sh,ad),f in data do
            let tokens = ExcelToken.tokenize f |> List.ofSeq

            let y = Validation.message tokens
            match y with
            | "" -> ()
            | _ -> output.WriteLine(sprintf "%s!%s:%s" sh ad y)
