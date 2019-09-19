using System;
using System.Threading.Tasks;
using RongCloud.Server.methods.chatroom.ban;
using RongCloud.Server.models.chatroom;
using RongCloud.Server.models.response;

namespace RongCloud.Server.Sdk.Example.chatroom
{
    public class BanExample

    {
        /**
         * 此处替换成您的appKey
         * */
        private static readonly string appKey = "kj7swf8okyqt2";

        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "mFe3U1UClx4gx";

        /**
         * 自定义api地址C:\Users\rc\Downloads\server-sdk-dotnet-master\example\chatroom\BanExample.cs
         * */
        private static readonly string api = "http://api.cn.ronghub.com";

        static async Task Main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api地址方式
            //RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Ban ban = rongCloud.Chatroom.ban;

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/ban.html#add
             * 添加聊天室全局禁言
             * */
            ChatroomMember[] members =
            {
                new ChatroomMember()
                {
                    Id = "qawr34h"
                },
                new ChatroomMember()
                {
                    Id = "qawr35h"
                }
            };
            ChatroomModel chatroom = new ChatroomModel()
            {
                Members = members,
                Minute = 5
            };

            ResponseResult result = await ban.Add(chatroom);
            Console.WriteLine("addGagUser:  " + result);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/ban.html#getList
             * 获取聊天时全局禁言列表
             */

            ListGagChatroomUserResult chatroomListGagUserResult = await ban.GetList();
            Console.WriteLine("ListGagUser:  " + chatroomListGagUserResult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/ban.html#remove
             * 删除聊天时全局禁言
             */
            chatroom = new ChatroomModel()
            {
                Members = members
            };
            ResponseResult removeResult = await ban.Remove(chatroom);
            Console.WriteLine("removeBanUser:  " + removeResult);
            Console.ReadLine();
        }
    }
}