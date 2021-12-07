# ExcelCompiler

`ExcelCompiler`是解析或序列化Excel公式的工具。

公式字符串分解为原子词素。所谓原子词素是不可再分最小语法单位。

```F#
ExcelFormulaString.tokenize: string -> seq<ExcelToken>
```

示例：

```F#
    let x = "(A1+A2)/2"
    let y = ExcelFormulaString.tokenize x |> List.ofSeq
    let z = [LPAREN;ID "A1";ADD;ID "A2";RPAREN;DIV;INTEGER "2"]
    Should.equal y z
```

公式字符串分解为词素。词素相对于原子词素进行了初步分组合并。

```F#
ExcelFormulaString.normToken: string -> seq<ExcelToken>
```

示例：

```F#
    let x = "sheet1!A2"
    let y = ExcelFormulaString.normToken x |> List.ofSeq
    let z = [REFERENCE(["sheet1"],["A2"])]
    Should.equal y z
```

公式字符串解析为表达式。

```F#
ExcelFormulaString.parseToExpr: string -> ExcelExpr
```

示例：

```F#
    let x = "(A1+A2)/2"
    let y = ExcelFormulaString.parseToExpr x
    let z = Div(Add(Reference([],["A1"]),Reference([],["A2"])),Number "2")
    Should.equal y z
```

将名称的名称属性分解为工作表名和名称。如果是工作簿名称，则工作表名称为空。

```F#
ExcelFormulaString.splitName: string -> string * string
```

示例：

```F#
    let x = "sheet1!x"
    let y = ExcelFormulaString.splitName x
    let z = "sheet1","x"
    Should.equal y z
```

找出解析器不支持的情况。

```F#
ExcelFormulaString.varifyMessage: tokens:ExcelToken list -> string
```

示例：

```F#
    let tokens = [LBRACKET;ID "工作簿1.xlsx";RBRACKET;ID "Sheet1";EXCLAM;DOLLAR "$A$1"]
    let y = ExcelFormulaString.varifyMessage tokens
    Should.equal y "中括号"
```

将Excel公式打印成F#表达式。

```F#
ExcelFormulaString.fsharpExpr: expr:ExcelExpr -> string
```

示例：

```F#
    let expr = Mul(Func("pi",[]),Number "3")
    let y = ExcelFormulaString.fsharpString expr
    Should.equal y "Math.PI*3.0"
```

词素：

```F#
type ExcelToken =
    | NUMBER of string
    | INTEGER of string
    | QUOTE of string
    | APOSTROPHE of string
    | DOLLAR of string
    | ID of string
    | FUNCTION of string
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
    | REFERENCE of string list * string list
```

表达式树：

```F#
type ExcelExpr =
    | Func     of string * ExcelExpr list
    | Reference of string list * string list
    | Number   of string
    | False | True
    | Quote    of string
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
```
