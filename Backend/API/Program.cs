using API.Configuration;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConfig = new DatabaseConfig();

// Testing database connection
if (dbConfig.TestConnection())
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Connection successfully to U-Mandaditos database"); 
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Connection failed to U-Mandaditos database");
}


Console.ResetColor();


// Add services to the container.
builder.Services.AddDbContext<BackendDbContext>(options => options.UseSqlServer(dbConfig.ConnectionString));


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

app.UseAuthorization();

app.MapControllers();

app.Run();