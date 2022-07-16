using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication3;
using WebApplication3.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(c =>
{
    c.Filters.Add<LoggerActionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<RequestBodyTypeFilter>();
});
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Logging.AddJsonConsole();

var app = builder.Build();

ValidatorOptions.Global.LanguageManager.Enabled = false;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
