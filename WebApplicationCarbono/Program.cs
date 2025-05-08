using Microsoft.Extensions.Configuration;
using WebApplicationCarbono.Interface;
using WebApplicationCarbono.Servi�os;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��es de servi�o (Registrar antes de chamar builder.Build())
builder.Services.AddScoped<ISaldo, SaldoServi�os>();
builder.Services.AddScoped<ICreditos, CreditosServi�os>();

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
