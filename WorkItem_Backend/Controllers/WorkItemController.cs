using BusinessRule.Interfaces;
using Common.Enums;
using DataAccess.DTOs.WorkItem;
using DataAccess.Models.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WorkItem_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemService _workItemService;

        public WorkItemController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        /// <summary>
        /// 取得當前登入者的工作清單
        /// </summary>
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<WorkItemDto>>> GetListWorkItemAsync()
        {
            var userIdStr = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                Log.Error("WorkItem GetList: Token 內無效的使用者識別碼");
                return ApiResponseFactory.CreateErrorResult<IEnumerable<WorkItemDto>>(ErrorCode.DATA_EMPTY);
            }

            var result = await _workItemService.GetUserWorkItemsAsync(userId);

            Log.Information($"使用者 {userId} 成功取得工作清單，共 {result.Count()} 筆");

            return ApiResponseFactory.CreateSuccessResult(result);
        }
    }
}
