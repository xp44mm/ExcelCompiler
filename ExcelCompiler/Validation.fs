module ExcelCompiler.Validation

open System.Text.RegularExpressions

let exprLasts = 
    set ["%";")";"NUMBER";"QUOTE";"REFERENCE"]

let exprFollows = 
    set ["$";"%";"&";")";"*";"+";",";"-";"/";"<";"<=";"<>";"=";">";">=";"^"]

///找出不支持情况，比如：中括号，大括号，ERROR，外部引用，逗号，空格
//("Sheet1","$A$1"),"='C:\\Users\\cuishengli\\Desktop\\[工作簿1.xlsx]Sheet1'!$A$1";
//("Sheet1","$A$2"),"=[工作簿1.xlsx]Sheet1!$D$15";
//("Tower","$M$36"),"='C:\\Program Files\\Microsoft Office\\root\\Office16\\LIBRARY\\Analysis\\ATPVBAEN.XLA'!mround(C36,0.25)";

///不支持，中括号，大括号，外部链接
let rec message (tokens: ExcelToken list) =
    match tokens with
    | LBRACE :: _ ->
        "大括号"
    | LBRACKET :: _ ->
        "中括号"
    | ERROR _ :: _ ->
        "ERROR"
    | EXCLAM :: ID _ :: LPAREN :: _ -> 
        "外部函数"
    | (APOSTROPHE s ) :: _ when Regex.IsMatch(s, @"\\\[.+\]") -> 
        "外部引用文件"

    | _ :: tail -> message tail //前进一个继续找

    | [] -> ""


    //| x::y::tail -> 
    //    let xx = getTag x
    //    let yy = getTag y
    //    if exprLasts.Contains xx && not(exprFollows.Contains yy) then
    //        true
    //    else
    //        deprecate (y::tail)


