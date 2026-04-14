using DataAccess.DTOs.User;

namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        Task<GetUserOutputDto> GetUserAsync(GetUserInputDto input);
    }
}
