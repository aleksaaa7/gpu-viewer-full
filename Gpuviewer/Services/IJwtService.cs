using Gpuviewer.Models;

namespace Gpuviewer.Services
{
    public interface IJwtService
    {
        Task<LoginResponseModel?> AuthenticateAsync(LoginDto loginDto);
        Task<RegisterResponseModel?> RegisterAsync(RegisterDto registerDto);
    }
}