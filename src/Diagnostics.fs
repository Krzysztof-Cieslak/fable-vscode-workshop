module Diagnostics

open Fable.Core
open Fable.Import
open Fable.Import.vscode
open Fable.Import.Node
open Fable.Core.JsInterop

//Custom helpers that we're using in Ionide
open Ionide.VSCode.Helpers

/// Schedules execution of a one-time callback after delay milliseconds.
/// Returns a Timeout for use with `clearTimeout`.
[<Emit("setTimeout($0, $1)")>]
let setTimeout (callback: unit -> unit) (delay: float): Base.NodeJS.Timer = jsNative

/// Cancels a Timeout object created by `setTimeout`.
[<Emit("clearTimeout($0)")>]

let clearTimeout (timeout: Base.NodeJS.Timer): unit = jsNative
let mutable private timer = None

let mutable private currentDiagnostic = languages.createDiagnosticCollection ()

let sampleEventHandler (event : TextDocumentChangeEvent) =
    currentDiagnostic.clear()
    let sampleRange = Range(0., 0., 0., 100.)
    let sampleDiags =
        Diagnostic(sampleRange, "some message", DiagnosticSeverity.Error)
        |> Array.singleton
        |> ResizeArray
    let sampleFileDiags =
        (event.document.uri, sampleDiags)
        |> Array.singleton
        |> ResizeArray

    currentDiagnostic.set(sampleFileDiags)

let sampleTimer () =
    timer |> Option.iter(clearTimeout)
    timer <- Some (setTimeout (fun _ -> printfn "TIMEOUT CALL" ) 1000.)

//TASK 1: Use event handler and timeout to set diagnostic on the file after user stoped writting for 1000ms.
// Put diagnostic on every line that's not empty and doesn't start with #

let activate (context : vscode.ExtensionContext) =
    workspace.onDidChangeTextDocument.Invoke(unbox sampleEventHandler)
    |> context.subscriptions.Add

    sampleTimer ()

    ()