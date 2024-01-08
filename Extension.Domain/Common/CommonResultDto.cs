using System.Net;
using static Extension.Domain.Enum.ExtensionEnums;

namespace Extension.Domain.Common
{
    public class CommonResultDto
    {
        public bool IsSuccessful { get; set; } = false;
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public MessageCode MessageCode { get; set; }
    }

    public class CommonResultDto<T> : CommonResultDto
    {
        public T DataResult { get; set; }

        public CommonResultDto(T dataSuccess)
        {
            DataResult = dataSuccess;
            IsSuccessful = true;
            StatusCode = HttpStatusCode.OK;
            ErrorMessage = string.Empty;
        }

        public CommonResultDto(string errorMessage)
        {
            IsSuccessful = false;
            ErrorMessage = errorMessage;
        }

        public CommonResultDto(HttpStatusCode statusCode, string errorMessage)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public CommonResultDto() : base()
        {
            IsSuccessful = true;
        }

        public void SetDataSuccess(T data)
        {
            DataResult = data;
            IsSuccessful = true;
            StatusCode = HttpStatusCode.OK;
            ErrorMessage = string.Empty;
        }
    }
}
