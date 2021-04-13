module ExcelCompiler.ExcelExprTranslator

open FSharpCompiler.Parsing

let rec translateExpr = function
    | Interior("expr",[Terminal(FUNCTION nm);Terminal LPAREN;arguments;Terminal RPAREN]) ->
        Func(nm, translateArguments arguments |> List.rev)
    | Interior("expr",[Terminal(REFERENCE(ws,rg))]) ->
        Reference(ws,rg)
    | Interior("expr",[Terminal(NUMBER n)]) ->
        Number n
    | Interior("expr",[Terminal(QUOTE n)]) ->
        Quote n
    | Interior("expr",[Terminal FALSE]) ->
        False
    | Interior("expr",[Terminal TRUE]) ->
        True
    | Interior("expr",[Terminal LPAREN;e;Terminal RPAREN]) ->
        translateExpr e
    | Interior("expr",[e1;Terminal EQ;e2]) ->
        Eq(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal GT;e2]) ->
        Gt(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal GE;e2]) ->
        Ge(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal LT;e2]) ->
        Lt(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal LE;e2]) ->
        Le(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal NE;e2]) ->
        Ne(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal AMPERSAND;e2]) ->
        Concat(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal ADD;e2]) ->
        Add(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal SUB;e2]) ->
        Sub(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal MUL;e2]) ->
        Mul(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal DIV;e2]) ->
        Div(translateExpr e1,translateExpr e2)
    | Interior("expr",[e1;Terminal CARET;e2]) ->
        Pow(translateExpr e1,translateExpr e2)
    | Interior("expr",[e;Terminal PERCENT]) ->
        Percent(translateExpr e)
    | Interior("expr",[Terminal POSITIVE;e]) ->
        Positive(translateExpr e)
    | Interior("expr",[Terminal NEGATIVE;e]) ->
        Negative(translateExpr e)
    | never -> failwithf "%A" <| never.firstLevel()

and translateArguments = function
    | Interior("arguments",[]) ->
        []
    | Interior("arguments",[e]) ->
        [translateExpr e]
    | Interior("arguments",[ls;_;e]) ->
        translateExpr e :: translateArguments ls
    | never -> failwithf "%A" <| never.firstLevel()

let translateFormula = function
| Interior("formula",[Terminal EQ;e]) -> translateExpr e
| never -> failwithf "%A" <| never.firstLevel()

