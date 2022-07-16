namespace WebApplication3;

public static class HttpContextExt
{
    public static async Task ExecuteAsync<T>(this HttpContext context, Aff<T> aff, Func<T, IResult> succ) =>
        await match(await aff.Run(), succ, ResultsError).ExecuteAsync(context);

    public static Aff<T> ReadFromJsonAff<T>(this HttpContext http) =>
        from _1 in unitEff
        from _2 in http.Request.ReadFromJsonAsync<T>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }, http.RequestAborted).ToAff()
        select _2;
}
