using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using WebApplication1.Dto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Logging.AddConsole();

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

var app = builder.Build();

ValidatorOptions.Global.LanguageManager.Enabled = false;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/api/NotificationMail", async (HttpContext http)  =>  
{
    var q = from __ in unitEff
            from req in JsonDeserializeAff<NotificationMailDto>(http.Request.Body)
            from _1 in ValidateAff(req)
            select req;           

    return match(await q.Run(), Results.Ok, ResultsError);
}).Accepts<NotificationMailDto>("application/json");

await app.RunAsync();
