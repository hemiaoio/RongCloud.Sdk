namespace RongCloud.Server.models.@group
{
    public class UserGroup

    {
        public string Id { get; set; }

        public GroupModel[] Groups { get; set; }

        public UserGroup()
        {
        }

        public UserGroup(string id, GroupModel[] groups)
        {
            Id = id;
            Groups = groups;
        }

        public string getId()
        {
            return Id;
        }

        public UserGroup setId(string id)
        {
            Id = id;
            return this;
        }

        public GroupModel[] getGroups()
        {
            return Groups;
        }

        public UserGroup setGroups(GroupModel[] groups)
        {
            Groups = groups;
            return this;
        }
    }
}
