using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;

namespace SupportWidgetXF.Models.API.Request
{
    public abstract class AESRequestBaseModel : BaseModel
    {
        public virtual string Get_GetParamsRequest()
        {
            List<string> listItems = new List<string>();
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var valueP = property.GetValue(this, null);
                if (valueP != null)
                {
                    listItems.Add(property.Name + "=" + valueP.ToString());
                }
            }
            return "?" + String.Join("&", listItems);
        }

        public virtual string Get_PostParamsRequest()
        {
            return JsonConvert.SerializeObject(this);
        }

        public virtual string Get_PutParamsRequest()
        {
            return JsonConvert.SerializeObject(this);
        }

        public virtual FormUrlEncodedContent Get_PostParamsRequestEncoded()
        {
            return null;
        }
    }
}