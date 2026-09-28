using DbUp;
using FairShare.API.Mappers;
using FairShare.API.Mappers.Classes;
using FairShare.API.Mappers.Interfaces;
using FairShare.Data.Interfaces;
using FairShare.Data.Repositories;
using FluentValidation;
using Npgsql;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
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
builder.Services.AddSingleton<IParticipantMapper, ParticipantMapper>();
builder.Services.AddSingleton<IExpenseMapper, ExpenseMapper>();
builder.Services.AddSingleton<IExpenseSplitMapper, ExpenseSplitMapper>();

// DB CONNECTION
builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connectionString));

// REPOSITORIES
builder.Services.AddSingleton<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseSplitRepository, ExpenseSplitRepository>();

// VALIDATORS
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// SERILOG
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/fairshare-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.PostgreSQL(
        connectionString,
        "Logs",
        null,
        null,
        restrictedToMinimumLevel: LogEventLevel.Information,
        needAutoCreateTable: true)  
    .CreateLogger();

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