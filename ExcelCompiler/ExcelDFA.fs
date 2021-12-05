module ExcelCompiler.ExcelDFA
let nextStates = Map [0u,Map ["%",23u;"&",23u;"(",7u;")",23u;"*",23u;"+",23u;",",9u;"-",23u;"/",23u;"<",23u;"<=",23u;"<>",23u;"=",23u;">",23u;">=",23u;"APOSTROPHE",12u;"DOLLAR",18u;"ERROR",23u;"FALSE",23u;"ID",5u;"INTEGER",20u;"NEGATIVE",1u;"NUMBER",23u;"POSITIVE",1u;"QUOTE",23u;"TRUE",23u;"[",23u;"]",23u;"^",23u;"{",23u;"}",23u];1u,Map ["INTEGER",2u;"NUMBER",4u];2u,Map [":",3u];5u,Map ["!",17u;"(",6u;":",14u];7u,Map [",",8u];9u,Map [")",11u;",",10u];12u,Map ["!",17u;":",13u];13u,Map ["APOSTROPHE",15u;"ID",15u];14u,Map ["APOSTROPHE",15u;"DOLLAR",22u;"ID",16u;"INTEGER",22u];15u,Map ["!",17u];16u,Map ["!",17u];17u,Map ["DOLLAR",18u;"ID",18u;"INTEGER",19u];18u,Map [":",21u];19u,Map [":",21u];20u,Map [":",21u];21u,Map ["DOLLAR",22u;"ID",22u;"INTEGER",22u]]
let lexemesFromFinal = Map [3u,set [1u];6u,set [5u];8u,set [7u];10u,set [9u];11u,set [9u]]
let universalFinals = set [1u;2u;3u;4u;5u;6u;7u;8u;9u;10u;11u;16u;18u;20u;22u;23u]
let indicesFromFinal = Map [1u,8;2u,1;3u,0;4u,1;5u,6;6u,2;7u,8;8u,3;9u,8;10u,4;11u,5;16u,6;18u,6;20u,7;22u,6;23u,8]
let header = "open ExcelCompiler.ExcelTokenUtils"
let semantics = ["lexbuf";"signNumber lexbuf";"[functionFromId lexbuf.Head]";"appendNA lexbuf";"appendNA lexbuf";"appendNA lexbuf";"[REFERENCE(getRange lexbuf)]";"[numberFromInteger lexbuf.Head]";"lexbuf"]
open ExcelCompiler.ExcelTokenUtils
let finalMappers = Map [
    1u, fun (lexbuf:_ list) ->
        lexbuf
    2u, fun (lexbuf:_ list) ->
        signNumber lexbuf
    3u, fun (lexbuf:_ list) ->
        lexbuf
    4u, fun (lexbuf:_ list) ->
        signNumber lexbuf
    5u, fun (lexbuf:_ list) ->
        [REFERENCE(getRange lexbuf)]
    6u, fun (lexbuf:_ list) ->
        [functionFromId lexbuf.Head]
    7u, fun (lexbuf:_ list) ->
        lexbuf
    8u, fun (lexbuf:_ list) ->
        appendNA lexbuf
    9u, fun (lexbuf:_ list) ->
        lexbuf
    10u, fun (lexbuf:_ list) ->
        appendNA lexbuf
    11u, fun (lexbuf:_ list) ->
        appendNA lexbuf
    16u, fun (lexbuf:_ list) ->
        [REFERENCE(getRange lexbuf)]
    18u, fun (lexbuf:_ list) ->
        [REFERENCE(getRange lexbuf)]
    20u, fun (lexbuf:_ list) ->
        [numberFromInteger lexbuf.Head]
    22u, fun (lexbuf:_ list) ->
        [REFERENCE(getRange lexbuf)]
    23u, fun (lexbuf:_ list) ->
        lexbuf
]
open FslexFsyacc.Runtime
let analyzer = Analyzer(nextStates, lexemesFromFinal, universalFinals, finalMappers)
let analyze (tokens:seq<_>) = 
    analyzer.analyze(tokens,getTag)