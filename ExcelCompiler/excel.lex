unary   = [POSITIVE NEGATIVE]
worksheet  = [ID APOSTROPHE]
worksheets = {worksheet} (":" {worksheet})? "!"
cell       = [ DOLLAR ID INTEGER ]
cells      = {cell} ":" {cell}
range   = [ DOLLAR ID ] | {cells}
wsrange = {worksheets}? {range}

%%
{unary} / INTEGER ":"
{unary} [ INTEGER NUMBER ]
ID / "("
"(" / ","
"," / ","
"," / ")"
{wsrange}
INTEGER
NUMBER
QUOTE
ERROR
FALSE
TRUE
","
"("
")"
"="
"<>"
"<"
"<="
">"
">="
"&"
"+"
"-"
"*"
"/"
"^"
"%"
POSITIVE
NEGATIVE
"{"
"}"
"["
"]"
