using Common.Enums;
using Common.Extensions;

namespace DataAccess.Models.ResponseModel
{
    public class ApiResponseFactory
    {
        public static ApiResponse<T> CreateErrorResult<T>(ErrorCode errorCode, T data = default)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = EnumExtensions.GetEnumDescription(errorCode),
                Data = data,
                StatusCode = ApiStatusCode.BadRequest
            };
        }

        public static ApiResponse<T> CreateSuccessResult<T>(T data)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Message = "Success",
                Data = data,
                StatusCode = ApiStatusCode.OK
            };
        }
    }
}
