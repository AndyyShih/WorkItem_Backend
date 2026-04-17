namespace DataAccess.DTOs.WorkItem
{
    public class BatchConfirmReq
    {
        // 傳入要確認的 ID 列表
        public List<int> WorkItemIds { get; set; }
    }
}
