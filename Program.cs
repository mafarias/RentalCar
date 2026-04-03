using Microsoft.EntityFrameworkCore;
using RentalCar.Application.Interfaces;
using RentalCar.Infrastructure.Data;
using RentalCar.Infrastructure.Repositories;
using RentalCar.Infrastructure.Repositories.RentalCar.Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// --- 1. REGISTRO DE SERVICIOS (CONTENEDOR DI) ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDbContext>();

// Registro de MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql"))
    ));

// Registro de la Interfaz del Contexto (Clean Architecture - Opción A)
// Usamos GetRequiredService para asegurar que use la misma instancia de AppDbContext
builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

// Registro de Repositorios
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();

// Registro de MediatR (Escanea el proyecto Application para Handlers, Commands y Events)
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(IVehiculoRepository).Assembly);
});


// --- 2. CONSTRUCCIÓN DE LA APP ---

var app = builder.Build();


// --- 3. CONFIGURACIÓN DEL PIPELINE (MIDDLEWARES) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();