using BusinessRule.Interfaces;
using DataAccess.DTOs.User;
using DataAccess.IRepository;

namespace BusinessRule.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<GetUserOutputDto> GetUserAsync(GetUserInputDto input)
        {
            var result = await _userRepository.GetUserAsync(input);

            return result;
        }
    }
}
