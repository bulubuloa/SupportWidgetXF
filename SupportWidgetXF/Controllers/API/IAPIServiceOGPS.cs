using System;
using System.Threading.Tasks;
using SupportWidgetXF.Models.API.Request;

namespace SupportWidgetXF.Controllers.API
{
    public interface IAPIServiceOGPS
    {
        Task<TResponse> RequestAsyncWithCredential<TResponse>(RequestMethod requestMethod, string url, AESRequestBaseModel param, string apiUsername, string apiPassword);
    }

}