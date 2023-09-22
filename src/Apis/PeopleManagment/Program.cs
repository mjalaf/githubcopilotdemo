using Apis.PeopleManagement.Repositories;
using Apis.PeopleManagment.DataBaseContxt;
using Apis.PeopleManagment.Interfaces;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependency Injection to my Controllers
builder.Services.AddScoped<IPeopleProvider, PeopleManagementRepository>();
 
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#if !DEBUG
    string keyVaultUrl = Environment.GetEnvironmentVariable("KEYVAULT_URL") ?? "https://default-keyvault-url.com";
    SecretClient _secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
    var secret = await _secretClient.GetSecretAsync(connectionString);
    connectionString = secret.Value.Value;
#endif

builder.Services.AddDbContext<PeopleManagementContext>(options =>
    options.UseSqlServer(connectionString));

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
