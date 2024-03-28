using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sympli.Core.Exceptions;

namespace Aswhiteglobal.Sympli.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;
        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
             {
                 { typeof(GoogleErrorException), HandleGoogleErrorException },
                 { typeof(BingErrorException), HandleBingErrorException },
             };
        }


        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            _logger.LogError("Unhandled exception:\n{Exception}", context.Exception);
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
            context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        private void HandleGoogleErrorException(ExceptionContext context)
        {
            var exception = context.Exception as GoogleErrorException;
            _logger.LogCritical("Google failed exception:\n{Exception}", context.Exception);
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = $"{exception.Message}",
                Type = typeof(GoogleErrorException).Name
            };
            context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }

        private void HandleBingErrorException(ExceptionContext context)
        {
            var exception = context.Exception as BingErrorException;
            _logger.LogCritical("Bing failed exception:\n{Exception}", context.Exception);
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = $"{exception.Message}",
                Type = typeof(BingErrorException).Name
            };
            context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }
    }
}