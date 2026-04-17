using BusinessRule.Interfaces;
using Common.Enums;
using DataAccess.DTOs.Auth;
using DataAccess.Models.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace WorkItem_Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ApiResponse<LoginResultDto>> LoginAsync(LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request.Username, request.Password);

            if (result == null)
            {
                Log.Error("登入失敗：帳號或密碼錯誤");
                return ApiResponseFactory.CreateErrorResult<LoginResultDto>(ErrorCode.DATA_EMPTY, result);
            }
            else
            {
                Log.Information($"登入成功：{result.Username}");
                return ApiResponseFactory.CreateSuccessResult<LoginResultDto>(result);
            }
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<ApiResponse<LoginResultDto>> GetProfileAsync()
        {
            var userIdStr = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                Log.Error("Token 內無效的使用者識別碼");
                return ApiResponseFactory.CreateErrorResult<LoginResultDto>(ErrorCode.DATA_EMPTY);
            }

            var result = await _authService.GetUserProfileAsync(userId);

            if (result == null)
            {
                Log.Error($"查無此使用者，ID: {userId}");
                return ApiResponseFactory.CreateErrorResult<LoginResultDto>(ErrorCode.DATA_EMPTY);
            }

            Log.Information($"成功取得使用者資料，ID: {userId}");
            return ApiResponseFactory.CreateSuccessResult(result);
        }
    }
}
