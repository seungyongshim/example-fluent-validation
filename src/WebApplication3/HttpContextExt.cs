namespace WebApplication3;

public static class HttpContextExt
{
    public static async Task ExecuteAsync<T>(this HttpContext context, Aff<T> aff, Func<T, IResult> succ) =>
        await match(await aff.Run(), succ, ResultsError).ExecuteAsync(context);
}
