namespace E_Commerce.API.Errors
{
    public class ApiValidationErrorsResponse : BaseCommonResponse
    {
        public ApiValidationErrorsResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
