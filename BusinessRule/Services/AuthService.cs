using BusinessRule.Interfaces;
using Common.Helpers;
using DataAccess.DTOs.Auth;
using DataAccess.IRepository;

namespace BusinessRule.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IAuthRepository authRepository, JwtHelper jwtHelper)
        {
            _authRepository = authRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResultDto?> LoginAsync(string username, string password)
        {
            var user = await _authRepository.GetByUsernameAsync(username);

            if (user == null || user.PasswordHash != password)
            {
                return null;
            }

            var token = _jwtHelper.GenerateToken(user.Id, user.Username, user.Role);

            return new LoginResultDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                Token = token
            };
        }
    }
}
