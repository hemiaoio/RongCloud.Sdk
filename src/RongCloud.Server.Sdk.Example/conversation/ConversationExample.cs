using System;
using System.Threading.Tasks;
using RongCloud.Server.methods.conversation;
using RongCloud.Server.models.conversation;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.Sdk.Example.conversation
{
    /**
     *
     * 会话示例
     * @author RongCloud
     *
     */
    public class ConversationExample

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

            Conversation Conversation = rongCloud.Conversation;

            ConversationModel conversation = new ConversationModel()
            {
                Type = CodeUtil.ConversationType.PRIVATE.Name,
                UserId = "uPj70HUrRSUk-ixtt7iIGc",
                TargetId = "Vu-oC0_LQ6kgPqltm_zYtI"
            };

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/conversation/conversation.html#mute
             * 设置消息免打扰
             *
             */
            ResponseResult muteConversationResult = await Conversation.Mute(conversation);

            Console.WriteLine("muteConversationResult:  " + muteConversationResult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/conversation/conversation.html#unmute
             * 解除消息免打扰
             *
             * */
            ResponseResult unMuteConversationResult = await Conversation.UnMute(conversation);

            Console.WriteLine("unMuteConversationResult:  " + unMuteConversationResult);
            Console.ReadLine();
        }
    }
}