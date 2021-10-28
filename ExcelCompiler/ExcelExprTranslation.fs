module  ExcelCompiler.ExcelExprTranslation

open FSharpCompiler.Parsing
open FSharpCompiler.Parsing.ParseTreeUtils

let rec trans_formula = function
    | [Terminal EQ;Interior("expr", expr)] ->
        let expr = trans_expr expr
        expr
    | never -> failwithf "trans_formula: %A" (List.map firstLevel never)

and trans_expr = function
    | [Terminal(FUNCTION nm);Terminal LPAREN;Interior("arguments", arguments);Terminal RPAREN] ->
        let arguments = trans_arguments arguments
        Func(nm, arguments |> List.rev)
    | [Terminal(REFERENCE (ws,rg))] ->
        Reference(ws,rg)
    | [Terminal(NUMBER number)] ->
        Number number
    | [Terminal(QUOTE quote)] ->
        Quote quote
    | [Terminal FALSE] ->
        False
    | [Terminal TRUE] ->
        True
    | [Terminal LPAREN;Interior("expr", expr);Terminal RPAREN] ->
        let expr = trans_expr expr
        expr
    | [Interior("expr", expr1);Terminal EQ;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Eq(expr1,expr2)
    | [Interior("expr", expr1);Terminal LT;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Lt(expr1,expr2)
    | [Interior("expr", expr1);Terminal LE;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Le(expr1,expr2)
    | [Interior("expr", expr1);Terminal GT;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Gt(expr1,expr2)
    | [Interior("expr", expr1);Terminal GE;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Ge(expr1,expr2)
    | [Interior("expr", expr1);Terminal NE;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Ne(expr1,expr2)
    | [Interior("expr", expr1);Terminal AMPERSAND;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Concat(expr1,expr2)
    | [Interior("expr", expr1);Terminal ADD;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Add(expr1,expr2)
    | [Interior("expr", expr1);Terminal SUB;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Sub(expr1,expr2)
    | [Interior("expr", expr1);Terminal MUL;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Mul(expr1,expr2)
    | [Interior("expr", expr1);Terminal DIV;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Div(expr1,expr2)
    | [Interior("expr", expr1);Terminal CARET;Interior("expr", expr2)] ->
        let expr1 = trans_expr expr1
        let expr2 = trans_expr expr2
        Pow(expr1,expr2)
    | [Interior("expr", expr);Terminal PERCENT] ->
        let expr = trans_expr expr
        Percent expr
    | [Terminal POSITIVE;Interior("expr", expr)] ->
        let expr = trans_expr expr
        Positive expr
    | [Terminal NEGATIVE;Interior("expr", expr)] ->
        let expr = trans_expr expr
        Negative expr
    | never -> failwithf "trans_expr: %A" (List.map firstLevel never)

and trans_arguments = function
    | [] ->
        []
    | [Interior("expr", expr)] ->
        let expr = trans_expr expr
        [expr]
    | [Interior("arguments", arguments);Terminal COMMA;Interior("expr", expr)] ->
        let arguments = trans_arguments arguments
        let expr = trans_expr expr
        expr::arguments
    | never -> failwithf "trans_arguments: %A" (List.map firstLevel never)

let translate = function
    | Interior("formula", children) -> trans_formula children
    | root -> failwithf "translate: %A" (firstLevel root)