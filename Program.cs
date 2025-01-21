using GitHubBlipAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços à aplicação
builder.Services.AddControllers();
builder.Services.AddHttpClient<IGitHubService, GitHubService>();

var app = builder.Build();

// Configura o ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Define a porta a partir da variável de ambiente "PORT"
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Configura o roteamento e os controladores
app.UseRouting();
app.MapControllers();

app.Run();
