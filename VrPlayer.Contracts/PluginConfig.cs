using System.Collections.Generic;
using System.Configuration;

namespace VrPlayer.Contracts
{
    public class PluginConfig
    {
        public string Type { get; set; }
        public List<DataItem> Data; 

        public PluginConfig() 
        {
            Data = new List<DataItem>();
        }

        public static PluginConfig FromSettings(KeyValueConfigurationCollection settings)
        {
            var config = new PluginConfig();
            foreach (var key in settings.AllKeys)
            {
                config.Data.Add(new DataItem(key, settings[key].Value));
            }
            return config;
        }
    }

    public class DataItem
    {
        public string Key;
        public string Value;

        public DataItem()
        {
        }

        public DataItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}