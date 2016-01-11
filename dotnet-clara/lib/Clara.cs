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

        private Config config;

        public Clara()
        {
            this.config = new Config();
            config.initializeConfig();
            this.scene = new Scenes();
        }
        public Clara(Config config)
        {
            this.config = config;
            this.scene = new Scenes();
        }
    }
}
