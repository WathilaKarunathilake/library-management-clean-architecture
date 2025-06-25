using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Behaviour
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.Application.Response;
    using MediatR;

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count > 0)
                {
                    var errorMessage = string.Join(" | ", failures.Select(f => f.ErrorMessage));

                    var resultType = typeof(TResponse);
                    if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        var failureResult = typeof(Result<>)
                            .MakeGenericType(resultType.GetGenericArguments())
                            .GetMethod("Failure")!
                            .Invoke(null, new object[] { errorMessage });

                        return (TResponse)failureResult!;
                    }

                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
