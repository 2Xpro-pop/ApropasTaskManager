using System;
using System.Collections.Generic;
using System.Text;

namespace ApropasTaskManager.Shared
{
    [Obsolete("ApropasTaskManager.Shared.RequestResult is deprecated, use ApropasTaskManager.Shared.Result instead")]
    public readonly struct RequestResult
    {
        public static RequestResult Error(string errorName) => new RequestResult(false, errorName);
        public static RequestResult Success() => new RequestResult(true);

        public RequestResult(bool isSuccess, string errorName = null)
        {
            IsSuccess = isSuccess;
            ErrorName = errorName;
        }

        public bool IsSuccess { get; }
        public string ErrorName { get; }

        public static implicit operator bool(RequestResult result)
        {
            return result.IsSuccess;
        }
    }
}
