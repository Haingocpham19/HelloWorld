using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using System.Text;
using JwtAuthenticationHandler;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Access configuration
var configuration = builder.Configuration;

// Access specific configuration values
string validIssuer = configuration.GetSection("JwtSettings:Issuer").Value;
string validAudience = configuration.GetSection("JwtSettings:Audience").Value;

builder.Services.ConfigureJwtAuthentication(validIssuer, validAudience);

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
