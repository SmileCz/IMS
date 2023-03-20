using Microsoft.Extensions.Caching.Memory;

namespace IMS.UseCases.Interfaces.Caching;

public interface ICachableRequest
{
    string CacheKey
    {
        get => string.Empty;
    }

    MemoryCacheEntryOptions? Options { get; }
}