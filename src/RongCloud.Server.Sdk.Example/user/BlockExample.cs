using System;
using System.Threading.Tasks;
using RongCloud.Server.methods.user.block;
using RongCloud.Server.models;
using RongCloud.Server.models.response;
using RongCloud.Server.models.user;

namespace RongCloud.Server.Sdk.Example.user
{
    public class BlockExample

    {
        /**
         * 此处替换成您的appKey
         * */
        private static readonly string appKey = "appKey";

        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "appSecret";

        /**
         * 自定义api地址
         * */
        private static readonly string api = "http://api.cn.ronghub.com";

        static async Task Main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api 地址方式
            // RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Block block = rongCloud.User.block;

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/block.html#add
             * 解除用户封禁
             *
             */
            UserModel user = new UserModel()
            {
                Id = "hkjo09h",
                Minute = 1000
            };

            Result addBlockResult = (ResponseResult) await block.Add(user);
            Console.WriteLine("userAddBlock:  " + addBlockResult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/block.html#remove
             * 解除用户封禁
             *
             */
            ResponseResult unBlockResult = await block.Remove(user.Id);
            Console.WriteLine("unBlock:  " + unBlockResult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/user/block.html#getList
             * 获取被封禁用户
             *
             */
            BlockUserResult blockResult = await block.GetList();
            Console.Write("queryBlock:  " + blockResult);

            Console.ReadLine();
        }
    }
}