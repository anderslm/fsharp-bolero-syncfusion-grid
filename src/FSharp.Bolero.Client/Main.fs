module FSharp.Bolero.Client.Main

open Elmish
open Bolero
open Bolero.Remoting.Client
open Bolero.Templating.Client
open Bolero.Html
open FSharp.Control.Tasks
open Syncfusion.Blazor.Grids
open Syncfusion.Blazor.Buttons
open Microsoft.AspNetCore.Components.Web

type Page =
    | [<EndPoint "/">] Home

type ViewModel = { Col1 : string
                   Col2 : string
                   Col3 : string }

type Model =
    { Page : Page
      Data : ViewModel list }

let init _ =
    { Page = Home
      Data = [ { Col1 = "1.1"
                 Col2 = "1.2"
                 Col3 = "1.3" }
               { Col1 = "2.1"
                 Col2 = "2.2"
                 Col3 = "2.3" }
               { Col1 = "3.1"
                 Col2 = "3.2"
                 Col3 = "3.3" } ] }
    , Cmd.none

type Message =
    | SetPage of Page
    | ExportExcel of SfGrid<ViewModel>
    | ExcelExported

let update message model =
    match message with
    | SetPage p -> { model with Page = p }, Cmd.none
    | ExportExcel grid ->
        model,
        Cmd.OfTask.perform (fun () ->
            task {
                do! grid.ExcelExport()
            }) () (fun () -> ExcelExported)
    | ExcelExported ->
        { model with Data = [ { Col1 = "1.1"
                                Col2 = "1.2"
                                Col3 = "1.3" } ] }
        , Cmd.none

let router = Router.infer SetPage (fun model -> model.Page)

type Main = Template<"wwwroot/main.html">

let view model dispatch =
    let gridRef = Ref<SfGrid<ViewModel>>()
    
    Main()
        .Grid(
            comp<SfGrid<ViewModel>>
                [ attr.ref gridRef
                  "DataSource" => model.Data
                  "AllowExcelExport" => true ]
                [ comp<GridColumns> []
                    [ comp<GridColumn>
                          [ "Field" => "Col1" ] []
                      comp<GridColumn>
                          [ "Field" => "Col2" ] []
                      comp<GridColumn>
                          [ "Field" => "Col3" ] [] ] ]
        ).ExportToExcel(
            comp<SfButton>
                [ attr.callback<MouseEventArgs> "OnClick" (fun _ -> dispatch <| ExportExcel gridRef.Value)
                  "Content" => "Excel Export" ] []
        ).Elt()

type MyApp() =
    inherit ProgramComponent<Model, Message>()

    override this.Program =
        Program.mkProgram init update view
        |> Program.withRouter router