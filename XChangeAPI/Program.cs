using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using XChangeAPI.Data;
using XChangeAPI.Data.Interfaces;
using XChangeAPI.Logic;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Services;
using XChangeAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("./logs.txt")
    .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(log);

// Add services to the container.


builder.Services.AddSingleton<ICurrencyLogic, CurrencyLogic>();
builder.Services.AddSingleton<ICurrencyData, CurrencyData>();

builder.Services.AddSingleton<IHistoryLogic, HistoryLogic>();
builder.Services.AddSingleton<IHistoryData, HistoryData>();

builder.Services.AddSingleton<ITickerLogic, TickerLogic>();
builder.Services.AddSingleton<ITickerData, TickerData>();

builder.Services.AddSingleton<IUserLogic, UserLogic>();
builder.Services.AddSingleton<IUserData, UserData>();

builder.Services.AddSingleton<IExchangeRateService, ExchangeRateService>();


builder.Services.AddControllers();
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:APIToken").Value!))
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
