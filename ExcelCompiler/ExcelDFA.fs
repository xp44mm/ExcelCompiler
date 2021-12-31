module ExcelCompiler.ExcelDFA
let nextStates = [|0u,[|"%",23u;"&",23u;"(",7u;")",23u;"*",23u;"+",23u;",",9u;"-",23u;"/",23u;"<",23u;"<=",23u;"<>",23u;"=",23u;">",23u;">=",23u;"APOSTROPHE",12u;"DOLLAR",18u;"ERROR",23u;"FALSE",23u;"ID",5u;"INTEGER",20u;"NEGATIVE",1u;"NUMBER",23u;"POSITIVE",1u;"QUOTE",23u;"TRUE",23u;"[",23u;"]",23u;"^",23u;"{",23u;"}",23u|];1u,[|"INTEGER",2u;"NUMBER",4u|];2u,[|":",3u|];5u,[|"!",17u;"(",6u;":",14u|];7u,[|",",8u|];9u,[|")",11u;",",10u|];12u,[|"!",17u;":",13u|];13u,[|"APOSTROPHE",15u;"ID",15u|];14u,[|"APOSTROPHE",15u;"DOLLAR",22u;"ID",16u;"INTEGER",22u|];15u,[|"!",17u|];16u,[|"!",17u|];17u,[|"DOLLAR",18u;"ID",18u;"INTEGER",19u|];18u,[|":",21u|];19u,[|":",21u|];20u,[|":",21u|];21u,[|"DOLLAR",22u;"ID",22u;"INTEGER",22u|]|]
let lexemesFromFinal = [|3u,[|1u|];6u,[|5u|];8u,[|7u|];10u,[|9u|];11u,[|9u|]|]
let universalFinals = [|1u;2u;3u;4u;5u;6u;7u;8u;9u;10u;11u;16u;18u;20u;22u;23u|]
let indicesFromFinal = [|1u,8;2u,1;3u,0;4u,1;5u,6;6u,2;7u,8;8u,3;9u,8;10u,4;11u,5;16u,6;18u,6;20u,7;22u,6;23u,8|]
let header = "open ExcelCompiler.ExcelTokenUtils\r\ntype token = ExcelToken"
let semantics = [|"lexbuf";"signNumber lexbuf";"[functionFromId lexbuf.Head]";"appendNA lexbuf";"appendNA lexbuf";"appendNA lexbuf";"[REFERENCE(getRange lexbuf)]";"[numberFromInteger lexbuf.Head]";"lexbuf"|]
open ExcelCompiler.ExcelTokenUtils
type token = ExcelToken
let mappers = [|
    fun (lexbuf:token list) ->
        lexbuf
    fun (lexbuf:token list) ->
        signNumber lexbuf
    fun (lexbuf:token list) ->
        [functionFromId lexbuf.Head]
    fun (lexbuf:token list) ->
        appendNA lexbuf
    fun (lexbuf:token list) ->
        appendNA lexbuf
    fun (lexbuf:token list) ->
        appendNA lexbuf
    fun (lexbuf:token list) ->
        [REFERENCE(getRange lexbuf)]
    fun (lexbuf:token list) ->
        [numberFromInteger lexbuf.Head]
    fun (lexbuf:token list) ->
        lexbuf
|]
let finalMappers =
    indicesFromFinal
    |> Array.map(fun(fnl, i) -> fnl,mappers.[i])
open FslexFsyacc.Runtime
let analyzer = Analyzer(nextStates, lexemesFromFinal, universalFinals, finalMappers)
let analyze (tokens:seq<_>) = 
    analyzer.analyze(tokens,getTag)