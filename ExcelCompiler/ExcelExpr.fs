namespace ExcelCompiler

type ExcelExpr =
    | Func     of string * ExcelExpr option list
    | Reference of string list * string list
    | Number   of string
    | False | True
    | Quote    of string //双引号，字符串字面量
    | Eq       of ExcelExpr * ExcelExpr
    | Gt       of ExcelExpr * ExcelExpr
    | Ge       of ExcelExpr * ExcelExpr
    | Lt       of ExcelExpr * ExcelExpr
    | Le       of ExcelExpr * ExcelExpr
    | Ne       of ExcelExpr * ExcelExpr
    | Concat   of ExcelExpr * ExcelExpr
    | Add      of ExcelExpr * ExcelExpr
    | Sub      of ExcelExpr * ExcelExpr
    | Mul      of ExcelExpr * ExcelExpr
    | Div      of ExcelExpr * ExcelExpr
    | Pow      of ExcelExpr * ExcelExpr
    | Percent  of ExcelExpr
    | Positive of ExcelExpr
    | Negative of ExcelExpr

