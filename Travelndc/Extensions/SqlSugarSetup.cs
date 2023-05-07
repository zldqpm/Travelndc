using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Data;
using System.Reflection;
using Travelndc.Models;
using DbType = SqlSugar.DbType;

namespace Travelndc.Extensions
{
    public static class SqlSugarSetup
    {
        /// <summary>
        /// 初始化SqlSugar
        /// </summary>
        /// <param name="builder"></param>
        public static void InitSqlSugar(this IServiceCollection services, IConfiguration config)
        {
            string? connectionString = config.GetConnectionString("ConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("请配置数据库链接字符串~");
            }

            ConnectionConfig connection = new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            };
            services.AddScoped<ISqlSugarClient>(s =>
            {
                SqlSugarClient client = new SqlSugarClient(connection);
                //client.Aop.OnLogExecuting = (s, p) =>
                //{
                //    Console.WriteLine($"OnLogExecuting:输出Sql语句:{s} || 参数为：{string.Join(",", p.Select(p => p.Value))}");
                //};
                //client.Aop.OnExecutingChangeSql = (s, p) =>
                //{
                //    Console.WriteLine($"OnLogExecuting:输出Sql语句:{s} || 参数为：{string.Join(",", p.Select(p => p.Value))}");
                //    return new KeyValuePair<string, SugarParameter[]>(s, p);
                //};
                //client.Aop.OnLogExecuted = (s, p) =>
                //{
                //    Console.WriteLine($"OnLogExecuted:输出Sql语句:{s} || 参数为：{string.Join(",", p.Select(p => p.Value))}");
                //};
                client.Aop.OnError = e =>
                {
                    Console.WriteLine($"OnError:Sql语句执行异常:{e.Message}");
                };

                return client;
            });
        }

        public static void InitDatabase(this IServiceCollection services, IConfiguration config)
        {

            //读取配置文件中的数据库链接字符串
            string? connectionString = config.GetConnectionString("ConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("请配置数据库链接字符串~");
            }
            ConnectionConfig connection = new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            };
            using (SqlSugarClient client = new SqlSugarClient(connection))
            {
                client.DbMaintenance.CreateDatabase();
                Assembly assembly = Assembly.LoadFile(Path.Combine(AppContext.BaseDirectory, "Travelndc.dll"));
                Type[] typeArray = assembly.GetTypes().Where(t => t.Namespace.Equals("Travelndc.Models"))
                    .ToArray();
                client.CodeFirst.InitTables(typeArray);
            }
        }
    }
}
