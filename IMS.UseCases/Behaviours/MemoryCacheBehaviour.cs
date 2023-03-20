using IMS.UseCases.Interfaces.Caching;
using LazyCache;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IMS.UseCases.Behaviours;

public class MemoryCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachableRequest
{
    private readonly IAppCache _cache;
    private readonly ILogger<MemoryCacheBehaviour<TRequest, TResponse>> _logger;

    public MemoryCacheBehaviour(IAppCache cache, ILogger<MemoryCacheBehaviour<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("{Name} is caching with request {@Request}", nameof(request), request);
        var response = await _cache.GetOrAddAsync(
            request.CacheKey,
            async () => await next(),
            request.Options).ConfigureAwait(false);

        return response;
    }
}