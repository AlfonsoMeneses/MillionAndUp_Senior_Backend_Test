using BackendTestApp.Business.Services;
using BackendTestApp.Contracts.Services;
using BackendTestApp.DataService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Adding the service 
builder.Services.AddScoped<IPropertyService, PropertyService>();

//Adding the DB Conecction
var dbConnection = builder.Configuration.GetConnectionString("MSSQL_Dev");
builder.Services.AddDbContext<BackendTestDB>(ops => ops.UseSqlServer(dbConnection));

//Adding AutoMappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
