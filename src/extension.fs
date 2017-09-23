module VSCodeFable

open Fable.Core
open Fable.Import
open Fable.Import.vscode

let activate (context : vscode.ExtensionContext) =
    UserInteractions.activate context
    LanguageFeatures.activate context
    Diagnostics.activate context


