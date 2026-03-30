using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Common;
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

        if(result == null) return BadRequest(ApiResponse<AuthResponse>.Fail("No se pudo registrar. Correo en uso"));

        return Ok(ApiResponse<AuthResponse>.Ok(result, "Usuario registrado correctamente"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null) return BadRequest(ApiResponse<AuthResponse>.Fail("Correo o contraseña incorrecta"));

        return Ok(ApiResponse<AuthResponse>.Ok(result));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshToken)
    {
        var result = await _authService.RefreshTokenAsync(refreshToken.RefreshToken);
        if(result == null) return Unauthorized(ApiResponse<AuthResponse>.Fail("Token inválido"));
        return Ok(ApiResponse<AuthResponse>.Ok(result));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest logoutRequest)
    {
        var result = await _authService.LogoutAsync(logoutRequest.RefreshToken);
        if(result) return Ok(ApiResponse<string>.Ok("Se ha cerrado la sesión exitosamente"));

        return BadRequest(ApiResponse<string>.Fail("No se pudo cerrar la sesión"));
    }

}