open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Giraffe

let endpoint = choose [
    route "/" >=> choose [
        POST >=> json "Hello world"
        GET >=> json "Hello world" 
    ]
]

let builder = WebApplication.CreateBuilder()
builder.Services.AddGiraffe() |> ignore

let app = builder.Build()
app.UseGiraffe endpoint

app.Run()
