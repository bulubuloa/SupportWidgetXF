using System;
namespace SupportWidgetXF.Models.API.Response
{
    public abstract class AESResponseBaseModel : BaseModel
    {
        public string Version { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { set; get; }

        public AESResponseBaseModel()
        {
        }
    }
}