using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using System.Text;
using JwtAuthenticationHandler;

var builder = WebApplication.CreateBuilder(args);

// Access configuration
var configuration = builder.Configuration;

// Access specific configuration values
var issuerSigningKey = configuration.GetSection("JwtSettings:TokenValidationParameters:IssuerSigningKey").Value;

builder.Services.ConfigureJwtAuthentication(issuerSigningKey);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Configuration
    .AddJsonFile("ocelot.json");

builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

// Use Ocelot
app.UseOcelot().Wait();

app.Run();
