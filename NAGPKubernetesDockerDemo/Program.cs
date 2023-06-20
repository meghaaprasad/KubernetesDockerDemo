using Microsoft.OpenApi.Models;
using NAGPKubernetesDockerDemo.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5932);
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
});
builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
 {
     config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
         .AddJsonFile("/config/db-config.json", optional: true, reloadOnChange: true)
         .AddJsonFile("appsettings.json", optional: true)
         .AddEnvironmentVariables();
 });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});


builder.WebHost.ConfigureServices((hostContext, services) =>
 {
     IConfiguration configuration = hostContext.Configuration;

     // Read the connection string from appsettings.json
     string username = Environment.GetEnvironmentVariable("DATABASE_USER");
     string password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
     string server = Environment.GetEnvironmentVariable("DATABASE_HOST");
     string database = Environment.GetEnvironmentVariable("DATABASE_NAME");
     // Build the database connection string
     string connectionString = $"server={server}; database={database}; user={username}; password={password}";
     Console.WriteLine($"Connecting to {connectionString}");
     services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

 });

var app = builder.Build();

var configuration = app.Configuration;


app.UseRouting();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty; // Set the Swagger UI root route
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context => context.Response.WriteAsync("Hello World!"));
    endpoints.MapControllers();
});

app.Run();


