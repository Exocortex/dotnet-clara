using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib
{
    class Config
    {
        public ConfigInfo defaultConfig;

        public Config()
        {
            defaultConfig = new ConfigInfo
            {
                apiToken = null,
                basePath = "/api",
                host = "https://clara.io",
                username = null
            };
        }

        public class ConfigInfo
        {
            public string apiToken { get; set; }
            public string basePath { get; set; }
            public string host { get; set; }
            public string username { get; set; }
        }

        public static string home = Environment.GetEnvironmentVariable("USERPROFILE");
        public string configFilePath =  home + "/.Netclara.json";
        
        // Write the default config to disk when starting.
        public void initializeConfig()
        {
            string configFile = System.IO.File.ReadAllText(configFilePath);//json string
            if (configFile == null)
                WriteConfig(defaultConfig);
        }

        // Write the config info to disk.
        public void WriteConfig(ConfigInfo configObj)
        {
            if (home == null)
            {
                Console.WriteLine("Invalid Home Directory!");
                return;
            }
            if(configFilePath == null)
            {
                Console.WriteLine("Invalid Configuration File Path!");
                return;
            }
            string output = JsonConvert.SerializeObject(configObj);
            if (output == null)
            {
                Console.WriteLine("Invalid Configuration!");
                return;
            }
            System.IO.StreamWriter file = new System.IO.StreamWriter(configFilePath);
            file.WriteLine(output);
            file.Close();
        }

        // Read the config from disk.
        public ConfigInfo ReadConfig(string dir)
        {
            if (dir == null) dir = home;

            string configFilePath = dir + "/.Netclara.json";
            string configFile = System.IO.File.ReadAllText(configFilePath);//json string
            if (configFile == null) return null;
            try
            {
                ConfigInfo configObj = JsonConvert.DeserializeObject<ConfigInfo>(configFile);
                return configObj;
            }
            catch (Exception e)
            {
                Console.Write("Invalid Configuration File:" + configFilePath);
                return null;
            }
        }

        // Get one config item value by key.
        public string GetOneConfigInfo(string key)
        {
            ConfigInfo configObj = ReadConfig(home);
            PropertyInfo[] properties = typeof(ConfigInfo).GetProperties();
            string value = null;
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == key)
                    value = (string)property.GetValue(configObj);
            }
            return value;
        }

        // Set the value of one config item.
        public void SetConfig(string key, string value)
        {
            ConfigInfo curConfig = defaultConfig;
            PropertyInfo[] properties = typeof(ConfigInfo).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == key)
                    property.SetValue(curConfig, value);
            }
            WriteConfig(curConfig);
        }
    }
}
