module ExcelCompiler.ExcelDFA
let nextStates = [0u,["%",18u;"&",18u;"(",18u;")",18u;"*",18u;"+",18u;",",18u;"-",18u;"/",18u;"<",18u;"<=",18u;"<>",18u;"=",18u;">",18u;">=",18u;"APOSTROPHE",7u;"DOLLAR",13u;"ERROR",18u;"FALSE",18u;"ID",5u;"INTEGER",15u;"NEGATIVE",1u;"NUMBER",18u;"POSITIVE",1u;"QUOTE",18u;"TRUE",18u;"[",18u;"]",18u;"^",18u;"{",18u;"}",18u];1u,["INTEGER",2u;"NUMBER",4u];2u,[":",3u];5u,["!",12u;"(",6u;":",9u];7u,["!",12u;":",8u];8u,["APOSTROPHE",10u;"ID",10u];9u,["APOSTROPHE",10u;"DOLLAR",17u;"ID",11u;"INTEGER",17u];10u,["!",12u];11u,["!",12u];12u,["DOLLAR",13u;"ID",13u;"INTEGER",14u];13u,[":",16u];14u,[":",16u];15u,[":",16u];16u,["DOLLAR",17u;"ID",17u;"INTEGER",17u]]
open FslexFsyacc.Runtime
open ExcelCompiler.ExcelTokenUtils
type token = Position<ExcelToken>
let rules:list<uint32 list*uint32 list*(list<token>->_)> = [
    [3u],[1u],fun (lexbuf:list<_>) ->
        lexbuf.Head
    [2u;4u],[],fun (lexbuf:list<_>) ->
        signNumber lexbuf
    [6u],[5u],fun (lexbuf:list<_>) ->
        functionFromId lexbuf
    [5u;11u;13u;17u],[],fun (lexbuf:list<_>) ->
        getReference lexbuf
    [15u],[],fun (lexbuf:list<_>) ->
        numberFromInteger lexbuf
    [1u;18u],[],fun (lexbuf:list<_>) ->
        lexbuf.Head
]
let analyzer = Analyzer<_,_>(nextStates, rules)