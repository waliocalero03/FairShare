using DbUp;
using FairShare.API.Mappers;
using FairShare.API.Mappers.Interfaces;
using FairShare.Data.Interfaces;
using FairShare.Data.Repositories;
using FluentValidation;
using Npgsql;
using Scalar.AspNetCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if(string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Cadena de conexión no encontrada en la configuración.");
}

ReviewBBDD(connectionString);

// MAPPERS
builder.Services.AddSingleton<IGroupMapper, GroupMapper>();

// DB CONNECTION
builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connectionString));

// REPOSITORIES
builder.Services.AddSingleton<IGroupRepository, GroupRepository>();

// VALIDATORS
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // This is for have the visual interface like Swagger UI
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ReviewBBDD(string connectionString)
{
    EnsureDatabase.For.PostgresqlDatabase(connectionString);

    var upgrader = DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(typeof(FairShare.Data.AssemblyReference).Assembly)
        .LogToConsole()
        .Build();

    var result = upgrader.PerformUpgrade();

    if (!result.Successful)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error en la migración de la base de datos:");
        Console.WriteLine(result.Error);
        Console.ResetColor();

        // Detiene el arranque si la base de datos falla
        throw new Exception("Fallo al inicializar la base de datos", result.Error);
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("¡Base de datos actualizada y lista!");
    Console.ResetColor();
}