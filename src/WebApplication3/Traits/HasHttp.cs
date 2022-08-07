using LanguageExt.Attributes;
using LanguageExt.Effects.Traits;
using LanguageExt.Pipes;
using static LanguageExt.Prelude;

namespace WebApplication3.Traits;

public interface IHttpIO
{
    ValueTask<T?> RequestToAsync<T>();
}

[Typeclass("*")]
public interface HasHttp<RT> : HasCancel<RT> where RT : struct, HasHttp<RT>
{
    HttpContext HttpContext { get; }
    Eff<RT, IHttpIO> HttpEff => Eff<RT, IHttpIO>(static rt => new HttpIO
    {
        HttpContext = rt.HttpContext
    });
}


public class HttpIO : IHttpIO
{
    public HttpContext? HttpContext { get; init; }

    public ValueTask<T?> RequestToAsync<T>() =>
        HttpContext?.Request.ReadFromJsonAsync<T>(new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    }, HttpContext.RequestAborted) ?? throw new NullReferenceException();
}
