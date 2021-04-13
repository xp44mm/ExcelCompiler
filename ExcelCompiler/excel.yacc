formula : "=" expr
        ;
expr    : FUNCTION "(" arguments ")"
        | REFERENCE
        | NUMBER
        | QUOTE
        | FALSE
        | TRUE
        | "(" expr ")"
        | expr "="  expr
        | expr "<"  expr
        | expr "<=" expr
        | expr ">"  expr
        | expr ">=" expr
        | expr "<>" expr
        | expr "&" expr
        | expr "+" expr
        | expr "-" expr
        | expr "*" expr
        | expr "/" expr
        | expr "^" expr
        | expr "%"
        | POSITIVE expr
        | NEGATIVE expr
        ;
arguments : /* empty */
          | expr
          | arguments "," expr
          ;

%%

%nonassoc "<" "<=" "<>" "=" ">" ">="
%left "&"
%left "+" "-"
%left "*" "/"
%left "^"
%left "%"
%right POSITIVE NEGATIVE

