using System;

namespace CompanyNetCore.Helper
{
    public static class Authorization
    {
       
        public static bool isAuthorised(String base64EncodedData)
        {
            var a = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
            if (a.Split(':')[0] == "Nick" && a.Split(':')[1] == "12345")
                return true;
            else
                return false;
        }
    }
}
