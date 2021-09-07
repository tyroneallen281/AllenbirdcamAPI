namespace ABC.Domain.Models
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
        }

        public ApiErrorResponse(string errorMessage, string errorDetail, string errorType)
        {
            ErrorMessage = errorMessage;
            ErrorDetail = errorDetail;
            ErrorType = errorType;
        }

        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorType { get; set; }
    }
}