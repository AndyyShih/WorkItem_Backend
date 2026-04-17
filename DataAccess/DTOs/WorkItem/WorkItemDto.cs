namespace DataAccess.DTOs.WorkItem
{
    public class WorkItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        // 額外欄位：顯示當前登入者是否已確認
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedAt { get; set; }
    }
}
