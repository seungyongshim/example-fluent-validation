using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication3.Filters;

public class LoggerActionFilter : IAsyncActionFilter
{
    public LoggerActionFilter(ILogger<LoggerActionFilter> logger)
    {
        Logger = logger;
    }

    public ILogger<LoggerActionFilter> Logger { get; }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _ = await next();

        Logger.LogInformation("{@Request} - {@Response}", context.HttpContext.Request,
                                                          context.HttpContext.Response);
    }
}
