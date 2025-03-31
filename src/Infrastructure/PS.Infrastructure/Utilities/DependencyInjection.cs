using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using PS.Application.Abstractions.ICaching;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IRepositories;
using PS.Application.Abstractions.IServices;
using PS.Infrastructure.Configurations.Caching;
using PS.Infrastructure.Implementations.Caching;
using PS.Infrastructure.Implementations.Interfaces;
using PS.Infrastructure.Implementations.Repositories;
using PS.Infrastructure.Implementations.Services;
using PS.Persistence.DataContext;

using StackExchange.Redis;

namespace PS.Infrastructure.Utilities;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            var cacheSettings = new CacheSetting();
            configuration.GetSection("CacheSetting").Bind(cacheSettings);
            services.AddSingleton(cacheSettings);
            CacheKeyGenerator.Configure(cacheSettings);

            var connString = configuration.GetConnectionString("PS");
            services.AddDbContext<DBContext>(options => options.UseSqlServer(connString!));
            services.AddScoped<DapperContext>();

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ITaxBracketService, TaxBracketService>();
            services.AddScoped<ITaxRateService, TaxRateService>();
            services.AddScoped<ITaxCalculationService, TaxCalculationService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ITaxBracketRepository, TaxBracketRepository>();
            services.AddScoped<ITaxBracketLineRepository, TaxBracketLineRepository>();
            services.AddScoped<ITaxRateRepository, TaxRateRepository>();
            services.AddScoped<ITaxCalculationRepository, TaxCalculationRepository>();

            switch (cacheSettings.CacheType)
            {
                case string type when type.Equals("redis", StringComparison.OrdinalIgnoreCase):

                    services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(cacheSettings.Redis!.Configuration!));

                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = cacheSettings.Redis!.Configuration;
                        options.InstanceName = cacheSettings.Redis.InstanceName;
                    });
                    services.AddSingleton<ICacheService, RedisMultiplexerCacheService>();
                    services.AddSingleton<ICacheService, RedisCacheService>();
                    break;

                case string type when type.Equals("azure", StringComparison.OrdinalIgnoreCase):
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = cacheSettings.Azure!.ConnectionString;
                    });
                    services.AddSingleton<ICacheService, AzureCacheService>();
                    break;

                case string type when type.Equals("aws", StringComparison.OrdinalIgnoreCase):
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = cacheSettings.Aws!.Endpoint;
                    });
                    services.AddSingleton<ICacheService, ElastiCacheService>();
                    break;

                default:
                    services.AddMemoryCache();
                    services.AddSingleton<ICacheService, InMemoryCacheService>();
                    break;
            }


            return services;
        }
        catch (Exception)
        {

            throw;
        }
        
    }
}