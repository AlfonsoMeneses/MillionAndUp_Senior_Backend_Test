using BackendTestApp.Business.Services;
using BackendTestApp.Contracts.Services;
using BackendTestApp.DataService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Agregando Los Servicios
builder.Services.AddScoped<IPropertyService, PropertyService>();


//Agregando Conexi�n Con la DB
var dbConnection = builder.Configuration.GetConnectionString("MSSQL_Dev");

builder.Services.AddDbContext<BackendTestDB>(ops => ops.UseSqlServer(dbConnection));

//AutoMappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
