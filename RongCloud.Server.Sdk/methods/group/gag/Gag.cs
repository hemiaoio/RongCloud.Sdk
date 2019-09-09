using io.rong.util;
using io.rong.models.group;
using io.rong.models;
using io.rong.models.response;
using System;
using System.Text;
using System.Web;

namespace io.rong.methods.group.gap
{
    /**
     * 群组成员禁言服务
     * docs : http://www.rongcloud.cn/docs/server.html#group_user_gag
     *
     * */
    public class Gag

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "group/gag";


        public Gag(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;

        }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        internal RongCloud RongCloud { get; set; }

        /**
        * 添加禁言群成员方法（在 App 中如果不想让某一用户在群中发言时，可将此用户在群组中禁言，被禁言用户可以接收查看群组中用户聊天信息，但不能发送消息。）
        *
        * @param group:群组信息。id , munite , memberIds（必传）
        *
        * @return Result
        **/
        public Result Add(GroupModel group)
        {
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.ADD);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            /* message = CommonUtil.checkParam("munite",munite,PATH,CheckMethod.ADD);
             if(null != message){
                 return (Result)RongJsonUtil.JsonStringToObj(message,Result.class);
             }*/

            StringBuilder sb = new StringBuilder();
            GroupMember[] members = group.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }
            sb.Append("&groupId=").Append(HttpUtility.UrlEncode(@group.Id, UTF8));
            sb.Append("&minute=").Append(HttpUtility.UrlEncode(group.Minute.ToString(), UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                            RongCloud.ApiHostType.Type + "/group/user/gag/add.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.ADD, result));

        }

        /**
         * 查询被禁言群成员方法
         *
         * @param  groupId:群组Id。（必传）
         *
         * @return ListGagGroupUserResult
         **/
        public ListGagGroupUserResult GetList(string groupId)
        {
            string message = CommonUtil.CheckParam("id", groupId, PATH, CheckMethod.GETLIST);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ListGagGroupUserResult>(message);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("&groupId=").Append(HttpUtility.UrlEncode(groupId, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                RongCloud.ApiHostType.Type + "/group/user/gag/list.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ListGagGroupUserResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.GETLIST, result));
        }

        /**
         * 移除禁言群成员方法
         *
         * @param  group:群组（必传）
         *
         * @return ResponseResult
         **/
        public Result Remove(GroupModel group)
        {
            //参数校验
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.REMOVE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }
            StringBuilder sb = new StringBuilder();

            GroupMember[] members = group.Members;
            foreach (var member in members)
            {
                sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            }

            sb.Append("&groupId=").Append(HttpUtility.UrlEncode(@group.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }
            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                                    RongCloud.ApiHostType.Type + "/group/user/gag/rollback.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ListGagGroupUserResult>(CommonUtil.GetResponseByCode(PATH, CheckMethod.REMOVE, result));
        }
    }
}