module ExcelCompiler.ExcelExprDriver

//let parsingTree (normTokens:seq<ExcelToken>) = ExcelParsingTable.pconfig.parse(normTokens, fun tok -> tok.tag)

//let tokensToExpr (normTokens:seq<ExcelToken>) = 
//    normTokens
//    |> parsingTree
//    |> ExcelExprTranslation.translate
    
///解析公式
let parse(formula:string) =
    formula
    |> ExcelToken.tokenize
    |> ExcelDFA.analyze
    |> Seq.concat
    |> ExcelParsingTable.parse
