namespace DataAccess.DTOs.WorkItem
{
    public class CreateWorkItemReq
    {
        /// <summary>
        /// 工作項目標題 (例如：伺服器例行維護)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 詳細描述
        /// </summary>
        public string Description { get; set; }
    }
}
