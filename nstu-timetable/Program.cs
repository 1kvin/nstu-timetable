using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using nstu_timetable;
using nstu_timetable.DbContexts;
using nstu_timetable.Services;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    AddServices(builder);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
  //  if (app.Environment.IsDevelopment())
   // {
        app.UseSwagger();
        app.UseSwaggerUI();
   // }

    //TODO Remove
    app.UseHangfireDashboard("/hangfire", new DashboardOptions()
    {
        Authorization = new[] { new HangFireAuthorizationFilter() }
    });

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapHangfireDashboard();

    AddJobs();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

void AddJobs()
{
    RecurringJob.AddOrUpdate<GroupSyncService>(g => g.StartSyncAsync(), Cron.Hourly);
}

void AddServices(WebApplicationBuilder builder)
{
    ///NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<NstuTimetableContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("nstuTimetable")));

    builder.Services.AddHangfire(configuration => configuration
                   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                   .UseSimpleAssemblyNameTypeSerializer()
                   .UseRecommendedSerializerSettings()
                   .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"),
                       new SqlServerStorageOptions
                       {
                           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                           QueuePollInterval = TimeSpan.Zero,
                           UseRecommendedIsolationLevel = true,
                           DisableGlobalLocks = true
                       }));

    builder.Services.AddHangfireServer();

    builder.Services.AddTransient<GroupSyncService>();
}