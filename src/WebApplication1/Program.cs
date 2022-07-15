using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System.Text.Json;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

var app = builder.Build();

ValidatorOptions.Global.LanguageManager.Enabled = false;


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/NotificationMail", async (HttpRequest dto, IValidator<NotificationMail> validator) =>
{
    Console.WriteLine(dto.ContentLength);



    var q = from _ in unitEff
            from req in JsonSerializer.DeserializeAsync<NotificationMail>(dto.BodyReader.AsStream(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }).ToAff()
            from valid in validator.ValidateAndThrowAsync(req).ToUnit().ToAff()
            select req;

    var ret = await q.Run();

    return ret.Match(Results.Ok, err => err.Exception.Case switch
    {
        ValidationException e => Results.ValidationProblem(e.Errors.GroupBy(x => x.PropertyName)
                                                                   .ToDictionary(
                                                                        g => g.Key,
                                                                        g => g.Select(x => x.ErrorMessage).ToArray()
                                                                    )),
        _ => Results.Problem(err.ToString())
    });
}).Accepts<NotificationMail>("application/json");


await app.RunAsync();
