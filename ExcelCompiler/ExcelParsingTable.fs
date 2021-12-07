module ExcelCompiler.ExcelParsingTable
let header = "open ExcelCompiler.ExcelTokenUtils"
let productions = Map [-25,["expr";"expr";"^";"expr"];-24,["expr";"expr";">=";"expr"];-23,["expr";"expr";">";"expr"];-22,["expr";"expr";"=";"expr"];-21,["expr";"expr";"<>";"expr"];-20,["expr";"expr";"<=";"expr"];-19,["expr";"expr";"<";"expr"];-18,["expr";"expr";"/";"expr"];-17,["expr";"expr";"-";"expr"];-16,["expr";"expr";"+";"expr"];-15,["expr";"expr";"*";"expr"];-14,["expr";"expr";"&";"expr"];-13,["expr";"expr";"%"];-12,["expr";"TRUE"];-11,["expr";"REFERENCE"];-10,["expr";"QUOTE"];-9,["expr";"POSITIVE";"expr"];-8,["expr";"NUMBER"];-7,["expr";"NEGATIVE";"expr"];-6,["expr";"FUNCTION";"(";"arguments";")"];-5,["expr";"FALSE"];-4,["expr";"(";"expr";")"];-3,["arguments";"expr"];-2,["arguments";"arguments";",";"expr"];-1,["arguments"];0,["";"expr"]]
let actions = Map [0,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",1];1,Map ["",0;"%",33;"&",34;"*",35;"+",36;"-",37;"/",38;"<",39;"<=",40;"<>",41;"=",42;">",43;">=",44;"^",45];2,Map [")",12;",",3];3,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",4];4,Map ["%",33;"&",34;")",-2;"*",35;"+",36;",",-2;"-",37;"/",38;"<",39;"<=",40;"<>",41;"=",42;">",43;">=",44;"^",45];5,Map ["%",33;"&",34;")",-3;"*",35;"+",36;",",-3;"-",37;"/",38;"<",39;"<=",40;"<>",41;"=",42;">",43;">=",44;"^",45];6,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",7];7,Map ["%",33;"&",34;")",8;"*",35;"+",36;"-",37;"/",38;"<",39;"<=",40;"<>",41;"=",42;">",43;">=",44;"^",45];8,Map ["",-4;"%",-4;"&",-4;")",-4;"*",-4;"+",-4;",",-4;"-",-4;"/",-4;"<",-4;"<=",-4;"<>",-4;"=",-4;">",-4;">=",-4;"^",-4];9,Map ["",-5;"%",-5;"&",-5;")",-5;"*",-5;"+",-5;",",-5;"-",-5;"/",-5;"<",-5;"<=",-5;"<>",-5;"=",-5;">",-5;">=",-5;"^",-5];10,Map ["(",11];11,Map ["(",6;")",-1;",",-1;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"arguments",2;"expr",5];12,Map ["",-6;"%",-6;"&",-6;")",-6;"*",-6;"+",-6;",",-6;"-",-6;"/",-6;"<",-6;"<=",-6;"<>",-6;"=",-6;">",-6;">=",-6;"^",-6];13,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",14];14,Map ["",-7;"%",-7;"&",-7;")",-7;"*",-7;"+",-7;",",-7;"-",-7;"/",-7;"<",-7;"<=",-7;"<>",-7;"=",-7;">",-7;">=",-7;"^",-7];15,Map ["",-8;"%",-8;"&",-8;")",-8;"*",-8;"+",-8;",",-8;"-",-8;"/",-8;"<",-8;"<=",-8;"<>",-8;"=",-8;">",-8;">=",-8;"^",-8];16,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",17];17,Map ["",-9;"%",-9;"&",-9;")",-9;"*",-9;"+",-9;",",-9;"-",-9;"/",-9;"<",-9;"<=",-9;"<>",-9;"=",-9;">",-9;">=",-9;"^",-9];18,Map ["",-10;"%",-10;"&",-10;")",-10;"*",-10;"+",-10;",",-10;"-",-10;"/",-10;"<",-10;"<=",-10;"<>",-10;"=",-10;">",-10;">=",-10;"^",-10];19,Map ["",-11;"%",-11;"&",-11;")",-11;"*",-11;"+",-11;",",-11;"-",-11;"/",-11;"<",-11;"<=",-11;"<>",-11;"=",-11;">",-11;">=",-11;"^",-11];20,Map ["",-12;"%",-12;"&",-12;")",-12;"*",-12;"+",-12;",",-12;"-",-12;"/",-12;"<",-12;"<=",-12;"<>",-12;"=",-12;">",-12;">=",-12;"^",-12];21,Map ["",-14;"%",33;"&",-14;")",-14;"*",35;"+",36;",",-14;"-",37;"/",38;"<",-14;"<=",-14;"<>",-14;"=",-14;">",-14;">=",-14;"^",45];22,Map ["",-15;"%",33;"&",-15;")",-15;"*",-15;"+",-15;",",-15;"-",-15;"/",-15;"<",-15;"<=",-15;"<>",-15;"=",-15;">",-15;">=",-15;"^",45];23,Map ["",-16;"%",33;"&",-16;")",-16;"*",35;"+",-16;",",-16;"-",-16;"/",38;"<",-16;"<=",-16;"<>",-16;"=",-16;">",-16;">=",-16;"^",45];24,Map ["",-17;"%",33;"&",-17;")",-17;"*",35;"+",-17;",",-17;"-",-17;"/",38;"<",-17;"<=",-17;"<>",-17;"=",-17;">",-17;">=",-17;"^",45];25,Map ["",-18;"%",33;"&",-18;")",-18;"*",-18;"+",-18;",",-18;"-",-18;"/",-18;"<",-18;"<=",-18;"<>",-18;"=",-18;">",-18;">=",-18;"^",45];26,Map ["",-19;"%",33;"&",34;")",-19;"*",35;"+",36;",",-19;"-",37;"/",38;"^",45];27,Map ["",-20;"%",33;"&",34;")",-20;"*",35;"+",36;",",-20;"-",37;"/",38;"^",45];28,Map ["",-21;"%",33;"&",34;")",-21;"*",35;"+",36;",",-21;"-",37;"/",38;"^",45];29,Map ["",-22;"%",33;"&",34;")",-22;"*",35;"+",36;",",-22;"-",37;"/",38;"^",45];30,Map ["",-23;"%",33;"&",34;")",-23;"*",35;"+",36;",",-23;"-",37;"/",38;"^",45];31,Map ["",-24;"%",33;"&",34;")",-24;"*",35;"+",36;",",-24;"-",37;"/",38;"^",45];32,Map ["",-25;"%",33;"&",-25;")",-25;"*",-25;"+",-25;",",-25;"-",-25;"/",-25;"<",-25;"<=",-25;"<>",-25;"=",-25;">",-25;">=",-25;"^",-25];33,Map ["",-13;"%",-13;"&",-13;")",-13;"*",-13;"+",-13;",",-13;"-",-13;"/",-13;"<",-13;"<=",-13;"<>",-13;"=",-13;">",-13;">=",-13;"^",-13];34,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",21];35,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",22];36,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",23];37,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",24];38,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",25];39,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",26];40,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",27];41,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",28];42,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",29];43,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",30];44,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",31];45,Map ["(",6;"FALSE",9;"FUNCTION",10;"NEGATIVE",13;"NUMBER",15;"POSITIVE",16;"QUOTE",18;"REFERENCE",19;"TRUE",20;"expr",32]]
let kernelSymbols = Map [1,"expr";2,"arguments";3,",";4,"expr";5,"expr";6,"(";7,"expr";8,")";9,"FALSE";10,"FUNCTION";11,"(";12,")";13,"NEGATIVE";14,"expr";15,"NUMBER";16,"POSITIVE";17,"expr";18,"QUOTE";19,"REFERENCE";20,"TRUE";21,"expr";22,"expr";23,"expr";24,"expr";25,"expr";26,"expr";27,"expr";28,"expr";29,"expr";30,"expr";31,"expr";32,"expr";33,"%";34,"&";35,"*";36,"+";37,"-";38,"/";39,"<";40,"<=";41,"<>";42,"=";43,">";44,">=";45,"^"]
let semantics = Map [-25,"Pow(s0,s2)";-24,"Ge(s0,s2)";-23,"Gt(s0,s2)";-22,"Eq(s0,s2)";-21,"Ne(s0,s2)";-20,"Le(s0,s2)";-19,"Lt(s0,s2)";-18,"Div(s0,s2)";-17,"Sub(s0,s2)";-16,"Add(s0,s2)";-15,"Mul(s0,s2)";-14,"Concat(s0,s2)";-13,"Percent s0";-12,"True";-11,"Reference s0";-10,"Quote s0";-9,"Positive s1";-8,"Number s0";-7,"Negative s1";-6,"Func(s0, List.rev s2)";-5,"False";-4,"s1";-3,"[s0]";-2,"s2::s0";-1,"[]"]
let declarations = ["expr","ExcelExpr";"FUNCTION","string";"NUMBER","string";"QUOTE","string";"REFERENCE","string list*string list";"arguments","ExcelExpr list"]
open ExcelCompiler.ExcelTokenUtils
let mappers:Map<int,(obj[]->obj)> = Map [
    -25,fun (ss:obj[]) ->
        // expr : expr "^" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Pow(s0,s2)
        box result
    -24,fun (ss:obj[]) ->
        // expr : expr ">=" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Ge(s0,s2)
        box result
    -23,fun (ss:obj[]) ->
        // expr : expr ">" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Gt(s0,s2)
        box result
    -22,fun (ss:obj[]) ->
        // expr : expr "=" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Eq(s0,s2)
        box result
    -21,fun (ss:obj[]) ->
        // expr : expr "<>" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Ne(s0,s2)
        box result
    -20,fun (ss:obj[]) ->
        // expr : expr "<=" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Le(s0,s2)
        box result
    -19,fun (ss:obj[]) ->
        // expr : expr "<" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Lt(s0,s2)
        box result
    -18,fun (ss:obj[]) ->
        // expr : expr "/" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Div(s0,s2)
        box result
    -17,fun (ss:obj[]) ->
        // expr : expr "-" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Sub(s0,s2)
        box result
    -16,fun (ss:obj[]) ->
        // expr : expr "+" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Add(s0,s2)
        box result
    -15,fun (ss:obj[]) ->
        // expr : expr "*" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Mul(s0,s2)
        box result
    -14,fun (ss:obj[]) ->
        // expr : expr "&" expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Concat(s0,s2)
        box result
    -13,fun (ss:obj[]) ->
        // expr : expr "%"
        let s0 = unbox<ExcelExpr> ss.[0]
        let result:ExcelExpr =
            Percent s0
        box result
    -12,fun (ss:obj[]) ->
        // expr : TRUE
        let result:ExcelExpr =
            True
        box result
    -11,fun (ss:obj[]) ->
        // expr : REFERENCE
        let s0 = unbox<string list*string list> ss.[0]
        let result:ExcelExpr =
            Reference s0
        box result
    -10,fun (ss:obj[]) ->
        // expr : QUOTE
        let s0 = unbox<string> ss.[0]
        let result:ExcelExpr =
            Quote s0
        box result
    -9,fun (ss:obj[]) ->
        // expr : POSITIVE expr
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr =
            Positive s1
        box result
    -8,fun (ss:obj[]) ->
        // expr : NUMBER
        let s0 = unbox<string> ss.[0]
        let result:ExcelExpr =
            Number s0
        box result
    -7,fun (ss:obj[]) ->
        // expr : NEGATIVE expr
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr =
            Negative s1
        box result
    -6,fun (ss:obj[]) ->
        // expr : FUNCTION "(" arguments ")"
        let s0 = unbox<string> ss.[0]
        let s2 = unbox<ExcelExpr list> ss.[2]
        let result:ExcelExpr =
            Func(s0, List.rev s2)
        box result
    -5,fun (ss:obj[]) ->
        // expr : FALSE
        let result:ExcelExpr =
            False
        box result
    -4,fun (ss:obj[]) ->
        // expr : "(" expr ")"
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr =
            s1
        box result
    -3,fun (ss:obj[]) ->
        // arguments : expr
        let s0 = unbox<ExcelExpr> ss.[0]
        let result:ExcelExpr list =
            [s0]
        box result
    -2,fun (ss:obj[]) ->
        // arguments : arguments "," expr
        let s0 = unbox<ExcelExpr list> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr list =
            s2::s0
        box result
    -1,fun (ss:obj[]) ->
        // arguments : 
        let result:ExcelExpr list =
            []
        box result]
open FslexFsyacc.Runtime
let parser = Parser(productions, actions, kernelSymbols, mappers)
let parse (tokens:seq<_>) =
    parser.parse(tokens, getTag, getLexeme)
    |> unbox<ExcelExpr>