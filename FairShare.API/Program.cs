using DbUp;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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