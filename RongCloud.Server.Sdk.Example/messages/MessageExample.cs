﻿using io.rong.models.response;
using System;
using io.rong.messages;
using io.rong.models.message;
using io.rong.methods.messages._private;
using io.rong.methods.messages.chatroom;
using io.rong.methods.messages.discussion;
using io.rong.methods.messages.history;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using io.rong.methods.messages.system;

namespace io.rong.example.messages
{
    /**
     * 消息发送示例
     *
     */
    public class MessageExample
    {
        /**
         * 此处替换成您的appKey
         * */
        private static readonly string appKey = "n19jmcy59f1q9";

        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "CuhqdZMeuLsKj";

        private static readonly TxtMessage txtMessage = new TxtMessage(".NET hello", "helloExtra");

        private static readonly VoiceMessage voiceMessage = new VoiceMessage(".NET hello", "helloExtra", 20L);
        /**
         * 自定义api地址
         * */
        //private static readonly String api = "http://api.cn.ronghub.com";

        static void Main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api 地址方式
            //RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Private Private = rongCloud.Message.msgPrivate;
            // TODO
            MsgSystem system = rongCloud.Message.system;
            methods.messages.group.Group group = rongCloud.Message.group;
            Chatroom chatroom = rongCloud.Message.chatroom;
            Discussion discussion = rongCloud.Message.discussion;
            History history = rongCloud.Message.history;

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/system.html#send
             *
             * 发送系统消息
             *
             */
            string[] targetIds = {"uPj70HUrRSUk-ixtt7iIGc"};
            SystemMessage systemMessage = new SystemMessage()
            {
                SenderId = "Vu-oC0_LQ6kgPqltm_zYtI",
                TargetId = targetIds,
                ObjectName = txtMessage.GetType(),
                Content = txtMessage,
                PushContent = ".NET this is a push system",
                PushData = "{'pushData':'.NET hello'}",
                IsPersisted = 0,
                IsCounted = 0,
                ContentAvailable = 0
            };

            ResponseResult result = rongCloud.Message.system.Send(systemMessage);
            Console.WriteLine("send system message:  " + result);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/system.html#sendTemplate
             *
             * 发送系统模板消息方法
             *
             */
            StreamReader file = null;
            try
            {
                file = File.OpenText(Path.Combine(AppContext.BaseDirectory, "jsonsource/message/TemplateMessage.json"));
                TemplateMessage template = JsonConvert.DeserializeObject<TemplateMessage>(file.ReadToEnd());
                ResponseResult messagePublishTemplateResult = system.SendTemplate(template);

                Console.WriteLine("send systemTemplate message:  " + messagePublishTemplateResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                file.Close();
            }


            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/system.html#sendTemplate
             *
             * 发送系统模板消息方法
             *
             */
            BroadcastMessage message = new BroadcastMessage()
            {
                SenderId = "Vu-oC0_LQ6kgPqltm_zYtI",
                ObjectName = txtMessage.GetType(),
                Content = txtMessage,
                Os = "Android"
            };

            ResponseResult broadcastResult = rongCloud.Message.system.Broadcast(message);
            Console.WriteLine("send broadcast:  " + broadcastResult);


            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/private.html#send
             *
             * 发送单聊消息
             * */
            PrivateMessage privateMessage = new PrivateMessage()
            {
                SenderId = "Vu-oC0_LQ6kgPqltm_zYtI",
                TargetId = targetIds,
                ObjectName = txtMessage.GetType(),
                Content = txtMessage,
                PushContent = ".NET this is a push private",
                PushData = "{\"pushData\":\".NET hello\"}",
                VerifyBlacklist = 0,
                IsPersisted = 0,
                IsCounted = 0,
                ContentAvailable = 0,
                IsIncludeSender = 0
            };

            ResponseResult privateResult = Private.Send(privateMessage);
            Console.WriteLine("send private message:  " + privateResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/private.html#sendTemplate
             *
             * 发送单聊模板消息方法
             */
            try
            {
                file = File.OpenText(Path.Combine(AppContext.BaseDirectory,
                    "jsonsource/message/TemplateMessage.json"));
                TemplateMessage template = JsonConvert.DeserializeObject<TemplateMessage>(file.ReadToEnd());
                ResponseResult messagePublishTemplateResult = Private.SendTemplate(template);

                Console.WriteLine("send privateTemplate message:  " + messagePublishTemplateResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                file.Close();
            }

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/private.html#recall
             *
             * 撤回单聊消息
             * */
            RecallMessage recallMessage = new RecallMessage()
            {
                SenderId = "0fn8TiuHTUgjrZ1QJ8o50M",
                TargetId = "qHPBAoUS6DmEBtJH72RSDi",
                UId = "5H6P-CGC6-44QR-VB3R",
                SentTime = "1519444243981"
            };

            ResponseResult recallPrivateResult = (ResponseResult) Private.Recall(recallMessage);
            Console.WriteLine("recall private:  " + recallPrivateResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/group.html#send
             *
             * 群组消息
             * */
            GroupMessage groupMessage = new GroupMessage()
            {
                SenderId = "Vu-oC0_LQ6kgPqltm_zYtI",
                TargetId = new string[] {"STRe0shISpQlSOBvek1FfU"},
                ObjectName = txtMessage.GetType(),
                Content = txtMessage,
                PushContent = "this is a push",
                PushData = "{\"pushData\":\"hello\"}",
                IsPersisted = 0,
                IsCounted = 0,
                IsIncludeSender = 0,
                ContentAvailable = 0
            };

            ResponseResult groupResult = group.Send(groupMessage);

            Console.WriteLine("send Group message:  " + groupResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/group.html#recall
             *
             * 群组撤回消息
             * */
            recallMessage = new RecallMessage()
            {
                SenderId = "sea9901",
                TargetId = "markoiwm",
                UId = "5GSB-RPM1-KP8H-9JHF",
                SentTime = "1507778882124"
            };

            ResponseResult recallMessageResult = (ResponseResult) group.Recall(recallMessage);

            Console.WriteLine("send recall group message:  " + recallMessageResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/group.html#sendMention
             *
             * 群组@消息
             * */
            //要@的人
            string[] mentionIds = {"uPj70HUrRSUk-ixtt7iIGc", "Vu-oC0_LQ6kgPqltm_zYtI"};

            MentionedInfo mentionedInfo = new MentionedInfo(1, mentionIds, "");
            //@内容
            MentionMessageContent content = new MentionMessageContent(txtMessage, mentionedInfo);

            MentionMessage mentionMessage = new MentionMessage()
            {
                SenderId = "Vu-oC0_LQ6kgPqltm_zYtI",
                TargetId = new string[] {"STRe0shISpQlSOBvek1FfU"},
                ObjectName = txtMessage.GetType(),
                Content = content,
                PushContent = "this is a push",
                PushData = "{\"pushData\":\"hello\"}",
                IsPersisted = 0,
                IsCounted = 0,
                IsIncludeSender = 0,
                ContentAvailable = 0
            };
            ResponseResult mentionResult = rongCloud.Message.group.SendMention(mentionMessage);

            Console.WriteLine("group mention result:  " + mentionResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/discussion.html#send
             *
             * 发送讨论组消息
             * */
            string[] discussionIds = {"lijhGk87", "lijhGk88"};
            DiscussionMessage discussionMessage = new DiscussionMessage()
            {
                SenderId = "JuikH78ko",
                TargetId = discussionIds,
                ObjectName = txtMessage.GetType(),
                Content = txtMessage,
                PushContent = "this is a push",
                PushData = "{\"pushData\":\"hello\"}",
                IsPersisted = 0,
                IsCounted = 0,
                IsIncludeSender = 0,
                ContentAvailable = 0
            };

            ResponseResult discussionResult = discussion.Send(discussionMessage);

            Console.WriteLine("send discussion message:  " + discussionResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/discussion.html#recall
             *
             * 撤回讨论组消息
             * */
            recallMessage = new RecallMessage()
            {
                SenderId = "sea9901",
                TargetId = "IXQhMs3ny",
                UId = "5GSB-RPM1-KP8H-9JHF",
                SentTime = "1519444243981"
            };
            ResponseResult recallDiscussionResult = (ResponseResult) discussion.Recall(recallMessage);

            Console.WriteLine("recall discussion message:  " + recallDiscussionResult);


            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/chatroom.html#send
             *
             * 聊天室消息
             * */

            string[] chatroomIds = {"OIBbeKlkx"};

            CustomTxtMessage ctm = new CustomTxtMessage("hello world");
            ChatroomMessage chatroomMessage = new ChatroomMessage()
            {
                SenderId = "aP9uvganV",
                TargetId = chatroomIds,
                Content = ctm,
                ObjectName = ctm.GetType()
            };

            ResponseResult chatroomResult = chatroom.Send(chatroomMessage);
            Console.WriteLine("send chatroom message:  " + chatroomResult);


            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/chatroom.html#broadcast
             *
             * 聊天室广播消息
             *
             * 此功能需开通专有服务: http://www.rongcloud.cn/deployment#overseas-cloud
             *
             * */
            chatroomMessage = new ChatroomMessage()
            {
                SenderId = "aP9uvganV",
                Content = txtMessage,
                ObjectName = txtMessage.GetType()
            };


            ResponseResult chatroomBroadcastresult = chatroom.Broadcast(chatroomMessage);
            Console.WriteLine("send chatroom broadcast message:  " + chatroomBroadcastresult);


            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/history.html#get
             *
             * 获取历史消息日志文件
             *
             * */

            HistoryMessageResult historyMessageResult = history.Get("2019011711");
            Console.WriteLine("get history  message:  " + historyMessageResult);

            /**
             *
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/message/history.html#get
             *
             * 删除历史消息日志文件
             *
             * */
            ResponseResult removeHistoryMessageResult = history.Remove("2018030210");
            Console.WriteLine("remove history  message:  " + removeHistoryMessageResult);
            Console.ReadLine();
        }
    }
}