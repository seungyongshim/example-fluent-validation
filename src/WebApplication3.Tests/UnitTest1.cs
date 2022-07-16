using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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



        var httpContext = new DefaultHttpContext();

        using var ms = new MemoryStream();
        using var sw = new StreamWriter(ms);
        sw.WriteLine("{}");
        httpContext.Request.Body = ms;

        var controller = new NotificationMailController(Logger)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            }
        };

        await controller.PostAsync();
        
    }
}
