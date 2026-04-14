using DataAccess.DTOs.User;

namespace BusinessRule.Interfaces
{
    public interface IUserService
    {
        Task<GetUserOutputDto> GetUserAsync(GetUserInputDto input);
    }
}
