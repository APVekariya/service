using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SensorService : BackgroundService
    {
        private readonly ILogger<SensorService> _logger;
        private readonly IConfiguration _configuration;
        public string BaseURL { get { return _configuration.GetSection("BaseURL").Value; } }

        public SensorService(ILogger<SensorService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }

        public void StartCapture()
        {
            try
            {
                Log.Information($"Target API Url : {BaseURL}");
                Log.Information("StartCapture Event Called ");

                ///TODO 
                ///API Calling Logic Here 
                ///
            }
            catch (Exception ex) { };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string[] args = Environment.GetCommandLineArgs();
            Log.Information($"args : {string.Join(" ", args)}");
            int delay =1000;
            int.TryParse(args[0],out delay);
            Log.Information($"Interval : {delay}");
            new Thread(this.StartCapture).Start();
            await Task.Delay(delay, stoppingToken);

        }
    }

}