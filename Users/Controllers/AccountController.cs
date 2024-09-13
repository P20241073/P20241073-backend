using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Security.Domain.Services.Communication;
using Users.Domain.Model;
using Users.Services.EmailConfirmation;

namespace Users.Controllers;

public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    private readonly EmailService _emailService;
    public AccountController(UserManager<User> userManager, TokenService tokenService, EmailService emailService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticateResponse>> Login([FromBody] AuthenticateRequest authenticateRequest)
    {
        var user = await _userManager.FindByEmailAsync(authenticateRequest.Email);
        var checkConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        if(!checkConfirmed)
        {
            return Unauthorized("Email not verified.");
        }
        if (user == null || !await _userManager.CheckPasswordAsync(user, authenticateRequest.Password))
            return Unauthorized();

        return new AuthenticateResponse
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user)
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if(ModelState.IsValid)
        {
            var userExists = await _userManager.FindByEmailAsync(registerRequest.Email);
            if (userExists != null)
                return BadRequest(new { message = "User already exists" });

            var user = new User
            {   
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                LastName = registerRequest.LastName
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            //require email confirmation
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailService.SendEmailAsync(user.Email, "Bienvenido a Psyshield! Por favor verifica tu email", 
                    $"El código de verificación de su cuenta es: {code}");

            await _userManager.AddToRoleAsync(user, "User");
            return StatusCode(201);
        }
        return BadRequest();
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail(string? email, string? code)
    {

        if (email == null || code == null )
        {
            return BadRequest("Invalid or expired verification code.");
        }

        var user = await _userManager.FindByEmailAsync(email);
        if(user == null)
        {
            return BadRequest("Invalid or expired verification code.");
        }

        var IsEmailVerified  = await _userManager.ConfirmEmailAsync(user, code);
        if (IsEmailVerified.Succeeded)
        {
            return Ok(new 
            {
                message = "Email verified successfully."
            });
        }

        return BadRequest("Something went wrong.");
    }
    
    [Authorize]
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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
    {
        if(ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email);
        if (user == null)
            return BadRequest("User not found");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _emailService.SendEmailAsync(user.Email, "Reset Password", 
            $"Please reset your password by clicking here: {forgotPasswordRequest.ClientUri}/reset-password?email={user.Email}&token={token}");

        return Ok();
    }
}