using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WebApplication3.Dto;
using LanguageExt;
using LanguageExt.Common;
using System.Linq;
using Swashbuckle.AspNetCore;

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

public static class HttpContextExt
{
    public static async Task ExecuteAsync<T>(this HttpContext context, Aff<T> aff, Func<T, IResult> func) =>
        await match(await aff.Run(), func, ResultsError).ExecuteAsync(context);
}
