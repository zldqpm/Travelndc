using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Travelndc.Common.MemoryCache;
using Travelndc.Extensions;

namespace Travelndc
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("3.1是和2.1一起处理了，用的是SqlSugar这个orm框架，如果超过两次获取数据失败，需要重新登录一下");
            //加载配置文件
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", true, true);
            var ConfigRoot = builder.Build();//根节点
            //IOC
            IServiceCollection Services = new ServiceCollection();
            //缓存
            Services.AddMemoryCacheSetup();
            //SqlSugar
            Services.InitDatabase(ConfigRoot);
            Services.InitSqlSugar(ConfigRoot);
            ServiceProvider provider = Services.BuildServiceProvider();
            var cachingServices = provider.GetRequiredService<ICaching>();
            var client = provider.GetRequiredService<ISqlSugarClient>();
            //获取配置的超时时间
            var Timeout = ConfigRoot.GetValue<int>("Timeout");
            TravelndcServices TravelndcServices = new TravelndcServices(cachingServices, Timeout, client);
            await TravelndcServices.Main();
        }
    }
}


