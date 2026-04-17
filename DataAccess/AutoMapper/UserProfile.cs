using AutoMapper;
using DataAccess.DTOs.User;
using DataAccess.DTOs.WorkItem;
using DataAccess.Models;

namespace DataAccess.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserOutputDto>();
            CreateMap<UserWorkItemStatus, UserWorkItemStatusDto>();
            CreateMap<WorkItem, WorkItemDto>();
        }
    }
}
