using Microsoft.Extensions.Caching.Memory;
using TodoApp.Service.Contracts;

namespace TodoApp.Service
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheListKey = "CachedList" + typeof(T).Name;
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> Execute(int id, Func<int, Task<T>> valueGetter)
        {
            string cacheKey = GetKey(id);
            var cachedData = GetCachedView(cacheKey);
            if (cachedData is null)
            {
                var data = await valueGetter(id);
                if (data is not null)
                {
                    await CreateCacheView(cacheKey, data);
                    return data;
                }
                return data;

            }
            else
            {
                return cachedData;
            }
        }
        public async Task<List<T>> Execute(Func<Task<List<T>>> valueGetter)
        {
            var cachedData = GetCachedView();
            if (cachedData == null)
            {
                var data = await valueGetter();
                await CreateCacheViewAsync(_cacheListKey, data);
                return data;
            }
            return cachedData;
        }

        private T? GetCachedView(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out T? cachedProduct))
            {
                return cachedProduct;
            }

            return default(T);
        }

        private List<T>? GetCachedView()
        {
            if (_memoryCache.TryGetValue(_cacheListKey, out List<T>? cachedProduct))
            {
                return cachedProduct;
            }

            return null;
        }

        private async Task CreateCacheView(string cacheKey, T data)
        {
            _memoryCache.Set(cacheKey, data, TimeSpan.FromMinutes(10));
            await Task.CompletedTask;
        }

        private async Task CreateCacheViewAsync(string cacheKey, List<T> data)
        {
            _memoryCache.Set(cacheKey, data, TimeSpan.FromMinutes(10));
            await Task.CompletedTask;
        }

        public void DeleteCachedView(int id)
        {
            var cacheKey = typeof(T).Name + id.ToString();
            _memoryCache.Remove(cacheKey);
            DeleteCachedView();

        }
        public void DeleteCachedView()
        {
            _memoryCache.Remove(_cacheListKey);

        }
        public bool IsValueExists(int id)
        {
            if (_memoryCache.TryGetValue(GetKey(id), out bool result))
            {
                return true;
            }
            return false;
        }
        public bool IsListExists()
        {
            if (_memoryCache.TryGetValue(_cacheListKey, out bool result))
            {
                return true;
            }
            return false;
        }


        private string GetKey(int id)
        {
            return typeof(T).Name + id.ToString();
        }

        public T GetValue(int id)
        {
            if (!_memoryCache.TryGetValue(GetKey(id), out T? result) || result == null)
            {
                throw new Exception("Value not found in cache");
            }
            return result;
        }

        public void SetValue(int id, T value)
        {
            _memoryCache.Set(GetKey(id), value);
        }

        public List<T> GetList()
        {
            if (!_memoryCache.TryGetValue(_cacheListKey, out List<T>? result) || result == null)
            {
                throw new Exception("Value not found in cache");
            }
            return result;
        }

        public void SetList(List<T> list)
        {
           _memoryCache.Set<List<T>>(_cacheListKey, list);
        }
    }

}
