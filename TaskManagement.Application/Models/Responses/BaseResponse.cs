using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Models.Responses
{
    public class BaseResponse<T>
    {
        public T? Data { get; protected set; }
        public string? Message { get; protected set; }
        public bool Success { get; protected set; }
        public List<string> ErrorMessages { get; protected set; } = new List<string>();

        public BaseResponse(T data, string message)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public BaseResponse(string message)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(List<string> errorMessages)
        {
            Success = false;
            Data = default(T);
            ErrorMessages = errorMessages;
        }

    }
}
