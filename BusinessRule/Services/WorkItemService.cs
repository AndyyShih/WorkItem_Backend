using BusinessRule.Interfaces;
using DataAccess.DTOs.WorkItem;
using DataAccess.IRepository;
using DataAccess.Models;

namespace BusinessRule.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;

        public WorkItemService(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        /// <summary>
        /// 取得工作項目列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 取得工作項目詳細資訊，包含使用者的確認狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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
                UpdatedAt = statusRecord?.UpdatedAt
            };
        }

        /// <summary>
        /// 批次更新使用者對多個工作項目的確認狀態
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workItemIds"></param>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        public async Task<bool> BatchUpdateStatusAsync(int userId, List<int> workItemIds, bool isConfirmed)
        {
            if (workItemIds == null || !workItemIds.Any())
            {
                return false;
            }

            return await _workItemRepository.UpsertUserStatusesAsync(userId, workItemIds, isConfirmed);
        }

        /// <summary>
        /// 新增工作項目
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<bool> CreateWorkItemAsync(CreateWorkItemReq req)
        {
            var entity = new WorkItem
            {
                Title = req.Title,
                Description = req.Description,
                CreatedAt = DateTime.Now
            };
            return await _workItemRepository.AddAsync(entity);
        }

        /// <summary>
        /// 修改工作項目
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWorkItemAsync(UpdateWorkItemReq req)
        {
            // 這裡可以先檢查 ID 是否存在
            var entity = new WorkItem
            {
                Id = req.Id,
                Title = req.Title,
                Description = req.Description
            };
            return await _workItemRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 刪除工作項目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteWorkItemAsync(int id)
        {
            return await _workItemRepository.DeleteAsync(id);
        }
    }
}
