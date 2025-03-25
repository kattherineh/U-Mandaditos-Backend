using System.Text;
using API.Configuration;
using Infrastructure;
using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuraciÃ³n desde variables de entorno y appsettings.json
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var dbConfig = new DatabaseConfig();

// Introduction message to show in console
string? version = builder.Configuration[key: "version"];
if (version != null)
{
    Console.WriteLine($"U-Mandaditos API v{version}");
}
else
{
    Console.WriteLine("U-Mandaditos API version not found");
}

Console.WriteLine($"Up at {DateTime.Now}");

// Testing database connection

if (dbConfig.TestConnection())
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Connection successfully to U-Mandaditos database\n");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Connection failed to U-Mandaditos database\n");
}
Console.ResetColor();

// Cargar configuracion de Jwt para Autenticación
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthorization();

builder.Services.AddAuthentication("Bearer").AddJwtBearer( options =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signingKey
    };

    // Personalizar error
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Personalizar el error 401 (Unauthorized)
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(
                "{ \"success\": false, \"message\": \"Token inválido, expirado o no existente\"}"
            );
        }
    };

});

// Add services to the container.
builder.Services.AddDbContext<BackendDbContext>(options => options.UseSqlServer(dbConfig.ConnectionString));

builder.Services.AddInfrastructure();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();