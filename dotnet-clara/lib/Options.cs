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
            usage.AppendLine("-----------------Job-----------------------------");
            usage.AppendLine("job --<option> [value] : get one configuration item.");
            usage.AppendLine("<option> : get");
            usage.AppendLine("[value] : jobId");
            usage.AppendLine("-----------------User----------------------------");
            usage.AppendLine("user --<option> [value] : get one configuration item.");
            usage.AppendLine("<option> : get, update, listScenes, listJobs");
            usage.AppendLine("--get [value] : username");
            usage.AppendLine("--update [value, value] : username profile");
            usage.AppendLine("--listScenes [value, value] : username query");
            usage.AppendLine("--listJobs [value, value] : username query");
            usage.AppendLine("-----------------Scene---------------------------");
            usage.AppendLine("scene --<option> [value] : get one configuration item.");
            usage.AppendLine("<option> : get, update, library, create, delete, clone, export, import, command, render");
            usage.AppendLine("--get [value] : sceneId");
            usage.AppendLine("--update [value, value] : sceneId sceneName");
            usage.AppendLine("--library [value] : query");
            usage.AppendLine("--create");
            usage.AppendLine("--delete [value] : sceneId");
            usage.AppendLine("--clone [value] : sceneId");
            usage.AppendLine("--export [value, value, value] : sceneId extension filePath");
            usage.AppendLine("--import [value, value] : sceneId fileList");
            usage.AppendLine("--command [value, value] : sceneId commandOptions");
            usage.AppendLine("--render [value, value, value, value] : sceneId query options filePath");

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
            [Option("username", HelpText = "Tell the command to automatically stage files.")]
            public string username { get; set; }
            [Option("apiToken", HelpText = "Tell the command to automatically stage files.")]
            public string apiToken { get; set; }
            [Option("host", HelpText = "Tell the command to automatically stage files.")]
            public string host { get; set; }
        }


        public class GetSubOptions
        {
            [Option("username", HelpText = "Tell the command to automatically stage files.")]
            public bool username { get; set; }
            [Option("apiToken", HelpText = "Tell the command to automatically stage files.")]
            public bool apiToken { get; set; }
            [Option("host", HelpText = "Tell the command to automatically stage files.")]
            public bool host { get; set; }
        }

        public class JobSubOptions
        {
            [Option("get", HelpText = "Tell the command to automatically stage files.")]
            public string jobId { get; set; }
        }
        public class UserSubOptions
        {
            [Option("get", HelpText = "Tell the command to automatically stage files.")]
            public string username { get; set; }
            [OptionArray("update", HelpText = "Tell the command to automatically stage files.")]
            public string[] updateQuery { get; set; }
            [OptionArray("listScenes", HelpText = "Tell the command to automatically stage files.")]
            public string[] listScenesQuery { get; set; }
            [OptionArray("listJobs", HelpText = "Tell the command to automatically stage files.")]
            public string[] listJobsQuery { get; set; }
        }
        public class SceneSubOptions
        {
            [Option("library", HelpText = "Tell the command to automatically stage files.")]
            public string libraryQuery { get; set; }
            [Option("get", HelpText = "Tell the command to automatically stage files.")]
            public string getSceneId { get; set; }
            [OptionArray("update", HelpText = "Tell the command to automatically stage files.")]
            public string[] updateQuery { get; set; }
            [Option("create", HelpText = "Tell the command to automatically stage files.")]
            public bool create { get; set; }
            [Option("clone", HelpText = "Tell the command to automatically stage files.")]
            public string cloneSceneId { get; set; }
            [Option("delete", HelpText = "Tell the command to automatically stage files.")]
            public string deleteSceneId { get; set; }
            [OptionArray("export", HelpText = "Tell the command to automatically stage files.")]
            public string[] exportParams { get; set; }
            [OptionArray("import", HelpText = "Tell the command to automatically stage files.")]
            public string[] importParams { get; set; }
            [OptionArray("command", HelpText = "Tell the command to automatically stage files.")]
            public string[] commandParams { get; set; }
            [OptionArray('r', "render", HelpText = "Tell the command to automatically stage files.")]
            public string[] renderParams { get; set; }
        }
    }

}
