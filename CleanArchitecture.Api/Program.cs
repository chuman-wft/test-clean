using CleanArchitecture.Api;
using Serilog;
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information("Clean Architecture Api Starting");
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.WriteTo.Console().ReadFrom.Configuration(context.Configuration));
var app = builder.ConfigureServices().ConfigurePipeline();



app.Run();
