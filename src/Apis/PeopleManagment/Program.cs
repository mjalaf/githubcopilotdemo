using Apis.PeopleManagement.Repositories;
using Apis.PeopleManagment.DataBaseContxt;
using Apis.PeopleManagment.Interfaces;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependency Injection to my Controllers
builder.Services.AddScoped<IPeopleProvider, PeopleManagementRepository>();
 
string? connectionStringSecretName = builder.Configuration.GetConnectionString("DefaultConnection");
string keyVaultUrl = Environment.GetEnvironmentVariable("KEYVAULT_URL") ?? "https://default-keyvault-url.com";

SecretClient _secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
var secret = await _secretClient.GetSecretAsync(connectionStringSecretName);
String? connectionString = secret.Value.Value;

builder.Services.AddDbContext<PeopleManagementContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.Logger.LogInformation("Service Started");

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
