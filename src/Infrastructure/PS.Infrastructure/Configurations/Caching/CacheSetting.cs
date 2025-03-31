using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Configurations.Caching;

public class CacheSetting
{
    public string CacheKeyPrefix { get; set; } = "PS_";
    public string CacheType { get; set; } = "Redis";
    public RedisSetting? Redis { get; set; }
    public AzureCacheSetting? Azure { get; set; }
    public ElastiCacheSetting? Aws { get; set; }

}
