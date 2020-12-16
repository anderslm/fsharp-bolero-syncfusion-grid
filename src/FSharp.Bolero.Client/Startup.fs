namespace FSharp.Bolero.Client

open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Syncfusion.Blazor

module Program =

    [<EntryPoint>]
    let Main args =
        let builder = WebAssemblyHostBuilder.CreateDefault(args)
        builder.Services.AddSyncfusionBlazor() |> ignore
        builder.RootComponents.Add<Main.MyApp>("#main")
        builder.Build().RunAsync() |> ignore
        0
