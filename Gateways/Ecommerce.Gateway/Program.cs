using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ecommerce.Gateway.DelegateHandlers;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Using different config json files for development and production.
builder.Configuration.AddJsonFile($"ocelot.configuration.{builder.Environment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options => 
{
    // Token Issuer
    options.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value;
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false
    };
});

// Added token excxhange delegate handler
builder.Services.AddOcelot().AddDelegatingHandler<TokenExchangeDelegateHandler>();
builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();


var app = builder.Build();

app.UseOcelot().Wait();
app.Run();