using DataAccess.Models;

namespace DataAccess.IRepository
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByIdAsync(int id);
    }
}
