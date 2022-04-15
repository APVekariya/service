using Microsoft.Extensions.Logging.EventLog;
 using Serilog;
using Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
        services.AddHostedService<SensorService>();

        if (OperatingSystem.IsWindows())
        {
            services.Configure<EventLogSettings>(config =>
            {

                if (OperatingSystem.IsWindows())
                {
                    config.LogName = "Avis Sensor Service";
                    config.SourceName = "Avis Sensor Service Source";
                }
            });  
        }
    })
    .Build();

string logsDirectory = Path.Combine(Environment.CurrentDirectory, "logs");


// Configure Serilog pipeline
Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Debug()
   .WriteTo.RollingFile(Path.Combine(logsDirectory, "Log_{Date}.txt"))
   .CreateLogger();

await host.RunAsync();
