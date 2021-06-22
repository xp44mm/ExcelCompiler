module ExcelCompiler.ExcelFormulaString

/// 
let tokenize(formula:string) = ExcelToken.tokenize formula

let normToken(formula:string) = 
    formula 
    |> ExcelToken.tokenize  
    |> ExcelTokenNormalizer.normalize

let parseToExpr(formula:string) = 
    formula
    |> normToken
    |> ExcelExprDriver.tokensToExpr

let splitName(nameString:string) = NameParser.split nameString

let varifyMessage(tokens:ExcelToken list) = Validation.message tokens

let fsharpString(expr:ExcelExpr) = RenderFSharp.norm expr