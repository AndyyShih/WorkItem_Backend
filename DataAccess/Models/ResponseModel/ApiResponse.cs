using Common.Enums;

namespace DataAccess.Models.ResponseModel
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public ApiStatusCode StatusCode { get; set; }

        public ApiResponse()
        {

        }

        public static ApiResponse<T> Create(ApiStatusCode statusCode, T data = default, string message = null, List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = statusCode < ApiStatusCode.BadRequest,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Errors = errors
            };
        }
    }
}
