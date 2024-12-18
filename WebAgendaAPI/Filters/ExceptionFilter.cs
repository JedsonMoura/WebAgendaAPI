using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgendaAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException( ExceptionContext context)
        { 
            if (context.Exception is FluentValidation.ValidationException validationException)
            {
                var erros = validationException.Errors.Select( erro => $"{erro.PropertyName} : {erro.ErrorMessage}"  ).ToList();

                var result = new ObjectResult(new { Errors = erros })
                {
                    StatusCode = 400
                };
                context.Result = result;
                context.ExceptionHandled = true;
            }
            else if(context.Exception is HttpRequestException || context.Exception is InvalidCastException)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                context.ExceptionHandled = true;
            }
        }
    }
}
