using Microsoft.AspNetCore.Mvc;

namespace GrandHotel.API.Controllers;

/// <summary>
/// Provides system-level information — the primary endpoint for demonstrating
/// Kubernetes rolling updates and pod identity.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SystemController : ControllerBase
{
    // ──────────────────────────────────────────────────────────────────────
    //  ✏️  CHANGE THIS VALUE TO "2.0.0" FOR THE SECOND ROLLING UPDATE
    // ──────────────────────────────────────────────────────────────────────
    private const string AppVersion = "1.0.0";

    /// <summary>
    /// GET /api/system/info
    /// Returns the application version, the Kubernetes pod name, and the
    /// current environment — perfect for verifying rolling-update behaviour.
    /// </summary>
    [HttpGet("info")]
    [ProducesResponseType(typeof(SystemInfoResponse), StatusCodes.Status200OK)]
    public IActionResult GetInfo()
    {
        var info = new SystemInfoResponse
        {
            Version     = AppVersion,
            PodName     = Environment.MachineName,
            Environment = Environment.GetEnvironmentVariable("APP_ENVIRONMENT") ?? "Development"
        };

        return Ok(info);
    }
}

/// <summary>
/// Response DTO for the /api/system/info endpoint.
/// </summary>
public class SystemInfoResponse
{
    /// <summary>Current application version (e.g. "1.0.0").</summary>
    public string Version { get; set; } = default!;

    /// <summary>
    /// The machine / pod name. Inside Kubernetes this equals the Pod name,
    /// proving which replica handled the request.
    /// </summary>
    public string PodName { get; set; } = default!;

    /// <summary>Runtime environment read from the APP_ENVIRONMENT variable.</summary>
    public string Environment { get; set; } = default!;
}
