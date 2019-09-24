namespace RongCloud.Server.models.@group
{
    /**
     * 群组数据模型
     *
     * */
    public class GroupModel

    {
        /**
         * 群组id
         **/
        /**
         * 群组成员
         **/
        /**
         * 群组名
         **/

        /**
         * 禁言时间
         * */

        public GroupModel()
        {
        }
        /**
         * 构造方法
         *
         * @param id 群组id
         * @param members 群组成员
         * @param name 群名
         */
        public GroupModel(string id, GroupMember[] members, string name, int minute)
        {
            Id = id;
            Members = members;
            Name = name;
            Minute = minute;
        }

        public int Minute { get; set; }

        public string Id { get; set; }

        public GroupMember[] Members { get; set; }

        public string Name { get; set; }
    }
}
