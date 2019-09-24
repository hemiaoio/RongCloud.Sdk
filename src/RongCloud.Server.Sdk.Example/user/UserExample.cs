using System;
using System.Threading.Tasks;
using RongCloud.Server.methods.user;
using RongCloud.Server.methods.user.tag;
using RongCloud.Server.models;
using RongCloud.Server.models.response;
using RongCloud.Server.models.user;
using RongCloud.Server.models.user.tag;

namespace RongCloud.Server.Sdk.Example.user
{
    public class UserExample

    {
        /**
         * 此处替换成您的appKey
         * */
        private static readonly string appKey = "appKey";

        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "appSecret";

        public static async Task Main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api 地址方式
            // RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);
            User User = rongCloud.User;

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/user.html#register
             *
             * 注册用户，生成用户在融云的唯一身份标识 Token
             */
            UserModel user = new UserModel
            {
                Id = "注册用户，生成用户在融云的唯一身份标识",
                Name = "username",
                Portrait = "http://www.rongcloud.cn/images/logo.png"
            };

            TokenResult result = await User.Register(user);
            Console.WriteLine("getToken:  " + result);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/user.html#refresh
             *
             * 刷新用户信息方法
             */
            Result refreshResult = await User.Update(user);
            Console.WriteLine("refresh:  " + refreshResult);

            /**
             * 用户标签
             */
            Tag tag = User.tag;
            TagModel tagModel = new TagModel("111", new[] {"一级"});
            ResponseResult re = await tag.Set(tagModel);
            Console.WriteLine(re);

            Console.WriteLine(tag.BatchSet(new TagModel(new[] {"111", "222"}, new[] {"二级", "三级", "四季"})));

            TagListResult tags = (TagListResult) await tag.Get(new[] {"111"});
            Console.WriteLine(tags);

            Console.ReadLine();
        }
    }
}