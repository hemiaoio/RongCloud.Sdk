using System;
using System.Threading.Tasks;
using RongCloud.Server.methods.chatroom.distribute;
using RongCloud.Server.models.chatroom;
using RongCloud.Server.models.response;

namespace RongCloud.Server.Sdk.Example.chatroom
{
    public class DistributeExample

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

            Distribute distribute = rongCloud.Chatroom.distribute;

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/distribute.html#stop
             *
             * 聊天室消息停止分发
             *
             */
            ChatroomModel chatroomModel = new ChatroomModel()
            {
                Id = "d7ec7a8b8d8546c98b0973417209a548"
            };
            ResponseResult result = await distribute.Stop(chatroomModel);

            Console.WriteLine("stopDistributionMessage:  " + result);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/distribute.html#resume
             *
             * 聊天室消息恢复分发方法（每秒钟限 100 次）
             */
            ResponseResult resumeResult = await distribute.Resume(chatroomModel);
            Console.WriteLine("resumeDistributionMessage:  " + resumeResult);
            Console.ReadLine();
        }
    }
}