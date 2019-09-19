using System;

namespace RongCloud.Server.exception
{

    public abstract class RcloudException : Exception
    {
        /**
         *
         */
        private static readonly long serialVersionUID = -700374663662873165L;
        protected Error error = null;
        public RcloudException()
        {
        }

        public RcloudException(string message, Exception e) : base(message, e)
        {
        }

        public RcloudException(Exception e) : base(e.Message)
        {
        }

        public RcloudException(string message) : base(message)
        {
        }

        public Error GetError()
        {
            return error;
        }

        public int GetErrorCode()
        {
            if (error == null)
            {
                return 200;
            }
            return error.Code;
        }

        public int GetHttpCode()
        {
            if (error == null)
            {
                return 200;
            }
            return error.HttpCode;
        }

        public void SetUri(string uri)
        {
            if (error == null)
            {
                return;
            }
            error.Url = uri;
        }
    }
}

