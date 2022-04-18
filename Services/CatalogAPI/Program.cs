
using CatalogAPI.Business.Abstract;
using CatalogAPI.Services;
using CatalogAPI.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
// Business
builder.Services.AddScoped<ICategoryService, CategoryManager>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();

// ConfigurationManager : Service entegrasyonlari surecinde herhangi bir konfigurasyonel degere ihtiyacimiz varsa 

