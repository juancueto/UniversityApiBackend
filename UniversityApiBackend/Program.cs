using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using UniversityApiBackend.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 2. Connection with SQL Server

const string connectionName = "UniversityDb";
var connectionString = builder.Configuration.GetConnectionString(connectionName);

// Add services to the container.

// 3. Add Context to Services of Builder
builder.Services.AddDbContext<UniversityDBContext>(options =>
    options.UseSqlServer(connectionString)
);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
