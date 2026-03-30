using TechShop.Application.Features.Auth.DTOs;

namespace TechShop.Application.Interfaces;
public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);
    Task<AuthResponse?> RefreshTokenAsync(string tokenRefresh);
    Task<bool> LogoutAsync(string refreshToken);
    // Task<AuthResponse?> PasswordChangeAsync(PasswordChangeRequest passwordChangeRequest);
}