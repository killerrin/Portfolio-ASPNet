using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Infrastructure.Exceptions
{
    [Serializable]
    public class AppSettingNotFoundException : Exception
    {
        public string Key = "";

        public AppSettingNotFoundException()
        {
            Key = "";
        }

        public AppSettingNotFoundException(string message) : base(message)
        {
            Key = message;
        }

        public AppSettingNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            Key = message;
        }

        protected AppSettingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
