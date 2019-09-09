using System;
using io.rong.models.push;
using io.rong.util;

namespace RongCloud.Sdk.Server.NetStandard.Example.CheckFiled
{
    public class CheckFiled
    {

        static void Main(string[] args)
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