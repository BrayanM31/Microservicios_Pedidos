using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", false, true);

// Registrar Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

// Middleware Ocelot
await app.UseOcelot();

app.Run();