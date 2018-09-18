using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CompanyNetCore.Helper
{
    public static class Authorization
    {
        public static bool isAuthorised(String Token)
        {
            try
            {
                var a = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(Token.Split('.')[1]));
                var data = (JObject)JsonConvert.DeserializeObject(a);
                var asda = data.SelectToken("IsAdmin").ToString();
                if (data.SelectToken("IsAdmin").ToString() == "True")
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
