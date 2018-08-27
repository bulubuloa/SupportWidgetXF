using System;
using System.Threading.Tasks;
using SupportWidgetXF.Models.API.Request;

namespace SupportWidgetXF.Controllers.API
{
    public enum RequestMethod
    {
        GET, POST, PUT
    }

    public interface IAPIService
    {
        Task<TResponse> RequestPostAsync<TResponse>(string url, AESRequestBaseModel param, string token = "");
        Task<TResponse> RequesGetAsync<TResponse>(string url, AESRequestBaseModel param, string token = "");
        Task<TResponse> RequesPutAsync<TResponse>(string url, AESRequestBaseModel param, string token = "");
        Task<TResponse> RequestAsync<TResponse>(RequestMethod requestMethod, string url, AESRequestBaseModel param, string token = "");
    }
}
