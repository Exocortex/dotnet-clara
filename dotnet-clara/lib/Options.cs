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
            SetVerb = new SetSubOptions();
            GetVerb = new GetSubOptions();
            JobVerb = new JobSubOptions();
            UserVerb = new UserSubOptions();
            SceneVerb = new SceneSubOptions();
        }
        [HelpOption]
        public string GetUsage()
        {
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
        public GetSubOptions GetVerb { get; set; }

        [VerbOption("job", HelpText = "Update remote refs along with associated objects.")]
        public JobSubOptions JobVerb { get; set; }
        [VerbOption("user", HelpText = "Update remote refs along with associated objects.")]
        public UserSubOptions UserVerb { get; set; }
        [VerbOption("scene", HelpText = "Update remote refs along with associated objects.")]
        public SceneSubOptions SceneVerb { get; set; }


        public class SetSubOptions
        {
            [Option('u', "username", HelpText = "Tell the command to automatically stage files.")]
            public string username { get; set; }
            [Option('a', "apiToken", HelpText = "Tell the command to automatically stage files.")]
            public string apiToken { get; set; }
            [Option('h', "host", HelpText = "Tell the command to automatically stage files.")]
            public string host { get; set; }
        }


        public class GetSubOptions
        {
            [Option('u', "username", HelpText = "Tell the command to automatically stage files.")]
            public bool username { get; set; }
            [Option('a', "apiToken", HelpText = "Tell the command to automatically stage files.")]
            public bool apiToken { get; set; }
            [Option('h', "host", HelpText = "Tell the command to automatically stage files.")]
            public bool host { get; set; }
        }

        public class JobSubOptions
        {
            [Option('g', "get", HelpText = "Tell the command to automatically stage files.")]
            public string jobId { get; set; }
        }
        public class UserSubOptions
        {
            [Option('g', "get", HelpText = "Tell the command to automatically stage files.")]
            public string username { get; set; }
            [OptionArray('u', "update", HelpText = "Tell the command to automatically stage files.")]
            public string[] updateQuery { get; set; }
            [OptionArray("listScenes", HelpText = "Tell the command to automatically stage files.")]
            public string[] listScenesQuery { get; set; }
            [OptionArray("listJobs", HelpText = "Tell the command to automatically stage files.")]
            public string[] listJobsQuery { get; set; }
        }
        public class SceneSubOptions
        {
            [Option('g', "get", HelpText = "Tell the command to automatically stage files.")]
            public string jobId { get; set; }
        }
    }

}
