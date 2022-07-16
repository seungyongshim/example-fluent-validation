
using FluentValidation;

namespace WebApplication1;

public static class ValidatorExt
{
    public static Aff<Unit> ValidateAff<T>(this IValidator<T> validator, T req) =>
        validator.ValidateAndThrowAsync(req).ToUnit().ToAff();
}

public static class Prelude
{
    public static Aff<T> JsonDeserializeAff<T>(Stream stream) =>
        from __ in unitEff
        from _1 in JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }).ToAff()
        select _1;

    public static IResult ResultsError(Error err) => err.Exception.Case switch
    {
        ValidationException e =>
            Results.ValidationProblem(e.Errors
                                       .GroupBy(x => x.PropertyName)
                                       .ToDictionary(g => g.Key,
                                                     g => g.Select(x => x.ErrorMessage)
                                                           .ToArray())),
        _ => Results.Problem(err.ToString())
    };
}


