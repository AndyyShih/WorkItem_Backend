using DataAccess.DTOs.Auth;

namespace BusinessRule.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResultDto?> LoginAsync(string username, string password);

        Task<LoginResultDto?> GetUserProfileAsync(int userId);
    }
}
