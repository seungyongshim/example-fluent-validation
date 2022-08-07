module Tests

open Microsoft.AspNetCore.Hosting
open System.IO
open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection

let createHost() =
    WebHostBuilder()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .Configure(Action<IApplicationBuilder> SampleApp.App.configureApp)
        .ConfigureServices(Action<IServiceCollection> SampleApp.App.configureServices)
