using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Result
{
    public class Result<T> where T : class
    {
        public T? Value { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public List<string> ModelsErrors { get; private set; }

        public Result(T data)
        {
            Value = data;
            IsSuccess = true;
            ErrorMessage = string.Empty;
            ModelsErrors = [];
        }

        public Result(string errorMessage)
        {
            Value = default!;
            IsSuccess = false;
            ErrorMessage = errorMessage;
            ModelsErrors = [];

        }
        public Result(List<string> errorModels)
        {
            Value = default!;
            IsSuccess = false;
            ErrorMessage = "Error Models";
            ModelsErrors = errorModels;
            
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data);
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(errorMessage);
        }
        public static Result<T> FailureModels(List<string> errorModels)
        {
            return new Result<T>(errorModels);
        }
    }
}
