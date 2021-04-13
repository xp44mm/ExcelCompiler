module ExcelCompiler.ExcelTokenNormalizer

open FSharpCompiler.Analyzing

let normalizeMapping = function
| 0, [unary] -> [unary] // 不是负数，保持不变
| 1, [unary; num] -> [NUMBER (unary.lexeme + num.lexeme)] // 负数
| 2, [ID x] -> [FUNCTION x] // function
| (3|4|5), [x] -> [x;FUNCTION "NA";LPAREN;RPAREN] // 补齐逗号和小括号之间省略的参数
| 6, ls -> // 名称，范围
    match ls with
    | [x] -> 
        ([],[x.lexeme])
    | [x;COLON;y] -> 
        ([],[x.lexeme;y.lexeme])
    | [p;EXCLAM;x] -> 
        ([p.lexeme],[x.lexeme])
    | [p;EXCLAM;x;COLON;y] -> 
        ([p.lexeme],[x.lexeme;y.lexeme])
    | [p;COLON;q;EXCLAM;x] -> 
        ([p.lexeme;q.lexeme],[x.lexeme])
    | [p;COLON;q;EXCLAM;x;COLON;y] -> 
        ([p.lexeme;q.lexeme],[x.lexeme;y.lexeme])
    | never -> failwith ""
    |> REFERENCE
    |> List.singleton
| 7, [INTEGER x] -> [NUMBER x]
| _, ls -> ls // 其他不变

let private analyzer = LexicalAnalyzer(ExcelDFA.dtran, ExcelDFA.finalLexemes)

let normalize (tokens:seq<ExcelToken>) =
    analyzer.split(tokens,fun tok -> tok.tag)
    |> Seq.collect(normalizeMapping)
