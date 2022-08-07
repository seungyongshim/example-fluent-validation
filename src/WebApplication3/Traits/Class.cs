using LanguageExt.Pipes;
using static LanguageExt.Prelude;
using static LanguageExt.Pipes.Proxy;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Traits;

public static class Http<RT>
    where RT : struct, HasHttp<RT>
{
    public static Producer<RT, T, Unit> RequestTo<T>() =>
        from http in default(RT).HttpEff
        from x in Aff<RT, T>(rt => http.RequestToAsync<T>())
        from _1 in yield(x)
        select unit;

    public static Pipe<RT, T, T, Unit> Validation<T>(IValidator<T> validator) =>
        from v in awaiting<T>()
        from _1 in Aff<RT, Unit>(rt => validator.ValidateAndThrowAsync(v).ToUnit().ToValue())
        from _2 in yield(v)
        select unit;


    public static Consumer<RT, IResult, Unit> ResponseTo =>
        from v in awaiting<IActionResult>()
        from _1 in HttpClient
}
