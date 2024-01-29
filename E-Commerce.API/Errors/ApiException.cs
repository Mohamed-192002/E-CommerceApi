namespace E_Commerce.API.Errors
{
    public class ApiException : BaseCommonResponse
    {
        public ApiException(int statusCode, string statusMessage = null, string details = null) : base(statusCode, statusMessage)
        {
            Details = details;
        }
        public string Details { get; set; }

    }
}
