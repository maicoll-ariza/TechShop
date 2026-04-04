using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Common;
using TechShop.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TechShop.Application.Features.Auth.DTOs;

namespace TechShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService _authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return Ok(ApiResponse<AuthResponse>.Ok(result, "Usuario registrado correctamente"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(ApiResponse<AuthResponse>.Ok(result, "Login exitoso"));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken);
        return Ok(ApiResponse<AuthResponse>.Ok(result, "Token renovado"));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
    {
        await _authService.LogoutAsync(request.RefreshToken);
        return Ok(ApiResponse<object>.Ok(null, "Sesión cerrada correctamente"));
    }
}