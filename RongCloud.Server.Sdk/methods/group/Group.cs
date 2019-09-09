using io.rong.util;
using io.rong.models;
using io.rong.models.response;
using io.rong.methods.group.gap;
using io.rong.models.group;
using System.Text;
using System.Web;

namespace io.rong.methods.group
{
    /**
     * 群组服务
     * http://www.rongcloud.cn/docs/server.html#group
     *
     * */
    public class Group

    {
        private static readonly Encoding UTF8 = Encoding.UTF8;
        private static readonly string PATH = "group";
        private RongCloud rongCloud;

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public Gag Gag { get; set; }

        public RongCloud RongCloud
        {
            get => rongCloud;
            set
            {
                rongCloud = value;
                Gag.RongCloud = value;
            }
        }

        public Group(string appKey, string appSecret)
        {
            AppKey = appKey;
            AppSecret = appSecret;
            Gag = new Gag(appKey, appSecret);
            Gag.RongCloud = rongCloud;
        }


        /**
         * 创建群组方法（创建群组，并将用户加入该群组，用户将可以收到该群的消息，同一用户最多可加入 500 个群，每个群最大至 3000 人，App 内的群组数量没有限制.注：其实本方法是加入群组方法 /group/join 的别名。） 
         *
         * url /group/create.json
         * docs http://rongcloud.cn/docs/server.html#group_sync"
         *
         * @param group 创建群组的群组信息
         *
         * @return Result
         **/
        public Result Create(GroupModel group)
        {
            //需要校验的字段
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.CREATE);
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
            sb.Append("&groupName=").Append(HttpUtility.UrlEncode(@group.Name, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                rongCloud.ApiHostType.Type + "/group/create.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.CREATE, result));
        }

        /**
         * 同步用户所属群组方法（当第一次连接融云服务器时，需要向融云服务器提交 userId 对应的用户当前所加入的所有群组，此接口主要为防止应用中用户群信息同融云已知的用户所属群信息不同步。）
         *
         * @param  user:用户群组信息
         *
         * @return ResponseResult
         **/
        public Result Sync(UserGroup user)
        {
            if (user == null)
            {
                return new ResponseResult(1002, "Paramer 'user' is required");
            }

            string message = CommonUtil.CheckFiled(user, PATH, CheckMethod.SYNC);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(user.getId(), UTF8));

            for (int i = 0; i < user.Groups.Length; i++)
            {
                GroupModel child = user.getGroups()[i];
                if (child.Name == null)
                {
                    return new ResponseResult(1002, "Paramer 'group.name' is required");
                }

                if (child.Id == null)
                {
                    return new ResponseResult(1002, "Paramer 'group.id' is required");
                }

                sb.Append("&group[" + child.Id + "]=").Append(HttpUtility.UrlEncode(child.Name, UTF8));
            }

            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                rongCloud.ApiHostType.Type + "/group/sync.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.SYNC, result));
        }

        /**
         * 刷新群组信息方法
         *
         * @param  group:群组信息。id,name（必传）
         *
         * @return ResponseResult
         **/
        public Result Update(GroupModel group)
        {
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.UPDATE);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&groupId=").Append(HttpUtility.UrlEncode(@group.Id, UTF8));
            sb.Append("&groupName=").Append(HttpUtility.UrlEncode(@group.Name, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                rongCloud.ApiHostType.Type + "/group/refresh.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.UPDATE, result));
        }

        /**
         * 邀请用户加入指定群组，加入后可以接收该群消息，同一用户最多可加入 500 个群，每个群最大至 3000 人。
         *
         * @param group 用户加入指定群组参数
         *
         * @return Result
         **/
        public Result Invite(GroupModel group)
        {
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.INVITE);
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
            sb.Append("&groupName=").Append(HttpUtility.UrlEncode(@group.Name, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                rongCloud.ApiHostType.Type + "/group/join.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.INVITE, result));
        }

        /**
         * 将用户加入指定群组，用户将可以收到该群的消息，同一用户最多可加入 500 个群，每个群最大至 3000 人。
         *
         * @param group 用户加入指定群组参数
         *
         * @return Result
         **/
        public Result Join(GroupModel group)
        {
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.JOIN);
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
            sb.Append("&groupName=").Append(HttpUtility.UrlEncode(@group.Name, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                rongCloud.ApiHostType.Type + "/group/join.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<ResponseResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.JOIN, result));
        }

        /**
         * 查询群信息
         * 
         * @param  group:群组.Id。（必传）
         *
         * @return GroupUserQueryResult
         **/
        public GroupUserQueryResult Get(GroupModel group)
        {
            string errMsg = CommonUtil.CheckFiled(group, PATH, CheckMethod.GET);
            if (null != errMsg)
            {
                return RongJsonUtil.JsonStringToObj<GroupUserQueryResult>(errMsg);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("&groupId=").Append(HttpUtility.UrlEncode(@group.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                rongCloud.ApiHostType.Type + "/group/user/query.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<GroupUserQueryResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.GET, result));
        }

        /**
         * 退出群组方法（将用户从群中移除，不再接收该群组的消息.） 
         * 
         * @param  group:群组.id, memberIds（必传）
         *
         * @return ResponseResult
         **/
        public Result Quit(GroupModel group)
        {
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.QUIT);
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
                rongCloud.ApiHostType.Type + "/group/quit.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<GroupUserQueryResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.QUIT, result));
        }

        /**
         * 解散群组方法。（将该群解散，所有用户都无法再接收该群的消息。） 
         * 
         * @param  group: id,member。（必传）
         *
         * @return ResponseResult
         **/
        public Result Dismiss(GroupModel group)
        {
            string message = CommonUtil.CheckFiled(group, PATH, CheckMethod.DISMISS);
            if (null != message)
            {
                return RongJsonUtil.JsonStringToObj<ResponseResult>(message);
            }

            StringBuilder sb = new StringBuilder();
            GroupMember member = group.Members[0];
            sb.Append("&userId=").Append(HttpUtility.UrlEncode(member.Id, UTF8));
            sb.Append("&groupId=").Append(HttpUtility.UrlEncode(@group.Id, UTF8));
            string body = sb.ToString();
            if (body.IndexOf("&") == 0)
            {
                body = body.Substring(1, body.Length - 1);
            }

            string result = RongHttpClient.ExecutePost(AppKey, AppSecret, body,
                rongCloud.ApiHostType.Type + "/group/dismiss.json", "application/x-www-form-urlencoded");

            return RongJsonUtil.JsonStringToObj<GroupUserQueryResult>(
                CommonUtil.GetResponseByCode(PATH, CheckMethod.DISMISS, result));
        }
    }
}