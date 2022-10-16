module ExcelCompiler.ExcelExprCompiler

open FslexFsyacc.Runtime
    
let parser = 
    Parser<ExcelToken>(
        ExcelParsingTable.rules,
        ExcelParsingTable.actions,
        ExcelParsingTable.closures,ExcelTokenUtils.getTag,ExcelTokenUtils.getLexeme)

let parseTokens(tokens:seq<ExcelToken>) =
    tokens
    |> parser.parse
    |> ExcelParsingTable.unboxRoot

///解析公式
let parse(formula:string) =
    formula
    |> ExcelTokenUtils.tokenize
    |> ExcelDFA.analyze
    |> Seq.concat
    |> parseTokens
