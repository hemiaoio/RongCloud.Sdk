using System.Collections.Generic;

namespace RongCloud.Server.models.message
{
    public class TemplateMessage
    {
        /**
         * 发送人Id
         * */
        /**
         * 消息类型
         * */
        /**
         * 发送消息内容，内容中定义模版，标识通过 content 中的标识位内容进行替换，
         * 参考融云消息类型表.示例说明；如果 objectName 为自定义消息类型，该参数可自定义格式。（必传）
         * */
        /**
         * key 用户Id ,value 模板赋值内容
         *
         * */

        public string SenderId { get; set; }

        public string ObjectName { get; set; }

        public object Template { get; set; }

        public Dictionary<string, ContentData> Content { get; set; }

        public string[] PushData { get; set; }

        public int VerifyBlacklist { get; set; }

        public int ContentAvailable { get; set; }
    }

    public class ContentData
    {
        /**
         * 消息内容数据，key对应模版的标识 ，value具体内容
         */
        /**
         * push内容
         */

        public string Push { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
