using BCrypt.Net;
using Microsoft.Extensions.Options;
using TechShop.Application.Common.Settings;
using TechShop.Application.Features.Auth.DTOs;
using TechShop.Application.Interfaces;
using TechShop.Domain.Entities;
using TechShop.Domain.Exceptions;

namespace TechShop.Infrastructure.Services;

public class AuthService(
    IUserRepository userRepository,
    JwtService jwtService,
    IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly JwtService _jwtService = jwtService;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await _userRepository.ExistsAsync(request.Email))
        throw new BusinessException("El correo ya está en uso");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            RefreshToken = _jwtService.GenerateRefreshToken(),
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
        };

        await _userRepository.CreateAsync(user);

        return BuildAuthResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new BusinessException("Correo o contraseña incorrectos");

        user.RefreshToken = _jwtService.GenerateRefreshToken();
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);

        await _userRepository.UpdateAsync(user);

        return BuildAuthResponse(user);
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new BusinessException("Token inválido o expirado");

        user.RefreshToken = _jwtService.GenerateRefreshToken();
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);

        await _userRepository.UpdateAsync(user);

        return BuildAuthResponse(user);
    }

    public async Task<bool> LogoutAsync(string refreshToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

        if (user == null)
            throw new BusinessException("Token inválido");

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    private AuthResponse BuildAuthResponse(User user)
    {
        return new AuthResponse
        {
            AccessToken = _jwtService.GenerateAccessToken(user),
            RefreshToken = user.RefreshToken!,
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            Role = user.Role
        };
    }
}