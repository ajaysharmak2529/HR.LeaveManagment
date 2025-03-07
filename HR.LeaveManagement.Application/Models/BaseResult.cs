using System.Collections.Generic;

namespace HR.LeaveManagement.Application.Models
{
    public class BaseResult<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public string? Message { get; init; }
        public IEnumerable<string>? Errors { get; init; }

        public static BaseResult<T> Success(T data, string? message = null)
        {
            return new BaseResult<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
            };
        }
        public static BaseResult<T> Fail(string errorMessage, IEnumerable<string>? errors = null)
        {
            return new BaseResult<T>
            {
                IsSuccess = false,
                Message = errorMessage,
                Errors = errors,
            };
        }
    }
}
