using Microsoft.EntityFrameworkCore;
using PruebaAPI.Interfaces;
using PruebaAPI.Models;
using PruebaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//consultar
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
builder.Services.AddScoped<UsuarioInterface, UsuarioService>();
builder.Services.AddScoped<PersonaInterface, PersonaService>();
// se construye el servidor de base de datos, eh indica la direccion donde esta ubicada la la cadena de conexion
// para mas informacion vicite: https://learn.microsoft.com/es-es/ef/core/miscellaneous/connection-strings
builder.Services.AddDbContext<pruebaApiContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));





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
