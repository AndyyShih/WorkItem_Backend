using BusinessRule.Interfaces;
using DataAccess.DTOs.Auth;
using DataAccess.IRepository;

namespace BusinessRule.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResultDto?> LoginAsync(string username, string password)
        {
            var user = await _authRepository.GetByUsernameAsync(username);

            // 驗證帳號是否存在及密碼是否正確
            if (user == null || user.PasswordHash != password)
            {
                return null;
            }

            // 僅回傳必要資訊，排除密碼敏感資料
            return new LoginResultDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
