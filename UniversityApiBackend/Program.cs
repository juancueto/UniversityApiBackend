using Microsoft.EntityFrameworkCore;
using UniversityApiBackend;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 2. Connection with SQL Server

const string connectionName = "UniversityDb";
var connectionString = builder.Configuration.GetConnectionString(connectionName);

// Add services to the container.
// 10. Localization
builder.Services.AddLocalization(options => {
    options.ResourcesPath = "Resources";
});

// 3. Add Context to Services of Builder
builder.Services.AddDbContext<UniversityDBContext>(options =>
    options.UseSqlServer(connectionString)
);

// 7. Add Service of JWT Authorization
builder.Services.AddJwtTokenServices(builder.Configuration);

builder.Services.AddControllers();

// 4. Add Custom Services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();

// 8. Add Authorization
builder.Services.AddAuthorization(options => {
    options.AddPolicy("userOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 9. Config Swagger to take care of Authorization of JWT
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name= "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement() 
    {
        {
            new OpenApiSecurityScheme(){
                Reference = new OpenApiReference(){
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }        
    });
});

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

var supportedCultures = new[] { "en-US", "es-PE", "fr-FR", "de-DE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en-US")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

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
