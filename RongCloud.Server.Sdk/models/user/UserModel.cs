using Newtonsoft.Json;

namespace RongCloud.Server.models.user
{
    public class UserModel

    {
        /**
    * 用户 Id，最大长度 64 字节.是用户在 App 中的唯一标识码，
    * 必须保证在同一个 App 内不重复，重复的用户 Id 将被当作是同一用户。（必传）
    */
        public string id;
        /**
         * 用户名称，最大长度 128 字节。用来在 Push 推送时，显示用户的名称，
         * 刷新用户名称后 5 分钟内生效。（可选，提供即刷新，不提供忽略）
         */
        public string name;
        /**
         * 用户头像 URI，最大长度 1024 字节。
         * 用来在 Push 推送时显示。（可选，提供即刷新，不提供忽略)
         */
        public string portrait;

        public int minute;
        /**
         * 黑名单列表。
         */
        public UserModel[] blacklist;


        public UserModel()
        {
        }

        public UserModel(string id, string name, string portrait)
        {
            this.id = id;
            this.name = name;
            this.portrait = portrait;
        }

        [JsonIgnore]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string GetId()
        {
            return id;
        }

        public UserModel SetId(string id)
        {
            this.id = id;
            return this;
        }

        [JsonIgnore]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string GetName()
        {
            return name;
        }

        public UserModel SetName(string name)
        {
            this.name = name;
            return this;
        }

        [JsonIgnore]
        public string Portrait
        {
            get { return portrait; }
            set { portrait = value; }
        }

        public string GetPortrait()
        {
            return portrait;
        }

        public UserModel SetPortrait(string portrait)
        {
            this.portrait = portrait;
            return this;
        }

        [JsonIgnore]
        public int Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        public int GetMinute()
        {
            return minute;
        }

        public UserModel SetMinute(int minute)
        {
            this.minute = minute;
            return this;
        }

        [JsonIgnore]
        public UserModel[] Blacklist
        {
            get { return blacklist; }
            set { blacklist = value; }
        }

        public UserModel[] GetBlacklist()
        {
            return blacklist;
        }

        public UserModel SetBlacklist(UserModel[] blacklist)
        {
            this.blacklist = blacklist;
            return this;
        }

        override
        public string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
