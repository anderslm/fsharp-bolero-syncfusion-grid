module FSharp.Bolero.Client.Main

open Elmish
open Bolero
open Bolero.Remoting.Client
open Bolero.Templating.Client

type Page =
    | [<EndPoint "/">] Home

type Model =
    { Page : Page }

let init _ =
    { Page = Home }, Cmd.none

type Message =
    | SetPage of Page

let update message model =
    match message with
    | SetPage p -> { model with Page = p }, Cmd.none

let router = Router.infer SetPage (fun model -> model.Page)

type Main = Template<"wwwroot/main.html">

let view model dispatch =
    Main().Elt()

type MyApp() =
    inherit ProgramComponent<Model, Message>()

    override this.Program =
        
        Program.mkProgram init update view
        |> Program.withRouter router