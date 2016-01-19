using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnet_clara.lib.resoureces;

namespace dotnet_clara.lib
{
    public class Clara
    {
        public Scenes scene;
        public Jobs jobs;
        public User user;
        private Config config;

        public Clara(string username, string apiToken, string host)
        {
            this.config = new Config();
            config.initializeConfig();
            config.SetConfig("username", username);
            config.SetConfig("apiToken", apiToken);
            config.SetConfig("host", host);
            this.scene = new Scenes();
            this.jobs = new Jobs();
            this.user = new User();
        }
        public Clara(Config config)
        {
            this.config = config;
            this.scene = new Scenes();
            this.jobs = new Jobs();
            this.user = new User();
        }
    }
}
