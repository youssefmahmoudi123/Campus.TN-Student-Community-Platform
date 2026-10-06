// Import namespaces required for attributes
// and types related to ASP.NET Core (authorization and MVC controllers).
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Namespace declaration to organize the application's API controllers.
// Helps avoid name collisions.
namespace Campus.Api.Controllers;

[// [ApiController]: indicates this class is an API controller.
// Provides useful default behaviors (automatic validation, etc.).
// [Route("api/[controller]"): sets the base route for this controller.
// [controller] is replaced with the controller name without the "Controller" suffix.]
[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    // Action accessible via GET at: /api/test/public
    // Public endpoint that does not require authentication.
    // Returns an HTTP 200 (OK) with a simple message.
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok("Anyone can access this.");
    }

    // [Authorize]: requires the caller to be authenticated.
    // Action accessible via GET at: /api/test/private
    // If the user is not authenticated, the framework will return 401/403.
    [Authorize]
    [HttpGet("private")]
    public IActionResult Private()
    {
        return Ok("You are authenticated!");
    }
}
