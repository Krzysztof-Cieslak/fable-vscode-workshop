module LanguageFeatures

open Fable.Core
open Fable.Import
open Fable.Import.vscode
open Fable.Core.JsInterop

//Custom helpers that we're using in Ionide
open Ionide.VSCode.Helpers

//Using Object Expressions
let x =
    { new System.IDisposable
      with
        member x.Dispose() = ()
    }

//Fable Erased choice type
// Used to simulate TypeScripts union type like (string | int)
// It's used by huge part of VSCode API
let y = U2.Case1 "abc"

//Adding support for language features requires using functions from
// vscode.languages namespace. Implementing a feature has 2 parts -
// 1. Implementing provider interface for given feature, Providers interfaces are defined in vscode namespace
// 2. Registering the provider for the language

let createTooltipProvider () =
    { new vscode.HoverProvider
      with
        member x.provideHover (doc, pos, _) =
            let wordRange = doc.getWordRangeAtPosition(pos)
            let word = doc.getText wordRange
            let ms : MarkedString = U2.Case1 word
            Hover(U2.Case1 ms) |> U2.Case1
    }

//TASK 1: Implement and register "Symbols in file" provider (DocumentSymbolProvider)
// that let you navigate to each line starting with #

//TASK 2: Implement and register autocomplete provider (CompletionItemProvider)
// There are 2 functions to be implemented in this provider:
// 1. Returning the list of the completions
// 2. Returning additional documentation / comment for the comlpetion

// TASK 3: Implement and register CodeLens provider (CodeLensProvider)
// that will display the sum of the numbers in a line for every line starting with #
// There are 2 functions to be implemented in this provider:
// 1. Returning the list of the locations of the CodeLenses
// 2. Returning resolved content for each CodeLens

let activate (context : vscode.ExtensionContext) =
    let df = createEmpty<DocumentFilter>
    df.language <- Some "fable"
    let df' : DocumentSelector = df |> U3.Case2


    vscode.languages.registerHoverProvider(df', createTooltipProvider ())
    |> context.subscriptions.Add

    ()


// List of all providers:
// * CodeActions - the "yellow bulb" actions, implemented in Ionide with error parsing, FSharpLint hints, pattern match case generation, adding open namespaces
// * CodeLensProvider - providing code lenses, implemented in Ionide to display function signatures
// * CompletionItemProvider - autocomplete,implemented in Ionide
// * DefinitionProvider - go-to-definition, peak definition, implemented in Ionide
// * DocumentFormattingEditProvider - format document, not implemented in Ionide
// * DocumentHighlightProvider - highlights usages of same symbol in the file, implemented in Ionide
// * DocumentLinkProvider - detects links in the editor, not implemented in Ionide
// * DocumentRangeFormattingEditProvider - format selected text, not implemented in Ionide
// * DocumentSymbolProvider - shows all symbols defined in file, implemented in Ionide
// * HoverProvider - tooltips, implemented by Ionide
// * ImplementationProvider - go-to-implementation, not implemented by Ionide
// * OnTypeFormattingEditProvider - real time formatting, not implemented in Ionide
// * ReferenceProvider - find references, implemented in Ionide
// * RenameProvider - rename, implemented in Ionide
// * SignatureHelpProvider - shows signature helpers, implemented in Ionide
// * TypeDefinitionProvider - go-to-type-definition, not implemented in Ioide
// * WorkspaceSymbolProvider - go-to-symbol in workspace, implemented in Ionide
// * TreeDataProvider - create additional tree views in Explorer panel, implemented in Ionide for project explorer
// * TaskProvider - automatic task detection, not implemented in Ionide
// * TextDocumentContentProvider - displaying custom text content in editor, implemented in Ionide for failed project parsing diagnostics