using BusinessRule.Interfaces;
using Common.Enums;
using DataAccess.DTOs.Auth;
using DataAccess.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
    }
}
