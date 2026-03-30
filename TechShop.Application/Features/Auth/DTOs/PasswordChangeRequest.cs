

using TechShop.Application.Features.Auth.DTOs;

public class PasswordChangeRequest : LoginRequest
{
    
    public string NewPassword { get; set; } = string.Empty;

} 