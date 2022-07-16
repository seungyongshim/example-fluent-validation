using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dto;

namespace WebApplication3.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class NotificationMailController : ControllerBase
{
    public NotificationMailController(ILogger<NotificationMailController> logger) => Logger = logger;

    public ILogger Logger { get; }

    [HttpPost]
    [Consumes(typeof(NotificationMailDto), "application/json")]
    public async Task Post()
    {
        var dto = HttpContext.Request.Body;

        var q = from __ in unitEff
                from req in JsonDeserializeAff<NotificationMailDto>(dto)
                from _2 in Logger.InfoEff("here")
                from _1 in ValidateAff(req)
                select req;

        await HttpContext.ExecuteAsync(q, Results.Ok);
    }
}
