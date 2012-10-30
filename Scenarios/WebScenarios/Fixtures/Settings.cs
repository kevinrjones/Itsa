using System;
using System.Xml.Linq;
using WebSiteSpecifications.Values;

namespace WebSiteSpecifications.Fixtures
{
    class Settings
    {
        public string Url { get; set; }
        public string FirefoxBinaryPath { get; set; }

        public DefaultValues Defaults { get; set; }

        private static Settings _settings;
        public static Settings CurrentSettings
        {
            get
            {
                if (_settings == null)
                {
                    LoadSettings("TestRun.config");
                }
                return _settings;
            }
        }

        private Settings(string url)
        {
            Url = url;
        }

        private static void LoadSettings(String file)
        {
            XElement settingsFile = XElement.Load(file);
            var xElement = settingsFile.Element("URL");
            if (xElement != null)
            {
                string url = xElement.Value;
                _settings = new Settings(url);
            }

            _settings.Defaults = new DefaultValues();
            //_settings.Defaults.Album = new Album()
            //                               {
            //                                   Name = settingsFile.Element("album").Value
            //                               };
        }

    }
}