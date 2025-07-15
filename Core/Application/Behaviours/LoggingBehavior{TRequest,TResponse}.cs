// <copyright file="LoggingBehavior{TRequest,TResponse}.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Behaviours
{
    using MediatR;
    using Serilog;

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            Log.Information("Handling request: {RequestName} with payload: {@Request}", requestName, request);

            try
            {
                var response = await next();
                Log.Information("Handled request: {RequestName} with response: {@Response}", requestName, response);
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error handling request: {RequestName}", requestName);
                throw;
            }
        }
    }
}
