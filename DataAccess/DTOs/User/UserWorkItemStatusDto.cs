namespace DataAccess.DTOs.User
{
    public class UserWorkItemStatusDto
    {
        public int UserId { get; set; }
        public int WorkItemId { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime UpdatedAt { get; set; }
        public WorkItemDto WorkItem { get; set; }
    }
}
