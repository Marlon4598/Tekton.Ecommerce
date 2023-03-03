using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Tekton.Ecommerce.Transversal.Common
{
    public class Cache
    {
        private readonly IMemoryCache memoryCache;

        public Cache(IMemoryCache MemoryCache)
        {
            memoryCache = MemoryCache;
        }

        public void SetCache()
        {
            if (!memoryCache.TryGetValue(false, out Boolean _))
            {
                string statusInactive = "Inactive";

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                memoryCache.Set(false, statusInactive, cacheEntryOptions);
            }

            if (!memoryCache.TryGetValue(true, out Boolean _))
            {
                string statusActive = "Active";

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                memoryCache.Set(true, statusActive, cacheEntryOptions);
            }
        }
    }
}
