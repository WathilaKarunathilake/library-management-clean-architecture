// <copyright file="ValidationBehavior{TRequest,TResponse}.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Behaviour
{
    using FluentValidation;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Domain.Errors;
    using MediatR;

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (this.validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(this.validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count > 0)
                {
                    var errorMessage = string.Join(" | ", failures.Select(f => f.ErrorMessage));

                    var resultType = typeof(TResponse);
                    if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        var error = Error.Failure("ValidationError", errorMessage);

                        var failureResult = typeof(Result<>)
                            .MakeGenericType(resultType.GetGenericArguments())
                            .GetMethod("Failure") !
                            .Invoke(null, new object[] { error });

                        return (TResponse)failureResult!;
                    }

                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
