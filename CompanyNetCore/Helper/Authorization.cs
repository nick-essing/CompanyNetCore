using System;

namespace CompanyNetCore.Helper
{
    public static class Authorization
    {
        private static string[] adminUsername = {"Nick", "Admin2" };
        private static string[] adminPw = {"12345", "admin" };
        public static bool isAuthorised(String base64EncodedData)
        {
            var a = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
            for (int i = 0; i < adminUsername.Length; i++)
            {
                if (a.Split(':')[0] == adminUsername[i] && a.Split(':')[1] == adminPw[i])
                    return true;
            }
            return false;
        }
    }
}
