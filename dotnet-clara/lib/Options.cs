using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace dotnet_clara.lib
{
    public class Options
    {
        public Options()
        {
            // Since we create this instance the parser will not overwrite it
            SetVerb = new SetSubOptions();
        }
        [HelpOption]
        public string GetUsage()
        {
            // this without using CommandLine.Text
            //  or using HelpText.AutoBuild
            var usage = new StringBuilder();
            usage.AppendLine("dotnet clara v1.0");
            usage.AppendLine("Read README file for more usage instructions...");
            usage.AppendLine("help : for help" );
            usage.AppendLine("set --<option> [value] : set one configuration item.");
            usage.AppendLine("get --<option> [value] : get one configuration item.");
            return usage.ToString();
        }

        [VerbOption("set", HelpText = "Record changes to the repository.")]
        public SetSubOptions SetVerb { get; set; }

        [VerbOption("get", HelpText = "Update remote refs along with associated objects.")]
        public GetSubOptions AddVerb { get; set; }

        [VerbOption("job", HelpText = "Update remote refs along with associated objects.")]
        public JobsSubOptions JobsVerb { get; set; }
        [VerbOption("user", HelpText = "Update remote refs along with associated objects.")]
        public JobsSubOptions JobsVerb { get; set; }
        [VerbOption("scene", HelpText = "Update remote refs along with associated objects.")]
        public JobsSubOptions JobsVerb { get; set; }


        public class SetSubOptions
        {
            [Option('u',"username", HelpText = "Tell the command to automatically stage files.")]
            public string username { get; set; }
            // Remainder omitted
            [Option('a',"apiToken", HelpText = "Tell the command to automatically stage files.")]
            public string apiToken { get; set; }
            [Option('h',"host", HelpText = "Tell the command to automatically stage files.")]
            public string host { get; set; }
        }

        public class GetSubOptions
        {
            [Option('u', "username", HelpText = "Tell the command to automatically stage files.")]
            public bool username { get; set; }
            // Remainder omitted
            [Option('a', "apiToken", HelpText = "Tell the command to automatically stage files.")]
            public bool apiToken { get; set; }
            [Option('h', "host", HelpText = "Tell the command to automatically stage files.")]
            public bool host { get; set; }
        }

        public class JobsSubOptions
        {
            [Option('g', "get", HelpText = "Tell the command to automatically stage files.")]
            public string jobId { get; set; }
        }
    }

}
