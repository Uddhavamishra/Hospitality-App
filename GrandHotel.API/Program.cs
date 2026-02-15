using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Services
// ---------------------------------------------------------------------------

// Controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title   = "GrandHotel API",
        Version = "v1",
        Description = "Cloud-Native Hospitality System API — designed for Kubernetes zero-downtime rolling updates."
    });
});

// Health Checks (CRITICAL for Kubernetes Liveness / Readiness probes)
builder.Services.AddHealthChecks();

// Console logging (Kubernetes reads stdout)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// ---------------------------------------------------------------------------
// Middleware Pipeline
// ---------------------------------------------------------------------------

// NOTE: HTTPS redirection is intentionally DISABLED to simplify SSL inside Minikube.
// app.UseHttpsRedirection();

// Serve static files from wwwroot (Dashboard UI)
app.UseDefaultFiles(); // Serves index.html at "/"
app.UseStaticFiles();

// Swagger — available in all environments so we can test inside the cluster
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "GrandHotel API v1");
    options.RoutePrefix = "swagger"; // Swagger UI at /swagger
});

app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Health check endpoint — used by Kubernetes livenessProbe & readinessProbe
app.MapHealthChecks("/health");

// ---------------------------------------------------------------------------
// Run
// ---------------------------------------------------------------------------

app.Logger.LogInformation("🏨 GrandHotel.API is starting on {Environment} ...",
    Environment.GetEnvironmentVariable("APP_ENVIRONMENT") ?? "Development");

app.Run();
