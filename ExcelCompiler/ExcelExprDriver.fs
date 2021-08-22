module ExcelCompiler.ExcelExprDriver

let parsingTree (normTokens:seq<ExcelToken>) = ExcelParsingTable.pconfig.parse(normTokens, fun tok -> tok.tag)

let tokensToExpr (normTokens:seq<ExcelToken>) = 
    normTokens
    |> parsingTree
    |> ExcelExprTranslator.translateFormula 
    
///解析公式
let parse(formula:string) =
    formula
    |> ExcelToken.tokenize
    |> ExcelTokenNormalizer.normalize
    |> tokensToExpr


