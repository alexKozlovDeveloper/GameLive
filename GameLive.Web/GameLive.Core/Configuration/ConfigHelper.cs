using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.Configuration
{
    public static class ConfigHelper
    {
        public static string LogFolder => GetStringValue(KeyWords.AppSettings.LogFolder);
        public static string WcfServiceUri => GetStringValue(KeyWords.AppSettings.WcfServiceUri);
        public static string ArenaWcfServiceUri => GetStringValue(KeyWords.AppSettings.ArenaWcfServiceUri);

        public static int DefaultMapWidth => GetIntValue(KeyWords.AppSettings.DefaultMapWidth);
        public static int DefaultMapHeight => GetIntValue(KeyWords.AppSettings.DefaultMapHeight);
        public static int GameWindowServiceTickDelay => GetIntValue(KeyWords.AppSettings.GameWindowServiceTickDelay);

        private static string GetStringValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static int GetIntValue(string key)
        {
            var str = ConfigurationManager.AppSettings[key];

            int.TryParse(str, out var result);

            return result;
        }
    }
}
