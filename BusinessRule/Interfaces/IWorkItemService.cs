using DataAccess.DTOs.WorkItem;

namespace BusinessRule.Interfaces
{
    public interface IWorkItemService
    {
        /// <summary>
        /// 取得工作項目列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<WorkItemDto>> GetUserWorkItemsAsync(int userId);

        /// <summary>
        /// 取得工作項目詳細資訊，包含使用者的確認狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<WorkItemDetailDto?> GetWorkItemDetailAsync(int id, int userId);

        /// <summary>
        /// 批次更新使用者對多個工作項目的確認狀態
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workItemIds"></param>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        Task<bool> BatchUpdateStatusAsync(int userId, List<int> workItemIds, bool isConfirmed);
    }
}
