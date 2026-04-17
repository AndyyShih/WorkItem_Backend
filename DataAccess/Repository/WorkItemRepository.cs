using DataAccess.IRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly WorkItemContext _context;

        public WorkItemRepository(WorkItemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkItem>> GetListWithStatusAsync(int userId)
        {
            // 撈取所有工作項目，並過濾出屬於該使用者的狀態資料
            return await _context.WorkItems
                .Include(w => w.UserWorkItemStatuses.Where(s => s.UserId == userId))
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
        }

        public async Task<WorkItem?> GetDetailWithStatusAsync(int id, int userId)
        {
            return await _context.WorkItems
                .Include(w => w.UserWorkItemStatuses.Where(s => s.UserId == userId))
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> UpsertUserStatusesAsync(int userId, List<int> workItemIds, bool isConfirmed)
        {
            var now = DateTime.Now;

            var existingStatuses = await _context.UserWorkItemStatuses
                .Where(s => s.UserId == userId && workItemIds.Contains(s.WorkItemId))
                .ToListAsync();

            foreach (var status in existingStatuses)
            {
                status.IsConfirmed = isConfirmed;
                status.UpdatedAt = now;
            }

            var existingIds = existingStatuses.Select(s => s.WorkItemId).ToList();
            var newIds = workItemIds.Except(existingIds);

            foreach (var id in newIds)
            {
                _context.UserWorkItemStatuses.Add(new UserWorkItemStatus
                {
                    UserId = userId,
                    WorkItemId = id,
                    IsConfirmed = isConfirmed,
                    UpdatedAt = now
                });
            }

            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
