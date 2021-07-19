using Microsoft.OpenApi.Any;

namespace Verification.Data
{
    public class BaseResultModel
    {
        public int? Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public BaseResultModel(int? status,
            object data, string message = null)
        {
            Status = status;
            Data = data;
            Message = message;
        }
    }
}