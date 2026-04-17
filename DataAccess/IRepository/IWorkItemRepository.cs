using DataAccess.Models;

namespace DataAccess.IRepository
{
    public interface IWorkItemRepository
    {
        /// <summary>
        /// 取得所有工作項目，並包含指定使用者的確認狀態
        /// </summary>
        Task<IEnumerable<WorkItem>> GetListWithStatusAsync(int userId);
    }
}
