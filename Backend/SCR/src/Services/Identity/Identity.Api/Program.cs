using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = CreateSerilogLogger();
            var host = CreateHostBuilder(args).UseSerilog(logger).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                   
                });
        static Serilog.ILogger CreateSerilogLogger()
        {
            //var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            //var logstashUrl = configuration["Serilog:LogstashgUrl"];
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithProperty("ApplicationContext", typeof(Program).Namespace)
                .Enrich.FromLogContext()
                .WriteTo.Console().WriteTo.File("./log/log-.txt", rollingInterval: RollingInterval.Day)
                //.WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                //.WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://localhost:8080" : logstashUrl)
                //.ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

    }
}
