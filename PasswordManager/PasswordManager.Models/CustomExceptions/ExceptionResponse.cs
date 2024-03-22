using System.Net;

namespace PasswordManager.Models.CustomExceptions
{
    public class ExceptionResponse
    {
        public ExceptionResponse(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}
