namespace ExcelCompiler

type ExcelToken =
    | NUMBER of string
    | INTEGER of string
    | QUOTE of string // "
    | APOSTROPHE of string // '
    | DOLLAR of string
    | ID of string
    | FUNCTION of string // 后面跟着参数表
    | ERROR of string
    | FALSE
    | TRUE
    | EXCLAM
    | COLON
    | COMMA
    | LPAREN
    | RPAREN
    | EQ
    | NE
    | LT
    | LE
    | GT
    | GE
    | AMPERSAND
    | ADD
    | SUB
    | MUL
    | DIV
    | CARET
    | PERCENT
    | POSITIVE
    | NEGATIVE
    | LBRACE
    | RBRACE
    | LBRACKET
    | RBRACKET

    /// 整理一次
    | REFERENCE of string list * string list // range or name or bool

