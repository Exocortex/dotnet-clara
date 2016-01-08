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
        [Option('u', "username", Required = true, HelpText = "Username in Clara.io")]
        public string Username { get; set; }

        [Option('s', "server", DefaultValue = -1, HelpText = "The domain name of Clara server.")]
        public int Server { get; set; }

        [Option('a', "apiToken", Required = true, HelpText = "The user's api token.")]
        public bool ApiToken { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            // this without using CommandLine.Text
            //  or using HelpText.AutoBuild
            var usage = new StringBuilder();
            usage.AppendLine("Quickstart Application 1.0");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }

        class SetGetSubOptions
        {
            [Option('u', "username", Required = true, HelpText = "Username in Clara.io")]
            public string Username { get; set; }

            [Option('s', "server", DefaultValue = -1, HelpText = "The domain name of Clara server.")]
            public int Server { get; set; }

            [Option('a', "apiToken", Required = true, HelpText = "The user's api token.")]
            public bool ApiToken { get; set; }

            public bool All { get; set; }
            public bool Patch { get; set; }
        }

        class Options
        {
            public Options()
            {
                // Since we create this instance the parser will not overwrite it
                SetVerb = new SetGetSubOptions { Patch = true };
                GetVerb = new SetGetSubOptions { Patch = true };
            }

            [VerbOption("set", HelpText = "Record changes to the repository.")]
            public SetGetSubOptions SetVerb { get; set; }

            [VerbOption("get", HelpText = "Record changes to the repository.")]
            public SetGetSubOptions GetVerb { get; set; }

        }
    }

}
