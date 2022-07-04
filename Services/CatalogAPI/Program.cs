
using Ecommerce.Services.Catalog.APIBusiness.Abstract;
using Ecommerce.Services.Catalog.APIBusiness.Concrete;
using Ecommerce.Services.Catalog.APIServices;
using Ecommerce.Services.Catalog.APISettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Add Jwt Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Token Issuer
    options.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value;
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});

// Add services to the container.
builder.Services.AddControllers(options => 
{
    // Add authorize to all controllers
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

// Business
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
// Business

builder.Services.AddSingleton<IDataBaseSettings>(sp => 
{
    return sp.GetRequiredService<IOptions<DataBaseSettings>>().Value;
});

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

// ConfigurationManager : Service entegrasyonlari surecinde herhangi bir konfigurasyonel degere ihtiyacimiz varsa 

