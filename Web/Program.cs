using Core;
using Core.Identity;
using Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace Web
{
    public class LogEntity
    {
        public int id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
           // ConfigureLogging();
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile(
            //        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            //        optional: true)
            //    .Build();
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            //    {
            //        ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "bL6kk0hzokMYh-YQoOef"),
            //        AutoRegisterTemplate = true,
            //        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            //        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{DateTime.UtcNow:yyyy-MM}"
            //    })
            //    .Enrich.WithProperty("Environment", environment)
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

             // CreateHostBuilder(args).Build().Run();
            CreateHost(args);
        }
        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
             //   .ConfigureAppConfiguration(configuration =>
             //   {
             //       configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
             //       configuration.AddJsonFile(
             //           $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
             //   })
             //.UseSerilog();




        private static void ConfigureLogging()
        {
            var settings = new ElasticsearchClientSettings(new Uri("https://10.40.2.118:9200/"))
                   .CertificateFingerprint("E7:0E:B9:7C:91:56:EC:40:E1:46:BC:16:97:80:5F:DC:13:05:29:4E:0E:FF:24:9F:75:B7:FF:2B:22:26:67:24")
                   .Authentication(new BasicAuthentication("elastic", "bL6kk0hzokMYh-YQoOef"));

            var client = new ElasticsearchClient(settings);

           

            var indexResponse2 = client.Index(Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-"),i => i.Index("cleanlog1"));
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "bL6kk0hzokMYh-YQoOef"),
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
