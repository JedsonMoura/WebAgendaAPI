using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contacts.Commands.Validations
{
    public class ValidationBehaviour<TRequest, TReponse> : IPipelineBehavior<TRequest, TReponse> where TRequest : IRequest<TReponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validatorResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validatorResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();


                if (failures.Count > 0)
                {
                    throw new FluentValidation.ValidationException(failures);
                }

            }
            return await next();
        }
    }
}
