using BloodBank.Services;
using Microsoft.Extensions.Options;

public class SmsSender : ISmsSender
{
    private readonly SmsSettings _smsSettings;

    public SmsSender(IOptions<SmsSettings> smsSettings)
    {
        _smsSettings = smsSettings.Value;
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        using (var httpClient = new HttpClient())
        {
            var values = new Dictionary<string, string>
            {
                { "to", phoneNumber },
                { "message", message },
                { "api_key", _smsSettings.ApiKey }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await httpClient.PostAsync(_smsSettings.ApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to send SMS.");
            }
        }
    }
}


//using System.Threading.Tasks;
//using System.Net.Http;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Mail;
//namespace BloodBank.Services
//{
//    public class SmsSender : ISmsSender
//    {
//        private readonly string _smsApiUrl = "https://api.smsprovider.com/send";
//        private readonly string _apiKey = "your-api-key";

//        public async Task SendSmsAsync(string phoneNumber, string message)
//        {
//            using (var httpClient = new HttpClient())
//            {
//                var values = new Dictionary<string, string>
//                {
//                    { "to", phoneNumber },
//                    { "message", message },
//                    { "api_key", _apiKey }
//                };

//                var content = new FormUrlEncodedContent(values);

//                var response = await httpClient.PostAsync(_smsApiUrl, content);

//                if (!response.IsSuccessStatusCode)
//                {
//                    throw new Exception("Failed to send SMS.");
//                }
//            }
//        }
//    }
//}
