using BusinessRule.Interfaces;
using Common.Enums;
using DataAccess.DTOs.User;
using DataAccess.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WorkItem_Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("GetUser")]
        public async Task<ApiResponse<GetUserOutputDto>> GetUserAsync(GetUserInputDto input)
        {
            var result = await _userService.GetUserAsync(input);

            if (result == null)
            {
                Log.Error("查無資料");
                return ApiResponseFactory.CreateErrorResult<GetUserOutputDto>(ErrorCode.DATA_EMPTY, result);
            }
            else
            {
                Log.Information("查詢完成");
                return ApiResponseFactory.CreateSuccessResult<GetUserOutputDto>(result);

            }
        }
    }
}