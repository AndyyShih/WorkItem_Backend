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

        public async Task<WorkItemDetailDto?> GetWorkItemDetailAsync(int id, int userId)
        {
            var workItem = await _workItemRepository.GetDetailWithStatusAsync(id, userId);

            if (workItem == null) return null;

            var statusRecord = workItem.UserWorkItemStatuses.FirstOrDefault();

            return new WorkItemDetailDto
            {
                Id = workItem.Id,
                Title = workItem.Title,
                Description = workItem.Description,
                // 邏輯判定：如果有紀錄且已確認則為 confirmed，否則為 pending
                Status = (statusRecord?.IsConfirmed ?? false) ? "confirmed" : "pending",
                CreatedAt = workItem.CreatedAt,
                UpdatedAt = statusRecord?.UpdatedAt // 這邊對應你 Response 的 updatedAt
            };
        }
    }
}
