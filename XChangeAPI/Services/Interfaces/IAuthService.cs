using XChangeAPI.Models.DB;

namespace XChangeAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> CreateToken(User user);
        Task<string> HashPassword(string password);
        Task<int?> ValidateToken(string token);
    }
}