using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoProject.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AnotherPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Add services to the container.
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer("Server=DESKTOP-PAQGO90; Database=TodoDb;uid=sa;pwd=1234; TrustServerCertificate=True",
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        }));
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
});

var app = builder.Build();

app.UseCors(x => x
    .WithOrigins("http://localhost:5173","https://localhost:5173", "http://localhost:5292","https://localhost:5292")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
