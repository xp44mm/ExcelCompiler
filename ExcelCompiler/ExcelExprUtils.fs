module ExcelCompiler.ExcelExprUtils

let fromCommas (count:int) =
    List.replicate count None

let fromArgument (expr:ExcelExpr,count:int) =
    Some expr::fromCommas (count-1)

let fromArgumentList (ls:(ExcelExpr*int) list) =
    ls
    |> List.rev
    |> List.collect(fromArgument)
    
