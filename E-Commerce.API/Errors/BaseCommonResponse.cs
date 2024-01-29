
namespace E_Commerce.API.Errors
{
    public class BaseCommonResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public BaseCommonResponse(int statusCode, string statusMessage = null)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage ?? DefaultMessageForStatusCode(statusCode);
        }

        private string DefaultMessageForStatusCode(int statusCode)
        => statusCode switch
        {
            400 => "Bad Request",
            401 => "Not Authorize",
            404 => "Resource Not Found",
            500 => "Server Error",
            _ => null
        };


    }
}
