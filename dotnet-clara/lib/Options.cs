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
            usage.AppendLine("-----------------Job-----------------------------");
            usage.AppendLine("help --- for help" );

            usage.AppendLine("-----------------Configuration-----------------------------");
            usage.AppendLine("set --[option] <value> --- set configuration data");
            usage.AppendLine("get --[option] <value> --- get configuration data");
            usage.AppendLine("[option] : username, apiToken, host ");

            usage.AppendLine("-----------------Job-----------------------------");
            usage.AppendLine("job --[option] <value>");
            usage.AppendLine("[option] : get ");
            usage.AppendLine("--get jobId --- get job data ");

            usage.AppendLine("-----------------User----------------------------");
            usage.AppendLine("user --[option] <value>");
            usage.AppendLine("[option] : get, update, listScenes, listJobs");
            usage.AppendLine("--get username --- get user profile");
            usage.AppendLine("--update username profile --- update user profiel");
            usage.AppendLine("--listScenes username query --- list user's scenes");
            usage.AppendLine("--listJobs username query --- list user's jobs");

            usage.AppendLine("-----------------Scene---------------------------");
            usage.AppendLine("scene --[option] <value> : get one configuration item.");
            usage.AppendLine("[option] : get, update, library, create, delete, clone, export, import, command, render");
            usage.AppendLine("--get sceneId --- get a scene data");
            usage.AppendLine("--update sceneId sceneName --- update a scene");
            usage.AppendLine("--library query --- list public scenes");
            usage.AppendLine("--create --- create a new scene");
            usage.AppendLine("--delete sceneId --- delete a scene");
            usage.AppendLine("--clone sceneId --- clone a scene");
            usage.AppendLine("--export sceneId extension filePath --- export a scene");
            usage.AppendLine("--import sceneId fileList --- import a file into the scene");
            usage.AppendLine("--command sceneId commandOptions --- run a command");
            usage.AppendLine("--render sceneId query options filePath --- render an image");

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
