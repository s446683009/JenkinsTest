using System.Net;
using System.Reflection;
using Category.Api.Db;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Services.Common;
using ILogger = Microsoft.Extensions.Logging.ILogger;

static Serilog.ILogger CreateSerilogLogger()
{
    //var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    //var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.WithProperty("ApplicationContext", typeof(Program).Namespace)
        .Enrich.FromLogContext()
        .WriteTo.File("./log/log-.txt", rollingInterval: RollingInterval.Hour)
        .CreateLogger();
}

var builder = WebApplication.CreateBuilder(args);
//add serilog 
var logger = CreateSerilogLogger();
builder.Host.UseSerilog(logger);
var connectString = builder.Configuration.GetConnectionString("category");
// Add services to the container.
builder.Services.AddDbContext<CategoryDbContext>(t => {
    t.UseNpgsql(connectString);
});
builder.Services.AddControllers(options =>
{
    
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
               
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();