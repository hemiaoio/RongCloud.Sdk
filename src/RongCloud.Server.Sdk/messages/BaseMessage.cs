namespace RongCloud.Server.messages
{
    public abstract class BaseMessage
    {
        public new abstract string GetType();

        public abstract override string ToString();
    }
}