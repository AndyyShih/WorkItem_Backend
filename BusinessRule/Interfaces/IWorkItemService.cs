using DataAccess.DTOs.WorkItem;

namespace BusinessRule.Interfaces
{
    public interface IWorkItemService
    {
        Task<IEnumerable<WorkItemDto>> GetUserWorkItemsAsync(int userId);

        Task<WorkItemDetailDto?> GetWorkItemDetailAsync(int id, int userId);
    }
}
