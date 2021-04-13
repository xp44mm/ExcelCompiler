module ExcelCompiler.NameParser

open ExcelCompiler

///将名称的名称属性`name.Name`分解：如sheet1!x分解为"sheet1","x"；而x对齐为"","x"
let split (nameName:string) =
    let lexemes = ExcelToken.tokenize nameName |> List.ofSeq
    match lexemes with
    | [(APOSTROPHE ws|ID ws);EXCLAM;ID nm] -> ws,nm
    | [ID nm] -> "", nm
    | never -> failwithf "%A" never


