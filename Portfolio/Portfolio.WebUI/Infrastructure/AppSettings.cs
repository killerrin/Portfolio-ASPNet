using Portfolio.WebUI.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Infrastructure
{
    public static class AppSettings
    {
        public static class Values
        {
            public static bool AllowAccountCreation { get { return AppSettings.Get<bool>("AllowAccountCreation"); } }
        }

        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) throw new AppSettingNotFoundException(key);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }
    }
}
