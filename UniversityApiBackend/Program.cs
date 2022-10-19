using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

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

// 4. Add Custom Services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();

// TODO: Add the rest of services

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 5. CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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

// 6. Tell app to use cors
app.UseCors("CorsPolicy");

app.Run();
