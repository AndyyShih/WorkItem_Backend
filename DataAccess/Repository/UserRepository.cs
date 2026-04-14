using AutoMapper;
using DataAccess.DTOs.User;
using DataAccess.IRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkItemContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(WorkItemContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetUserOutputDto> GetUserAsync(GetUserInputDto input)
        {
            var query = await _dbContext.Users
                .Include(u => u.UserWorkItemStatuses)
                .ThenInclude(s => s.WorkItem)
                .Where(x => x.Id == input.Id)
                .FirstOrDefaultAsync();

            var result = _mapper.Map<User , GetUserOutputDto>(query);
            return result;
        }
    }
}
