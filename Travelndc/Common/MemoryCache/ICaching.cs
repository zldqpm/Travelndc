namespace Travelndc.Common.MemoryCache
{
    /// <summary>
    ///缓存
    /// </summary>
    public interface ICaching
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue, int timeSpan);
    }
}
