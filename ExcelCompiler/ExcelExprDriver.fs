module ExcelCompiler.ExcelExprDriver

open FSharpCompiler.Parsing

let parser = 
    SyntacticParser(
        ExcelParsingTable.rules, 
        ExcelParsingTable.kernelSymbols,
        ExcelParsingTable.parsingTable)

let tokensToExpr (normTokens:seq<ExcelToken>) = 
    let parsingTree = parser.parse(normTokens, fun tok -> tok.tag)
    let expr = ExcelExprTranslator.translateFormula parsingTree
    expr

///解析公式
let parse(formula:string) =
    //let tokens =

    formula
    |> ExcelToken.tokenize
    |> ExcelTokenNormalizer.normalize
    |> tokensToExpr

    //let parsingTree = parser.parse(tokens,fun tok -> tok.tag)
    //let expr = ExcelExprTranslator.translateFormula parsingTree
    //expr

