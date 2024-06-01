module ExcelCompiler.Dir

open System.IO

let solutionPath =
    DirectoryInfo( __SOURCE_DIRECTORY__ )
        .Parent
        .FullName

let sourcePath = Path.Combine(solutionPath, @"ExcelCompiler")
