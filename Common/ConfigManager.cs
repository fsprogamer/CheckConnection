using System.Configuration;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;

using log4net;
using System;

namespace Common
{
    public class ConfigManager:ClassWithLogger<ConfigManager>
    {
        public string[] GetStringArray(string ElementName)
        {
            string[] strings = null;

            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = Assembly.GetEntryAssembly().Location + ".config";
                Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                ConfigurationSectionGroup appSettingsGroup = conf.GetSectionGroup("userSettings");
                ClientSettingsSection clientSettings = (ClientSettingsSection)appSettingsGroup.Sections["CheckConnection.Properties.Settings"];
                ConfigurationElement element = clientSettings.Settings.Get(ElementName);
                string xml = ((SettingElement)element).Value.ValueXml.InnerXml;
                XmlSerializer xs = new XmlSerializer(typeof(string[]));
                strings = (string[])xs.Deserialize(new XmlTextReader(xml, XmlNodeType.Element, null));
            }
            catch(Exception ex)
            {
                log.Error("Ошибка чтения файла конфигурации", ex);
            }
            return strings;
        }
    }
}
