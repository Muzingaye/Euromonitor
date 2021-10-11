using System;

namespace Store.API.Models
{
    public abstract class ErrorBase
    {
        public int ErrorCode { get; set; }
        public string ErrorString { get; set; }
        public void SetError(int errorCode, string errorString)
        {
            ErrorCode = errorCode;
            ErrorString = errorString;
        }
        public string ToErrorString(int errorCode, string errorString)
        {
            return $"ErrorCode: {errorCode} ErrorString: {errorString}";
        }

        public string LogError(Exception ex = null)
        {
            return $"ErrorCode: {ErrorCode} ErrorString: {ErrorString}";
        }
    }
}