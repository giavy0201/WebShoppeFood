﻿namespace WebAPI.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public static ApiResponse<T> SuccessCRUD(string message)
        {
            return new ApiResponse<T>(true, message ?? "Thành Công", default(T));
        }
        public static ApiResponse<T> Success(string message, T data)
        {
            return new ApiResponse<T>(true, message ?? "Success", data);
        }
        public static ApiResponse<T> BadRequest(string message)
        {
            return new ApiResponse<T>(false, message ?? "Validate Input Failed", default(T));
        }
        public static ApiResponse<T> Delete(string message)
        {
            return new ApiResponse<T>(true, message ?? "Success", default(T));
        }
        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>(false, message ?? "Error Server", default(T));
        }
        public static ApiResponse<T> NotFound(string message)
        {
            return new ApiResponse<T>(false, message ?? "Not Found", default(T));
        }
    }
}
