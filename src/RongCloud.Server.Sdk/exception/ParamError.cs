namespace RongCloud.Server.exception
{
    public class ParamError : Error

    {
        public ParamError(int errorCode, string apiURL, string errorMessage) : base(errorCode, errorCode, apiURL, errorMessage)
        {
        }

        public ParamError(int errorCode, int httpCode, string apiURL,
                string errorMessage) : base(errorCode, httpCode, apiURL, errorMessage)
        {

        }

        public ParamError(string apiURL) : base(1002, 400, apiURL, "缺少参数，请检查。")
        {
        }

        public ParamError(string apiURL, string message) : base(1002, 400, apiURL, message)
        {
        }
    }
}
