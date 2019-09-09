using io.rong.models.response;
using io.rong.methods.chatroom.whitelist;
using System;

namespace io.rong.example.chatroom.whitelist
{
    public class MessageExample

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

            Whitelist whitelist = rongCloud.Chatroom.whiteList;
            string[] messageType = { "RC:VcMsg", "RC:ImgTextMsg", "RC:ImgMsg" };

            /**
             * API: 文档http://www.rongcloud.cn/docs/server_sdk_api/chatroom/whitelist/message.html#add
             * 添加聊天室全局禁言
             * */

            ResponseResult addResult = whitelist.Message.Add(messageType);
            Console.WriteLine("add whitelist:  " + addResult);
            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/whitelist/message.html#getList
             * 添加聊天室全局禁言
             * */

            ChatroomWhitelistMsgResult getResult = whitelist.Message.GetList();
            Console.WriteLine("get whitelist:  " + getResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/whitelist/message.html#remove
             * 添加聊天室全局禁言
             * */

            ResponseResult removeResult = whitelist.Message.Remove(messageType);
            Console.WriteLine("remove whitelist:  " + addResult);

            Console.ReadLine();
        }
    }
}
