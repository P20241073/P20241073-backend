using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Security.Domain.Services.Communication;
using Users.Domain.Model;
namespace Users.Controllers;


public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    public AccountController(UserManager<User> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticateResponse>> Login(AuthenticateRequest authenticateRequest)
    {
        var user = await _userManager.FindByEmailAsync(authenticateRequest.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, authenticateRequest.Password))
            return Unauthorized();

        return new AuthenticateResponse
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user)
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest registerRequest)
    {
        var user = new User
        {
            Email = registerRequest.Email,
            Name = registerRequest.Name,
            LastName = registerRequest.LastName
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, "User");

        return StatusCode(201);
    }

    [HttpGet("me")]
    public async Task<ActionResult<AuthenticateResponse>> GetCurrentUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return new AuthenticateResponse
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user)
        };
    }
}