using System.Threading.Tasks;
using RongCloud.Server.models;
using RongCloud.Server.models.push;
using RongCloud.Server.models.response;
using RongCloud.Server.util;

namespace RongCloud.Server.methods.push
{
    /**
     * 推送服务
     *
     * docs https://www.rongcloud.cn/docs/push_service.html#broadcast
     * https://www.rongcloud.cn/docs/push_service.html#push
     */
    public class Push
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

        public Push(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        /**
         * 推送
         *
         * @param push 推送数据
         * @return PushResult
         **/
        public async Task<PushResult> Send(PushModel push)
        {
            // 需要校验的字段
            string message = CommonUtil.CheckFiled(push, PATH, CheckMethod.PUSH);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<PushResult>(message);
            }

            string body = RongJsonUtil.ObjToJsonString(push);

            string result = await RongHttpClient.ExecutePost(appKey, appSecret, body,
                RongCloud.ApiHostType.Type + "/push.json", "application/json");

            return RongJsonUtil.JsonStringToObj<PushResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.BROADCAST,
                result));
        }
    }
}