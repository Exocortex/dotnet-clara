using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnet_clara.lib.resoureces;

namespace dotnet_clara.lib
{
    class Clara
    {
        public Scenes scene;
        public Jobs jobs;
        public User user;
        private Config config;

        public Clara()
        {
            this.config = new Config();
            config.initializeConfig();
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
