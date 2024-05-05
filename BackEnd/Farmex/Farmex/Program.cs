using Farmex.Repositories;
using Farmex.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra DbConnectionFactory
builder.Services.AddSingleton<DbConnectionFactory>();

// Ahora puedes registrar UserRepository
builder.Services.AddScoped<UserRepository>(); // O AddTransient o AddSingleton dependiendo de tus necesidades

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
