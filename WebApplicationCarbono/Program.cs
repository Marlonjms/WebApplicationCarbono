using Helpers;
using WebApplicationCarbono.Interface;
using WebApplicationCarbono.Servi�os;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

// Configuta��es para aparecer o Campo  de add o token
builder.Services.AddSwaggerDocumentation();


// Adicionar servi�os ao cont�iner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o de servi�os
builder.Services.AddScoped<ISaldo, SaldoServi�os>();
builder.Services.AddScoped<ICreditos, CreditosServi�os>();
builder.Services.AddScoped<IProjetos, ProjetosServi�os>();
builder.Services.AddScoped<ITransa�ao, TransacaoServi�os>();
builder.Services.AddScoped<IUsuario, UsuarioServi�o>();
builder.Services.AddScoped<IAutenticacao, AutenticacaoServico>();

var app = builder.Build();

// Configurar o pipeline de requisi��o
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
