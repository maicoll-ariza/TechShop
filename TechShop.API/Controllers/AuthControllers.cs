using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Features.Auth.DTOs;
using TechShop.Application.Interfaces;



namespace TechShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if(result == null) return Unauthorized("No se puede registrar. Correo en uso.");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null) return Unauthorized("Correo o contraseña incorrecta");

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshToken)
    {
        var result = await _authService.RefreshTokenAsync(refreshToken.RefreshToken);
        if(result == null) return Unauthorized("Token inválido");
        return Ok(result);
    }

}