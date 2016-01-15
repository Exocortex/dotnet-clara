using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace dotnet_clara.lib
{
    class Cli
    {
        [Verb("set", HelpText = "Add file contents to the index.")]
        public class Set_Verb
        {
            [Option('u', "username", SetName = "mode-u", HelpText = "username in clara.io.")]
            public bool Username { get; set; }

            [Option('a', "apiToken", SetName = "mode-a", HelpText = "apiToken in clara.io.")]
            public bool ApiToken { get; set; }

            [Option('h', "host", SetName = "mode-h", HelpText = "host in clara.io.")]
            public bool Host { get; set; }

            [Option('b', "basePath", SetName = "mode-b", HelpText = "base path in clara.io.")]
            public bool BasePath { get; set; }
        }

        [Verb("get", HelpText = "Add file contents to the index.")]
        public class Get_Verb
        {
            [Option('u', "username", SetName = "mode-u", HelpText = "username in clara.io.")]
            public string Username { get; set; }

            [Option('a', "apiToken", SetName = "mode-a", HelpText = "apiToken in clara.io.")]
            public string ApiToken { get; set; }

            [Option('h', "host", SetName = "mode-h", HelpText = "host in clara.io.")]
            public string Host { get; set; }

            [Option('b', "basePath", SetName = "mode-b", HelpText = "base path in clara.io.")]
            public string BasePath { get; set; }
        }
    }
}
