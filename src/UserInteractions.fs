module UserInteractions

open Fable.Core
open Fable.Import
open Fable.Import.vscode
open Fable.Core.JsInterop

//Custom helpers that we're using in Ionide
open Ionide.VSCode.Helpers

let activate (context : vscode.ExtensionContext) =

    // There are 5 main way of interacting with users:
    // 1. Commands - the actions that user can execute using command palette, or other parts of UI
    // 2. Message Box - way of displaying popup information, and some options that user can choose
    // 3. InputBox & QuickPick - way of asking user for input
    // 4. Output channel - printing out logs
    // 5. Status bar - putting information in the bottom bar

    // IMPORTANT: Every command you register must be also declared in ../release/package.json file

    //Simple hello world registering one command
    vscode.commands.registerCommand("extension.sayHello", fun _ ->
        vscode.window.showInformationMessage "Hello world!" |> unbox )
    |> context.subscriptions.Add

    //Using promises
    vscode.commands.registerCommand("extension.sayHello2", fun _ ->
        promise {
            let! result = vscode.window.showInformationMessage("Is FableConf fun?", "Yes", "No")
            printfn "RESULT: %s" result
            return ()
        } |> unbox)
    |> context.subscriptions.Add

    //Interacting with JS interfaces
    let opt = createEmpty<InputBoxOptions>
    opt.password <- Some true

    //-------------------------------------------------------------------------------------------

    //TASK 1: Use functions from vscode.window namespace to show
    // input box, asking for your name, and if it was provided display
    // popup showing "Hello, YOUR_NAME!"
    //OPTIONAL: Play with function for showing quick pick

    vscode.commands.registerCommand("extension.sayHelloInput", fun _ ->
        promise {
            return ""
        } |> unbox)
    |> context.subscriptions.Add


    //TASK 2: Use different way of outputing information to the user
    // There are 2 ways in which you can do this - using Output Channel
    // or by using Status Bar, you should be able to easily find needed
    // functions in vscode.window namespace
    // IMPORTANT: Both OutputChannel and StatusBarItem should be created once,
    // in the command you just set the content

    vscode.commands.registerCommand("extension.sayHelloOutputChannel", fun _ ->
        promise {
            return ""
        } |> unbox)
    |> context.subscriptions.Add


