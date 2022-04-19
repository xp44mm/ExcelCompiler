module ExcelCompiler.ExcelDFA
let header = "open ExcelCompiler.ExcelTokenUtils\r\ntype token = ExcelToken"
let nextStates = [|0u,[|"%",18u;"&",18u;"(",18u;")",18u;"*",18u;"+",18u;",",18u;"-",18u;"/",18u;"<",18u;"<=",18u;"<>",18u;"=",18u;">",18u;">=",18u;"APOSTROPHE",7u;"DOLLAR",13u;"ERROR",18u;"FALSE",18u;"ID",5u;"INTEGER",15u;"NEGATIVE",1u;"NUMBER",18u;"POSITIVE",1u;"QUOTE",18u;"TRUE",18u;"[",18u;"]",18u;"^",18u;"{",18u;"}",18u|];1u,[|"INTEGER",2u;"NUMBER",4u|];2u,[|":",3u|];5u,[|"!",12u;"(",6u;":",9u|];7u,[|"!",12u;":",8u|];8u,[|"APOSTROPHE",10u;"ID",10u|];9u,[|"APOSTROPHE",10u;"DOLLAR",17u;"ID",11u;"INTEGER",17u|];10u,[|"!",12u|];11u,[|"!",12u|];12u,[|"DOLLAR",13u;"ID",13u;"INTEGER",14u|];13u,[|":",16u|];14u,[|":",16u|];15u,[|":",16u|];16u,[|"DOLLAR",17u;"ID",17u;"INTEGER",17u|]|]
let rules:(uint32[]*uint32[]*string)[] = [|[|3u|],[|1u|],"lexbuf";[|2u;4u|],[||],"signNumber lexbuf";[|6u|],[|5u|],"[functionFromId lexbuf.Head]";[|5u;11u;13u;17u|],[||],"[REFERENCE(getRange lexbuf)]";[|15u|],[||],"[numberFromInteger lexbuf.Head]";[|1u;18u|],[||],"lexbuf"|]
open ExcelCompiler.ExcelTokenUtils
type token = ExcelToken
let fxRules:(uint32[]*uint32[]*_)[] = [|
    [|3u|],[|1u|],fun (lexbuf:token list) ->
        lexbuf
    [|2u;4u|],[||],fun (lexbuf:token list) ->
        signNumber lexbuf
    [|6u|],[|5u|],fun (lexbuf:token list) ->
        [functionFromId lexbuf.Head]
    [|5u;11u;13u;17u|],[||],fun (lexbuf:token list) ->
        [REFERENCE(getRange lexbuf)]
    [|15u|],[||],fun (lexbuf:token list) ->
        [numberFromInteger lexbuf.Head]
    [|1u;18u|],[||],fun (lexbuf:token list) ->
        lexbuf
|]
open FslexFsyacc.Runtime
let analyzer = Analyzer(nextStates, fxRules)
let analyze (tokens:seq<_>) = 
    analyzer.analyze(tokens,getTag)