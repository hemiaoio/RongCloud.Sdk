using io.rong.models.response;
using io.rong.methods.chatroom.keepalive;
using io.rong.models.chatroom;
using System;

namespace io.rong.example.chatroom
{
    public class KeepaliveExample

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

        static void Main(string[] args)
        {

            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api地址方式
            //RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Keepalive keepalive = rongCloud.Chatroom.keepalive;

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/keepalive.html#add
             *
             * 添加保活聊天室
             *
             **/
            ChatroomModel chatroom = new ChatroomModel()
            {
                Id = "d7ec7a8b8d8546c98b0973417209a548"
            };
            ResponseResult addResult = keepalive.Add(chatroom);
            Console.WriteLine("add keepalive result" + addResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/keepalive.html#remove
             *
             * 删除保活聊天室
             *
             **/
            ResponseResult removeResult = keepalive.Remove(chatroom);
            Console.WriteLine("keepalive remove" + removeResult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/keepalive.html#getList
             *
             * 获取保活聊天室
             *
             **/
            ChatroomKeepaliveResult result = keepalive.GetList();

            Console.WriteLine("keepalive getList" + result);
            Console.ReadLine();
        }
    }
}
