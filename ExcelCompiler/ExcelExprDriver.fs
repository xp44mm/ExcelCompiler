module ExcelCompiler.ExcelExprDriver
    
///解析公式
let parse(formula:string) =
    formula
    |> ExcelToken.tokenize
    |> ExcelDFA.analyze
    |> Seq.concat
    |> ExcelParsingTable.parse
