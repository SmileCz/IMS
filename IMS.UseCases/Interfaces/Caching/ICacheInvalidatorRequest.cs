namespace IMS.UseCases.Interfaces.Caching;

public interface ICacheInvalidatorRequest
{
    string CacheKey
    {
        get => string.Empty;
    }

    CancellationTokenSource? SharedExpiryTokenSource { get; }
}