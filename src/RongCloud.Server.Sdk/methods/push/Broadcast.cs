using System.Threading.Tasks;
using RongCloud.Server.models;
using RongCloud.Server.models.push;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.push
{
    public class Broadcast
    {
        private static string UTF8 = "UTF-8";
        private static string PATH = "push";
        private string appKey;
        private string appSecret;
        public RongCloud RongCloud;

        public RongCloud GetRongCloud()
        {
            return RongCloud;
        }

        public void SetRongCloud(RongCloud rongCloud)
        {
            RongCloud = rongCloud;
        }

        public Broadcast(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        /**
         * 广播
         *
         * @param broadcast 广播数据
         * @return PushResult
         **/
        public async Task<PushResult> Send(BroadcastModel broadcast)
        {
            // 需要校验的字段
            string message = CommonUtil.CheckFiled(broadcast, PATH, CheckMethod.BROADCAST);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<PushResult>(message);
            }

            string body = RongJsonUtil.ObjToJsonString(broadcast);

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/push.json", "application/json");

            return RongJsonUtil.JsonStringToObj<PushResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.BROADCAST,
                result));
        }
    }
}