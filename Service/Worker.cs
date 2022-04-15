using Serilog;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        public string BaseURL { get { return this._configuration["AppSeettings:BaseURL"]; } }

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information($"Target API Url : {_configuration.GetSection("BaseURL").Value}");
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

            }
        }

        //public void PushtoAPI(SensorData sensorData)
        //{
        //    var Url = BaseURL + "/api/SensorData";
        //    var urlStr = string.Empty;

        //    try
        //    {
        //        this.Call<Dictionary<string, SensorData>>(Url, WebRequestMethods.Http.Post, sensorData);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (enableEventLog)
        //        {
        //            EventLog.WriteEntry("Sensor Data ", ex.Message + " " + urlStr);
        //        }
        //    }
        //}
        //private T Call<T>(string url, string methodType, T data) where T : class
        //{
        //    T result;

        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Method = methodType;
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    if (methodType == WebRequestMethods.Http.Put || methodType == WebRequestMethods.Http.Post)
        //    {
        //        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        //        string jsonData = javaScriptSerializer.Serialize(data);

        //        byte[] arrData = Encoding.UTF8.GetBytes("JsonData=[" + jsonData + "]");
        //        request.ContentLength = arrData.Length;

        //        using (Stream dataStream = request.GetRequestStream())
        //        {
        //            dataStream.Write(arrData, 0, arrData.Length);
        //        }
        //    }

        //    // Note: You may not need to parse any response content,
        //    // or it may be a different class
        //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        using (StreamReader reader
        //                          = new StreamReader(response.GetResponseStream()))
        //        {
        //            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        //            string jsonData = reader.ReadToEnd();
        //            result = (T)javaScriptSerializer.Deserialize<T>(jsonData);
        //        }
        //    }

        //    return result;
        //}
    }
}
