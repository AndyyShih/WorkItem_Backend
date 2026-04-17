using BusinessRule.Interfaces;
using DataAccess.DTOs.WorkItem;
using DataAccess.IRepository;

namespace BusinessRule.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;

        public WorkItemService(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        public async Task<IEnumerable<WorkItemDto>> GetUserWorkItemsAsync(int userId)
        {
            var workItems = await _workItemRepository.GetListWithStatusAsync(userId);

            // 手動映射 (或使用 AutoMapper)
            return workItems.Select(w => {
                // 取得該使用者對應的狀態資料 (因為 Include 裡面已經過濾過 userId)
                var status = w.UserWorkItemStatuses.FirstOrDefault();

                return new WorkItemDto
                {
                    Id = w.Id,
                    Title = w.Title,
                    Description = w.Description,
                    CreatedAt = w.CreatedAt,
                    IsConfirmed = status?.IsConfirmed ?? false,
                    ConfirmedAt = status?.UpdatedAt // 如果沒確認過，status 為 null
                };
            });
        }
    }
}
