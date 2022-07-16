using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApplication1.Tests;

public class NotificationMailSpec
{
    [Fact]
    public async Task Test1()
    {
        var application = new WebApplicationFactory<Program>()
                             .WithWebHostBuilder(builder =>
                             {
                                 builder.ConfigureServices(services =>
                                 {
                                     // set up servises
                                 });
                             });
        using var client = application.CreateClient();
        using var flurl = new FlurlClient(client);
        var response = await flurl.Request("/api/NotificationMail")
                                  .OnError(call => { })
                                  .PostJsonAsync(new
        {
            Hello = "World"
        });
    }
}
