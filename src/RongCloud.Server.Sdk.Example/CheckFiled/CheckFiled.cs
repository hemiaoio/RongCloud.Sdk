using System;
using RongCloud.Server.models.user;
using RongCloud.Server.util;

namespace RongCloud.Server.Sdk.Example.CheckFiled
{
    public class CheckFiled
    {
        public static void Main(string[] args)
        {
            UserModel user = new UserModel("user_id",
                "user_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_nameuser_name",
                "url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url_url");
            string msg = CommonUtil.CheckFiled(user, "user", "register");
            Console.WriteLine(msg);
            Console.ReadLine();
        }
    }
}