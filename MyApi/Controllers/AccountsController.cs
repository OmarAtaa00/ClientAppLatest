using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountsController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserService userService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest("Invalid username or password");
            }

            // Redirect to IdentityServer login endpoint
            var authority = _configuration["IdentityServer:BaseUrl"];
            var clientId = _configuration["Clients:MyClient:ClientId"];
            var redirectUrl = $"{Request.Scheme}://{Request.Host}/callback";

            return Redirect($"{authority}/connect/authorize?client_id={clientId}&redirect_uri={redirectUrl}&response_type=code&scope=openid profile api myapi"); // Replace "myapi" with your API resource name
        }

        [HttpGet("me")]
        [Authorize] // Requires valid JWT token
        public async Task<IActionResult> GetCurrentUser()
        {
            // Access user information from the claims principal
            var user = await _userService.GetUserByUsername(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            // Return a sanitized user object (exclude sensitive data)
            return Ok(new { Id = user.Id, Username = user.UserName, FullName = user.FullName });
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
