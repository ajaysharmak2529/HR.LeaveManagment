using System.Collections.Generic;

namespace HR.LeaveManagement.Application.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; init; }
        public int StatusCode { get; set; }
        public T? Data { get; init; }
        public string? Message { get; init; }
        public IEnumerable<string>? Errors { get; init; }

        public static ApiResponse<T> Success(T data,int statusCode, string? message = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }
        public static ApiResponse<T> Fail(string errorMessage, int statusCode, IEnumerable<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = errorMessage,
                Errors = errors,
                StatusCode = statusCode
            };
        }

    }
}
