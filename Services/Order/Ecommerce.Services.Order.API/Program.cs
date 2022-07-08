using Core.IdentityService;
using Ecommerce.Services.Order.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor(); // For identity server
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();


var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // Create Policy
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // Remove mapping of sub
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Token Issuer
    options.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value;
    options.Audience = "resource_coupon";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure => 
    {
        configure.MigrationsAssembly("Ecommerce.Services.Order.Infrastructure");
    });
});

// MediatR => takes assembly including handlers
builder.Services.AddMediatR(typeof(Ecommerce.Services.Order.Application.Handlers.CreateOrderCommandHandler).Assembly);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
