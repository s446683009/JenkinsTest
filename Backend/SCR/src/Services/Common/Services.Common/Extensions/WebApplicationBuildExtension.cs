using Serilog;

namespace Services.Common.Extensions;

public static class WebApplicationBuildExtension
{
    public static Serilog.ILogger CreateSerilogLogger(Type proType)
    {
        //var seqServerUrl = configuration["Serilog:SeqServerUrl"];
        //var logstashUrl = configuration["Serilog:LogstashgUrl"];
        return new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithProperty("ApplicationContext", proType.Namespace)
            .Enrich.FromLogContext()
            .WriteTo.File("./log/log-.txt", rollingInterval: RollingInterval.Hour)
            .CreateLogger();
    }
    
}