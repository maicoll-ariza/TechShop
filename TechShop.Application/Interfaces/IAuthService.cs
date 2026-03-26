using TechShop.Application.Features.Auth.DTOs;

namespace TechShop.Application.Interfaces;
public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);
    Task<AuthResponse?> RefreshTokenAsync(string tokenRefresh);
}