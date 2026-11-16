using UserManagementAPI.Middleware;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Register application services.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserService, InMemoryUserService>();

var app = builder.Build();

// Global exception handling should be early in the pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Logs each request and its processing duration.
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();

// Exposed for integration testing if tests are added later.
public partial class Program { }
