using System;

namespace io.rong.exception
{

    public class ParamException : RcloudException

    {

        private static readonly long serialVersionUID = -5021603276540528761L;

        public ParamException()
        {
            error = new ParamError("/");
        }

        public ParamException(string message, Exception e) : base(new ParamError("/", message).ToString(), e)
        {
            error = new ParamError("/", message);
        }

        public ParamException(Exception e) : base(e.Message)
        {
            error = new ParamError("/");
        }

        public ParamException(string message) : base(message)
        {
            error = new ParamError("/", message);
        }

        public ParamException(int errorCode, string apiUrl, string message) : base(new ParamError(errorCode, apiUrl, message).ToString())
        {
            error = new ParamError(errorCode, apiUrl, message);

        }
    }
}

