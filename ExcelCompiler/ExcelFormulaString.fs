module ExcelCompiler.ExcelFormulaString

/// 
let tokenize(formula:string) = ExcelTokenUtils.tokenize 0 formula

let normToken(formula:string) = 
    formula 
    |> ExcelTokenUtils.tokenize 0 
    |> ExcelExprCompiler.analyze
    //|> Seq.concat

let parseToExpr(formula:string) = 
    formula
    |> normToken
    |> ExcelExprCompiler.parseTokens

let splitName(nameString:string) = NameParser.split nameString

let varifyMessage(tokens:ExcelToken list) = Validation.message tokens

let fsharpString(expr:ExcelExpr) = RenderFSharp.norm expr
