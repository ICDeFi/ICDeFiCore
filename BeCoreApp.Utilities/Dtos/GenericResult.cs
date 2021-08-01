using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Utilities.Dtos
{
    public class GenericResult
    {
        public GenericResult()
        {
        }

        public GenericResult(bool success)
        {
            Success = success;
        }

        public GenericResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public GenericResult(bool success, object data)
        {
            Success = success;
            Data = data;
        }

        public GenericResult(bool success, string message, object data)
        {
            Message = message;
            Success = success;
            Data = data;
        }

        public object Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public static GenericResult ToSuccess(string message, object data) => new GenericResult(success: true, message: message, data: data);

        public static GenericResult ToSuccess() => new GenericResult(success: true);

        public static GenericResult ToFail(string message) => new GenericResult(success: false, message: message);
    }
}
