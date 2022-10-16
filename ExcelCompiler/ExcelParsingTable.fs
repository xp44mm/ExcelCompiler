module ExcelCompiler.ExcelParsingTable
let actions = [["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",1];["",0;"%",38;"&",39;"*",40;"+",41;"-",42;"/",43;"<",44;"<=",45;"<>",46;"=",47;">",48;">=",49;"^",50];["%",38;"&",39;")",-3;"*",40;"+",41;",",51;"-",42;"/",43;"<",44;"<=",45;"<>",46;"=",47;">",48;">=",49;"^",50;"{\",\"+}",6];["%",38;"&",39;")",-5;"*",40;"+",41;",",51;"-",42;"/",43;"<",44;"<=",45;"<>",46;"=",47;">",48;">=",49;"^",50;"{\",\"+}",6];["%",38;"&",39;")",-7;"*",40;"+",41;",",51;"-",42;"/",43;"<",44;"<=",45;"<>",46;"=",47;">",48;">=",49;"^",50;"{\",\"+}",6];["%",38;"&",39;")",-9;"*",40;"+",41;",",51;"-",42;"/",43;"<",44;"<=",45;"<>",46;"=",47;">",48;">=",49;"^",50;"{\",\"+}",6];["(",-1;")",-1;",",52;"FALSE",-1;"FUNCTION",-1;"NEGATIVE",-1;"NUMBER",-1;"POSITIVE",-1;"QUOTE",-1;"REFERENCE",-1;"TRUE",-1];["(",10;")",-4;",",52;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"argument",53;"expr",3;"{argument+}",8];["(",10;")",-6;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"argument",54;"expr",4];["(",10;")",-8;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"argument",54;"expr",5];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",11];["%",38;"&",39;")",12;"*",40;"+",41;"-",42;"/",43;"<",44;"<=",45;"<>",46;"=",47;">",48;">=",49;"^",50];["",-10;"%",-10;"&",-10;")",-10;"*",-10;"+",-10;",",-10;"-",-10;"/",-10;"<",-10;"<=",-10;"<>",-10;"=",-10;">",-10;">=",-10;"^",-10];["",-11;"%",-11;"&",-11;")",-11;"*",-11;"+",-11;",",-11;"-",-11;"/",-11;"<",-11;"<=",-11;"<>",-11;"=",-11;">",-11;">=",-11;"^",-11];["(",15];["(",10;")",-2;",",51;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"argument",53;"arguments",16;"expr",2;"{\",\"+}",7;"{argument+}",9];[")",17];["",-12;"%",-12;"&",-12;")",-12;"*",-12;"+",-12;",",-12;"-",-12;"/",-12;"<",-12;"<=",-12;"<>",-12;"=",-12;">",-12;">=",-12;"^",-12];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",19];["",-13;"%",-13;"&",-13;")",-13;"*",-13;"+",-13;",",-13;"-",-13;"/",-13;"<",-13;"<=",-13;"<>",-13;"=",-13;">",-13;">=",-13;"^",-13];["",-14;"%",-14;"&",-14;")",-14;"*",-14;"+",-14;",",-14;"-",-14;"/",-14;"<",-14;"<=",-14;"<>",-14;"=",-14;">",-14;">=",-14;"^",-14];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",22];["",-15;"%",-15;"&",-15;")",-15;"*",-15;"+",-15;",",-15;"-",-15;"/",-15;"<",-15;"<=",-15;"<>",-15;"=",-15;">",-15;">=",-15;"^",-15];["",-16;"%",-16;"&",-16;")",-16;"*",-16;"+",-16;",",-16;"-",-16;"/",-16;"<",-16;"<=",-16;"<>",-16;"=",-16;">",-16;">=",-16;"^",-16];["",-17;"%",-17;"&",-17;")",-17;"*",-17;"+",-17;",",-17;"-",-17;"/",-17;"<",-17;"<=",-17;"<>",-17;"=",-17;">",-17;">=",-17;"^",-17];["",-18;"%",-18;"&",-18;")",-18;"*",-18;"+",-18;",",-18;"-",-18;"/",-18;"<",-18;"<=",-18;"<>",-18;"=",-18;">",-18;">=",-18;"^",-18];["",-20;"%",38;"&",-20;")",-20;"*",40;"+",41;",",-20;"-",42;"/",43;"<",-20;"<=",-20;"<>",-20;"=",-20;">",-20;">=",-20;"^",50];["",-21;"%",38;"&",-21;")",-21;"*",-21;"+",-21;",",-21;"-",-21;"/",-21;"<",-21;"<=",-21;"<>",-21;"=",-21;">",-21;">=",-21;"^",50];["",-22;"%",38;"&",-22;")",-22;"*",40;"+",-22;",",-22;"-",-22;"/",43;"<",-22;"<=",-22;"<>",-22;"=",-22;">",-22;">=",-22;"^",50];["",-23;"%",38;"&",-23;")",-23;"*",40;"+",-23;",",-23;"-",-23;"/",43;"<",-23;"<=",-23;"<>",-23;"=",-23;">",-23;">=",-23;"^",50];["",-24;"%",38;"&",-24;")",-24;"*",-24;"+",-24;",",-24;"-",-24;"/",-24;"<",-24;"<=",-24;"<>",-24;"=",-24;">",-24;">=",-24;"^",50];["",-25;"%",38;"&",39;")",-25;"*",40;"+",41;",",-25;"-",42;"/",43;"^",50];["",-26;"%",38;"&",39;")",-26;"*",40;"+",41;",",-26;"-",42;"/",43;"^",50];["",-27;"%",38;"&",39;")",-27;"*",40;"+",41;",",-27;"-",42;"/",43;"^",50];["",-28;"%",38;"&",39;")",-28;"*",40;"+",41;",",-28;"-",42;"/",43;"^",50];["",-29;"%",38;"&",39;")",-29;"*",40;"+",41;",",-29;"-",42;"/",43;"^",50];["",-30;"%",38;"&",39;")",-30;"*",40;"+",41;",",-30;"-",42;"/",43;"^",50];["",-31;"%",38;"&",-31;")",-31;"*",-31;"+",-31;",",-31;"-",-31;"/",-31;"<",-31;"<=",-31;"<>",-31;"=",-31;">",-31;">=",-31;"^",-31];["",-19;"%",-19;"&",-19;")",-19;"*",-19;"+",-19;",",-19;"-",-19;"/",-19;"<",-19;"<=",-19;"<>",-19;"=",-19;">",-19;">=",-19;"^",-19];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",26];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",27];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",28];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",29];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",30];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",31];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",32];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",33];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",34];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",35];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",36];["(",10;"FALSE",13;"FUNCTION",14;"NEGATIVE",18;"NUMBER",20;"POSITIVE",21;"QUOTE",23;"REFERENCE",24;"TRUE",25;"expr",37];["(",-32;")",-32;",",-32;"FALSE",-32;"FUNCTION",-32;"NEGATIVE",-32;"NUMBER",-32;"POSITIVE",-32;"QUOTE",-32;"REFERENCE",-32;"TRUE",-32];["(",-33;")",-33;",",-33;"FALSE",-33;"FUNCTION",-33;"NEGATIVE",-33;"NUMBER",-33;"POSITIVE",-33;"QUOTE",-33;"REFERENCE",-33;"TRUE",-33];["(",-34;")",-34;"FALSE",-34;"FUNCTION",-34;"NEGATIVE",-34;"NUMBER",-34;"POSITIVE",-34;"QUOTE",-34;"REFERENCE",-34;"TRUE",-34];["(",-35;")",-35;"FALSE",-35;"FUNCTION",-35;"NEGATIVE",-35;"NUMBER",-35;"POSITIVE",-35;"QUOTE",-35;"REFERENCE",-35;"TRUE",-35]]
let closures = [[0,0,[];-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[0,1,[""];-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-25,1,[];-26,1,[];-27,1,[];-28,1,[];-29,1,[];-30,1,[];-31,1,[]];[-1,1,[];-3,1,[")"];-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-25,1,[];-26,1,[];-27,1,[];-28,1,[];-29,1,[];-30,1,[];-31,1,[];-32,0,[];-33,0,[]];[-1,1,[];-5,2,[")"];-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-25,1,[];-26,1,[];-27,1,[];-28,1,[];-29,1,[];-30,1,[];-31,1,[];-32,0,[];-33,0,[]];[-1,1,[];-7,3,[")"];-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-25,1,[];-26,1,[];-27,1,[];-28,1,[];-29,1,[];-30,1,[];-31,1,[];-32,0,[];-33,0,[]];[-1,1,[];-9,2,[")"];-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-25,1,[];-26,1,[];-27,1,[];-28,1,[];-29,1,[];-30,1,[];-31,1,[];-32,0,[];-33,0,[]];[-1,2,["(";")";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE"];-33,1,[]];[-1,0,[];-4,1,[")"];-5,1,[];-6,1,[];-7,1,[];-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[];-33,1,[];-34,0,[];-35,0,[]];[-1,0,[];-6,2,[")"];-7,2,[];-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[];-35,1,[]];[-1,0,[];-8,1,[")"];-9,1,[];-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[];-35,1,[]];[-10,0,[];-10,1,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,2,[];-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-25,1,[];-26,1,[];-27,1,[];-28,1,[];-29,1,[];-30,1,[];-31,1,[]];[-10,3,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-11,1,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-12,1,[]];[-1,0,[];-2,0,[")"];-3,0,[];-4,0,[];-5,0,[];-6,0,[];-7,0,[];-8,0,[];-9,0,[];-10,0,[];-11,0,[];-12,0,[];-12,2,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[];-32,0,[];-33,0,[];-34,0,[];-35,0,[]];[-12,3,[]];[-12,4,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-13,1,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-13,2,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-14,1,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-15,1,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-15,2,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-16,1,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-17,1,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-18,1,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-19,1,[];-20,3,["";"&";")";",";"<";"<=";"<>";"=";">";">="];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-31,1,[]];[-19,1,[];-21,3,["";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">="];-31,1,[]];[-19,1,[];-21,1,[];-22,3,["";"&";")";"+";",";"-";"<";"<=";"<>";"=";">";">="];-24,1,[];-31,1,[]];[-19,1,[];-21,1,[];-23,3,["";"&";")";"+";",";"-";"<";"<=";"<>";"=";">";">="];-24,1,[];-31,1,[]];[-19,1,[];-24,3,["";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">="];-31,1,[]];[-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-25,3,["";")";","];-31,1,[]];[-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-26,3,["";")";","];-31,1,[]];[-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-27,3,["";")";","];-31,1,[]];[-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-28,3,["";")";","];-31,1,[]];[-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-29,3,["";")";","];-31,1,[]];[-19,1,[];-20,1,[];-21,1,[];-22,1,[];-23,1,[];-24,1,[];-30,3,["";")";","];-31,1,[]];[-19,1,[];-31,3,["";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-19,2,["";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-20,2,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-21,2,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-22,2,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-23,2,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-24,2,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-25,2,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-26,2,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-27,2,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-28,2,[];-29,0,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-29,2,[];-30,0,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-30,2,[];-31,0,[]];[-10,0,[];-11,0,[];-12,0,[];-13,0,[];-14,0,[];-15,0,[];-16,0,[];-17,0,[];-18,0,[];-19,0,[];-20,0,[];-21,0,[];-22,0,[];-23,0,[];-24,0,[];-25,0,[];-26,0,[];-27,0,[];-28,0,[];-29,0,[];-30,0,[];-31,0,[];-31,2,[]];[-32,1,["(";")";",";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE"]];[-33,2,["(";")";",";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE"]];[-34,1,["(";")";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE"]];[-35,2,["(";")";"FALSE";"FUNCTION";"NEGATIVE";"NUMBER";"POSITIVE";"QUOTE";"REFERENCE";"TRUE"]]]
open ExcelCompiler.ExcelExprUtils
let rules:(string list*(obj list->obj))list = [
    ["expr";"FUNCTION";"(";"arguments";")"],fun(ss:obj list)->
        let s0 = unbox<string> ss.[0]
        let s2 = unbox<ExcelExpr option list> ss.[2]
        let result:ExcelExpr =
            Func(s0, s2)
        box result
    ["expr";"REFERENCE"],fun(ss:obj list)->
        let s0 = unbox<string list*string list> ss.[0]
        let result:ExcelExpr =
            Reference s0
        box result
    ["expr";"NUMBER"],fun(ss:obj list)->
        let s0 = unbox<string> ss.[0]
        let result:ExcelExpr =
            Number s0
        box result
    ["expr";"QUOTE"],fun(ss:obj list)->
        let s0 = unbox<string> ss.[0]
        let result:ExcelExpr =
            Quote s0
        box result
    ["expr";"FALSE"],fun(ss:obj list)->
        let result:ExcelExpr =
            False
        box result
    ["expr";"TRUE"],fun(ss:obj list)->
        let result:ExcelExpr =
            True
        box result
    ["expr";"(";"expr";")"],fun(ss:obj list)->
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr =
            s1
        box result
    ["expr";"expr";"=";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Eq(s0,s2)
        box result
    ["expr";"expr";"<";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Lt(s0,s2)
        box result
    ["expr";"expr";"<=";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Le(s0,s2)
        box result
    ["expr";"expr";">";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Gt(s0,s2)
        box result
    ["expr";"expr";">=";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Ge(s0,s2)
        box result
    ["expr";"expr";"<>";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Ne(s0,s2)
        box result
    ["expr";"expr";"&";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Concat(s0,s2)
        box result
    ["expr";"expr";"+";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Add(s0,s2)
        box result
    ["expr";"expr";"-";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Sub(s0,s2)
        box result
    ["expr";"expr";"*";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Mul(s0,s2)
        box result
    ["expr";"expr";"/";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Div(s0,s2)
        box result
    ["expr";"expr";"^";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr =
            Pow(s0,s2)
        box result
    ["expr";"expr";"%"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let result:ExcelExpr =
            Percent s0
        box result
    ["expr";"POSITIVE";"expr"],fun(ss:obj list)->
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr =
            Positive s1
        box result
    ["expr";"NEGATIVE";"expr"],fun(ss:obj list)->
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr =
            Negative s1
        box result
    ["arguments"],fun(ss:obj list)->
        let result:ExcelExpr option list =
            []
        box result
    ["arguments";"expr"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let result:ExcelExpr option list =
            [Some s0]
        box result
    ["arguments";"{argument+}"],fun(ss:obj list)->
        let s0 = unbox<list<ExcelExpr*int>> ss.[0]
        let result:ExcelExpr option list =
            [
                yield! fromArgumentList s0
                yield None
            ]
        box result
    ["arguments";"{\",\"+}"],fun(ss:obj list)->
        let s0 = unbox<int> ss.[0]
        let result:ExcelExpr option list =
            fromCommas (s0+1)
        box result
    ["arguments";"{\",\"+}";"expr"],fun(ss:obj list)->
        let s0 = unbox<int> ss.[0]
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr option list =
            [
                yield! fromCommas s0
                yield Some s1
            ]
        box result
    ["arguments";"{argument+}";"expr"],fun(ss:obj list)->
        let s0 = unbox<list<ExcelExpr*int>> ss.[0]
        let s1 = unbox<ExcelExpr> ss.[1]
        let result:ExcelExpr option list =
            [
                yield! fromArgumentList s0
                yield Some s1
            ]
        box result
    ["arguments";"{\",\"+}";"{argument+}"],fun(ss:obj list)->
        let s0 = unbox<int> ss.[0]
        let s1 = unbox<list<ExcelExpr*int>> ss.[1]
        let result:ExcelExpr option list =
            [
                yield! fromCommas s0
                yield! fromArgumentList s1
                yield None
            ]
        box result
    ["arguments";"{\",\"+}";"{argument+}";"expr"],fun(ss:obj list)->
        let s0 = unbox<int> ss.[0]
        let s1 = unbox<list<ExcelExpr*int>> ss.[1]
        let s2 = unbox<ExcelExpr> ss.[2]
        let result:ExcelExpr option list =
            [
                yield! fromCommas s0
                yield! fromArgumentList s1
                yield Some s2
            ]
        box result
    ["{argument+}";"argument"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr*int> ss.[0]
        let result:list<ExcelExpr*int> =
            [s0]
        box result
    ["{argument+}";"{argument+}";"argument"],fun(ss:obj list)->
        let s0 = unbox<list<ExcelExpr*int>> ss.[0]
        let s1 = unbox<ExcelExpr*int> ss.[1]
        let result:list<ExcelExpr*int> =
            s1::s0
        box result
    ["argument";"expr";"{\",\"+}"],fun(ss:obj list)->
        let s0 = unbox<ExcelExpr> ss.[0]
        let s1 = unbox<int> ss.[1]
        let result:ExcelExpr*int =
            s0,s1
        box result
    ["{\",\"+}";","],fun(ss:obj list)->
        let result:int =
            1
        box result
    ["{\",\"+}";"{\",\"+}";","],fun(ss:obj list)->
        let s0 = unbox<int> ss.[0]
        let result:int =
            s0 + 1
        box result
]
let unboxRoot =
    unbox<ExcelExpr>