using JobMatch.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Registrera controllers
builder.Services.AddControllers();
// Swagger — skapar en test-sida för vårt API
builder.Services.AddSwaggerGen();
// EF Core — kopplar vår DbContext till SQLite (för lokal utveckling)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        //Lokalt
        options.UseSqlite("Data Source=JobMatch.db");
    else
        //Azure: 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
