using System;
using System.Threading.Tasks;
using RongCloud.Server.methods.chatroom.block;
using RongCloud.Server.models.chatroom;
using RongCloud.Server.models.response;

namespace RongCloud.Server.Sdk.Example.chatroom
{
    public class BlockExample

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
         * 自定义api地址
         * */
        private static readonly string api = "http://api.cn.ronghub.com";


        static async Task Main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api地址方式
            //RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Block block = rongCloud.Chatroom.block;

            ChatroomMember[] members =
            {
                new ChatroomMember() {Id = "qawr34h"}, new ChatroomMember() {Id = "qawr35h"}
            };
            /**
             *API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/block.html#add
             *
             * 添加封禁聊天室成员方法
             */


            ChatroomModel chatroom = new ChatroomModel()
            {
                Id = "d7ec7a8b8d8546c98b0973417209a548",
                Members = members,
                Minute = 5
            };

            ResponseResult result = await block.Add(chatroom);
            Console.WriteLine("addBlockUser:  " + result);


            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/block.html#remove
             *
             * 移除封禁聊天室成员方法
             */
            chatroom = new ChatroomModel()
            {
                Id = "d7ec7a8b8d8546c98b0973417209a548",
                Members = members
            };

            //ResponseResult removeResult = block.remove(chatroom);
            //Console.WriteLine("removeResult:  " + removeResult.ToString());

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/block.html#getList
             *
             * 查询被封禁聊天室成员方法
             */
            ListBlockChatroomUserResult getResult = await block.GetList("d7ec7a8b8d8546c98b0973417209a548");
            Console.WriteLine("getListBlockUser:  " + getResult);
            Console.ReadLine();
        }
    }
}