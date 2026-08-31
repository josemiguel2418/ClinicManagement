using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Backend.API.Results
{
    public class ApiResults
    {
        public static ActionResult MapProblems(Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }
            var statusCode = GetStatusCode(result.Error.Type);
            var problemDetails = new ProblemDetails
            {
                Title = result.Error.Code,
                Detail = result.Error.Description,
                Status = statusCode
            };
            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode
            };
        }
  
        private static int GetStatusCode(ErrorType type)
        {
            return type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
