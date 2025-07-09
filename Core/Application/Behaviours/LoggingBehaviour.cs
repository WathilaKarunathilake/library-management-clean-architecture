using MediatR;
using Serilog;

namespace LibraryManagementCleanArchitecture.Application.Behaviours
{
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
