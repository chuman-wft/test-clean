

using Serilog;
using Serilog.Events;
using CleanArchitecture.Api.Middleware;
using CleanArchitecture.Persistence;
using CleanArchitecture.Application;

namespace CleanArchitecture.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
            this WebApplicationBuilder builder
        )
        {

            DbUpMigrator.MigrateDatabase(builder.Configuration);

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging(loggingBuilder =>
            {
                var environment = builder.Environment;
                loggingBuilder.ClearProviders();
                if (environment.IsProduction())
                    loggingBuilder.AddSerilog(new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .MinimumLevel.Debug()
                        // .WriteTo.Console()
                        .WriteTo.Sentry(o =>
                        {
                            o.Dsn = builder.Configuration["ConnectionString:Sentry:DSN"];
                            o.Debug = false;
                            o.MinimumBreadcrumbLevel = LogEventLevel.Error;
                            o.MinimumEventLevel = LogEventLevel.Error;
                            // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
                            // We recommend adjusting this value in production.
                            o.TracesSampleRate = 1.0;
                            // Enable Global Mode if running in a client app
                            o.IsGlobalModeEnabled = false;
                            o.MinimumEventLevel = LogEventLevel.Error;

                        })
                    .CreateLogger());


                Log.CloseAndFlush();
            });
            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            app.UseSerilogRequestLogging();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.DisplayRequestDuration();

            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseCors("Open");
            app.MapControllers();

            return app;
        }
    }





}
