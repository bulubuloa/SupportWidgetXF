using System;
using System.Net.Http;
using System.Threading.Tasks;
using SupportWidgetXF.Controllers.HttpClient;
using SupportWidgetXF.Models.API.Request;

namespace SupportWidgetXF.Controllers.API
{
    public class APIServiceOGPS : APIServiceAES, IAPIServiceOGPS
    {
        public async Task<TResponse> RequestAsyncWithCredential<TResponse>(RequestMethod requestMethod, string url, AESRequestBaseModel param, string apiUsername, string apiPassword)
        {
            var handler = new TimeoutHandler
            {
                DefaultTimeout = TimeSpan.FromSeconds(Utils.API_REQUEST_TIMEOUT),
                InnerHandler = new HttpClientHandler() 
                { 
                    Credentials = new System.Net.NetworkCredential(apiUsername, apiPassword) 
                }
            };
            return await ExcuteMe<TResponse>(requestMethod, url, param, handler, null);
        }
    }
}