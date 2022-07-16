using Flurl.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using WebApplication3.Controllers;

namespace WebApplication3.Tests;

public class NotificationMailControllerSpec
{
    public NotificationMailControllerSpec()
    {
        var loggerFactory = LoggerFactory.Create(x => { });
        Logger = loggerFactory.CreateLogger<NotificationMailController>();
    }

    public ILogger<NotificationMailController> Logger { get; private set; }

    [Fact]
    public async Task Success()
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
                                  .PostJsonAsync(new
                                  {
                                      Email = "11@11.11"
                                  });
    }
}
