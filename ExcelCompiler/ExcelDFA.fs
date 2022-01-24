module ExcelCompiler.ExcelDFA
let header = "open ExcelCompiler.ExcelTokenUtils\r\ntype token = ExcelToken"
let nextStates = [|0u,[|"%",23u;"&",23u;"(",7u;")",23u;"*",23u;"+",23u;",",9u;"-",23u;"/",23u;"<",23u;"<=",23u;"<>",23u;"=",23u;">",23u;">=",23u;"APOSTROPHE",12u;"DOLLAR",18u;"ERROR",23u;"FALSE",23u;"ID",5u;"INTEGER",20u;"NEGATIVE",1u;"NUMBER",23u;"POSITIVE",1u;"QUOTE",23u;"TRUE",23u;"[",23u;"]",23u;"^",23u;"{",23u;"}",23u|];1u,[|"INTEGER",2u;"NUMBER",4u|];2u,[|":",3u|];5u,[|"!",17u;"(",6u;":",14u|];7u,[|",",8u|];9u,[|")",11u;",",10u|];12u,[|"!",17u;":",13u|];13u,[|"APOSTROPHE",15u;"ID",15u|];14u,[|"APOSTROPHE",15u;"DOLLAR",22u;"ID",16u;"INTEGER",22u|];15u,[|"!",17u|];16u,[|"!",17u|];17u,[|"DOLLAR",18u;"ID",18u;"INTEGER",19u|];18u,[|":",21u|];19u,[|":",21u|];20u,[|":",21u|];21u,[|"DOLLAR",22u;"ID",22u;"INTEGER",22u|]|]
let rules:(uint32[]*uint32[]*string)[] = [|[|3u|],[|1u|],"lexbuf";[|2u;4u|],[||],"signNumber lexbuf";[|6u|],[|5u|],"[functionFromId lexbuf.Head]";[|8u|],[|7u|],"appendNA lexbuf";[|10u|],[|9u|],"appendNA lexbuf";[|11u|],[|9u|],"appendNA lexbuf";[|5u;16u;18u;22u|],[||],"[REFERENCE(getRange lexbuf)]";[|20u|],[||],"[numberFromInteger lexbuf.Head]";[|1u;7u;9u;23u|],[||],"lexbuf"|]
open ExcelCompiler.ExcelTokenUtils
type token = ExcelToken
let fxRules:(uint32[]*uint32[]*_)[] = [|
    [|3u|],[|1u|],fun (lexbuf:token list) ->
        lexbuf
    [|2u;4u|],[||],fun (lexbuf:token list) ->
        signNumber lexbuf
    [|6u|],[|5u|],fun (lexbuf:token list) ->
        [functionFromId lexbuf.Head]
    [|8u|],[|7u|],fun (lexbuf:token list) ->
        appendNA lexbuf
    [|10u|],[|9u|],fun (lexbuf:token list) ->
        appendNA lexbuf
    [|11u|],[|9u|],fun (lexbuf:token list) ->
        appendNA lexbuf
    [|5u;16u;18u;22u|],[||],fun (lexbuf:token list) ->
        [REFERENCE(getRange lexbuf)]
    [|20u|],[||],fun (lexbuf:token list) ->
        [numberFromInteger lexbuf.Head]
    [|1u;7u;9u;23u|],[||],fun (lexbuf:token list) ->
        lexbuf
|]
open FslexFsyacc.Runtime
let analyzer = Analyzer(nextStates, fxRules)
let analyze (tokens:seq<_>) = 
    analyzer.analyze(tokens,getTag)