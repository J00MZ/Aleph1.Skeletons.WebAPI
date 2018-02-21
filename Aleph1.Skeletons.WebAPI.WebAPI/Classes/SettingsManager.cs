using System;
using System.Configuration;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Classes
{
    /// <summary>Handle settings from config</summary>
    internal static class SettingsManager
    {
        private static string[] _modulesPath;
        public static string[] ModulesPath
        {
            get
            {
                if (_modulesPath == default(string[]))
                {
                    try
                    {
                        _modulesPath = ConfigurationManager.AppSettings["ModulesPath"].Split(';').Select(p => p.Trim()).Where(p => !String.IsNullOrWhiteSpace(p)).ToArray();
                    }
                    catch
                    {
                        _modulesPath = new string[0];
                    }
                }
                return _modulesPath;
            }
        }

        private static string _documentationDirPath;
        public static string DocumentationDirPath
        {
            get
            {
                if (_documentationDirPath == default(string))
                {
                    _documentationDirPath = new Uri(new Uri(AppDomain.CurrentDomain.BaseDirectory), ConfigurationManager.AppSettings["DocumentationDirPath"]).LocalPath;
                }
                return _documentationDirPath;
            }
        }

        private static string _appPrefix;
        public static string AppPrefix
        {
            get
            {
                if (_appPrefix == default(string))
                {
                    _appPrefix = ConfigurationManager.AppSettings["AppPrefix"];
                }
                return _appPrefix;
            }
        }

        private static int? _ticketDurationMin;
        private static TimeSpan? _ticketDurationTimeSpan;
        public static TimeSpan? TicketDurationTimeSpan
        {
            get
            {
                if (_ticketDurationMin == default(int?))
                {
                    _ticketDurationMin = int.Parse(ConfigurationManager.AppSettings["TicketDurationMin"]);
                    if (_ticketDurationMin.Value != 0)
                        _ticketDurationTimeSpan = TimeSpan.FromMinutes(_ticketDurationMin.Value);
                }
                return _ticketDurationTimeSpan;
            }
        }

        private static string _authenticationHeaderKey;
        public static string AuthenticationHeaderKey
        {
            get
            {
                if (_authenticationHeaderKey == default(string))
                {
                    _authenticationHeaderKey = ConfigurationManager.AppSettings["AuthenticationHeaderKey"];
                }
                return _authenticationHeaderKey;
            }
        }
    }
}