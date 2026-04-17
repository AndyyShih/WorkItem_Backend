using DataAccess.Models;

namespace DataAccess.IRepository
{
    public interface IWorkItemRepository
    {
        /// <summary>
        /// 取得所有工作項目，並包含指定使用者的確認狀態
        /// </summary>
        Task<IEnumerable<WorkItem>> GetListWithStatusAsync(int userId);

        /// <summary>
        /// 取得工作項目詳細資訊，並包含指定使用者的確認狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<WorkItem?> GetDetailWithStatusAsync(int id, int userId);

        /// <summary>
        /// 批次更新或新增使用者的工作狀態
        /// </summary>
        Task<bool> UpsertUserStatusesAsync(int userId, List<int> workItemIds, bool isConfirmed);
    }
}
