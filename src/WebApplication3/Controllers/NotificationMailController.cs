using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dto;

namespace WebApplication3.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class NotificationMailController : ControllerBase
{
    public NotificationMailController(ILogger<NotificationMailController> logger)
    {
        Logger = logger;
    }

    public ILogger Logger { get; }

    [HttpPost]
    [Consumes(typeof(NotificationMailDto), "application/json")]
    public async Task PostAsync()
    {
        var q = from __ in unitEff
                from req in HttpContext.ReadFromJsonAff<NotificationMailDto>()
                from _2 in Logger.InfoEff("here")
                from _1 in ValidateAff(req)
                select req;

        await HttpContext.ExecuteAsync(q, Results.Ok);
    }
}
