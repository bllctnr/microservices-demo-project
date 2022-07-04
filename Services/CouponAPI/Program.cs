using AutoMapper;
using Core.IdentityService;
using Ecommerce.Services.CouponCode.APIBusiness.Abstract;
using Ecommerce.Services.CouponCode.APIBusiness.Concrete;
using Ecommerce.Services.CouponCode.APIEntities.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // Create Policy
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // Remove mapping of sub
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Token Issuer
    options.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value;
    options.Audience = "resource_coupon";
    options.RequireHttpsMetadata = false;
});

// AutoMapper Implementation
var automapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new GeneralMappings());
});
var mapper = automapperConfig.CreateMapper​();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers(opt => {
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


// Dependency Resolve
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityManager>();
builder.Services.AddScoped<ICouponService, CouponManager>();


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
