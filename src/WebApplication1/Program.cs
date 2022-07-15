using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using LanguageExt;
using static LanguageExt.Prelude;

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

app.MapPost( "/NotificationMail", (NotificationMail dto, IValidator<NotificationMail> validator) =>
{

    var ret = validator.Validate(dto);

    if (ret.IsValid is not true)
    {
        return Results.ValidationProblem(ret.ToDictionary());
    }

    return Results.Ok(dto);
});


await app.RunAsync();
