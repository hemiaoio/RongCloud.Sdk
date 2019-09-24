using System;
using System.Threading.Tasks;
using RongCloud.Server.models.push;
using RongCloud.Server.models.response;

namespace RongCloud.Server.Sdk.Example.push
{
    public class PushExample
    {
        /**
     * 此处替换成您的appKey
     * */
        private static string appKey = "appKey";

        /**
         * 此处替换成您的appSecret
         * */
        private static string appSecret = "appSecret";

        /**
         * 自定义api地址
         * */
        private static string api = "http://api-cn.ronghub.com";


        public static async Task Main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);

            /**
             *
             * API 文档:
             * https://www.rongcloud.cn/docs/push_service.html#broadcast
             *
             * 广播消息
             *
             **/
            BroadcastModel broadcast = new BroadcastModel();
            broadcast.SetFromuserid("fromuserid");
            broadcast.SetPlatform(new string[] {"ios", "android"});
            Audience audience = new Audience();
            audience.SetUserid(new string[] {"userid1", "userid2"});
            broadcast.SetAudience(audience);
            Message message = new Message();
            message.SetContent("this is message");
            message.SetObjectName("RC:TxtMsg");
            broadcast.SetMessage(message);
            Notification notification = new Notification();
            notification.SetAlert("this is broadcast");
            broadcast.SetNotification(notification);
            PushResult result = await rongCloud.Broadcast.Send(broadcast);

            Console.WriteLine("broadcast: " + result);


            /**
             *
             * API 文档:
             * https://www.rongcloud.cn/docs/push_service.html#push
             *
             * 推送消息
             *
             **/
            PushModel pushmodel = new PushModel();
            pushmodel.SetPlatform(new string[] {"ios", "android"});
            audience = new Audience();
            audience.SetUserid(new string[] {"userid1", "userid2"});
            pushmodel.SetAudience(audience);
            notification = new Notification();
            notification.SetAlert("this is push");
            pushmodel.SetNotification(notification);
            result = await rongCloud.Push.Send(pushmodel);

            Console.WriteLine("push: " + result);

            Console.ReadLine();
        }
    }
}