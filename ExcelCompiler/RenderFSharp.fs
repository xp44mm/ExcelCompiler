module ExcelCompiler.RenderFSharp

open System
open FSharp.Idioms

let precedences = 
    Map.ofList [
        "%",9979;"&",9939;"*",9959;"+",9949;"-",9949;"/",9959;"<",9930;"<=",9930;"<>",9930;"=",9930;">",9930;">=",9930;
        "NEGATIVE",9991;"POSITIVE",9991;"**",9969]

let getPrecOffset sym =
    let prec = precedences.[sym]
    match prec % 10 with
    | 9 -> prec+1,-1
    | 0 -> prec,0
    | 1 -> prec-1,1
    | never -> failwith ""

//打印标准型，解析的逆
let norm (expr) =
    //循环主体
    let rec loop precedence expr =
        //在二元表达式周围加括号，如果优先级低于周围的运算符
        let putparen sym e1 e2 =
            let myPrec,offset = getPrecOffset sym
            let res = 
                loop (myPrec+offset) e1 + sym + loop (myPrec-offset) e2
            //对于左结合的运算符，prec表示左侧运算符的优先级，precSym表示右侧运算符的优先级
            if  myPrec > precedence then
                res
            else
                "(" + res + ")"
                
        match expr with
        | Reference([],[id]) ->
            id.Replace("$","")
        | Reference(wss,ids) ->
            let wss = String.concat ":" wss
            let ids = ids |> Seq.map(fun id -> id.Replace("$","")) |> String.concat ":"
            [
                if String.IsNullOrEmpty wss 
                then () 
                else yield wss
                yield ids
            ] |> String.concat "!"
        | Number s ->
            if s.Contains "." then s else s + ".0"
        | Quote s -> s
        | Func (f,args) ->
            if f == "sqrt" then
                Pow(args.[0],Number "0.5")
                |> loop precedence
            elif f == "pi" then
                "Math.PI"
            else
                args 
                |> List.map (loop 0)
                |> String.concat ","
                |> sprintf "%s(%s)" (f.ToLower())
                //优先级高，一定不加括号
        | False -> "false"
        | True -> "true"

        | Eq    (e1, e2) -> (e1,e2) ||> putparen "="
        | Gt    (e1, e2) -> (e1,e2) ||> putparen ">"
        | Ge    (e1, e2) -> (e1,e2) ||> putparen ">="
        | Lt    (e1, e2) -> (e1,e2) ||> putparen "<"
        | Le    (e1, e2) -> (e1,e2) ||> putparen "<="
        | Ne    (e1, e2) -> (e1,e2) ||> putparen "<>"
        | Concat(e1, e2) //-> (e1,e2) ||> putparen "&"
        | Add   (e1, e2) -> (e1,e2) ||> putparen "+"
        | Sub   (e1, e2) -> (e1,e2) ||> putparen "-"
        | Mul   (e1, e2) -> (e1,e2) ||> putparen "*"
        | Div   (e1, e2) -> (e1,e2) ||> putparen "/"
        | Pow   (e1, e2) -> (e1,e2) ||> putparen "**"

        | Percent e -> 
            Div(e, Number "100.0")
            |> loop precedence

        | Negative e -> 
            let myPrec, offset = getPrecOffset "NEGATIVE"

            let res = "-" + loop (myPrec - offset) e
            if  myPrec > precedence then
                res
            else
                "(" + res + ")"
           
        | Positive e -> loop precedence e
    loop 0 expr
