module ExcelCompiler.ExcelFormulaString

/// 
let tokenize(formula:string) = ExcelTokenUtils.tokenize formula

let normToken(formula:string) = 
    formula 
    |> ExcelTokenUtils.tokenize  
    |> ExcelDFA.analyze
    |> Seq.concat

let parseToExpr(formula:string) = 
    formula
    |> normToken
    |> ExcelParsingTable.parse

let splitName(nameString:string) = NameParser.split nameString

let varifyMessage(tokens:ExcelToken list) = Validation.message tokens

let fsharpString(expr:ExcelExpr) = RenderFSharp.norm expr