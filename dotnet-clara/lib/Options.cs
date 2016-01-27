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
            usage.AppendLine("--get <jobId> --- get job data ");

            usage.AppendLine("-----------------User----------------------------");
            usage.AppendLine("user --[option] <value>");
            usage.AppendLine("[option] : get, update, listScenes, listJobs");
            usage.AppendLine("--get <username> --- get user profile");
            usage.AppendLine("--update <username> <profile> --- update user profiel");
            usage.AppendLine("--listScenes <username> <query> --- list user's scenes");
            usage.AppendLine("--listJobs <username> <query> --- list user's jobs");

            usage.AppendLine("-----------------Scene---------------------------");
            usage.AppendLine("scene --[option] <value> : get one configuration item.");
            usage.AppendLine("[option] : get, update, library, create, delete, clone, export, import, command, render");
            usage.AppendLine("--get <sceneId> --- get a scene data");
            usage.AppendLine("--update <sceneId> <sceneName> --- update a scene");
            usage.AppendLine("--library <query> --- list public scenes");
            usage.AppendLine("--create --- create a new scene");
            usage.AppendLine("--delete <sceneId> --- delete a scene");
            usage.AppendLine("--clone <sceneId> --- clone a scene");
            usage.AppendLine("--export <sceneId> <extension> <filePath> --- export a scene");
            usage.AppendLine("--import <sceneId> <fileList> --- import a file into the scene");
            usage.AppendLine("--command <sceneId> <commandOptions> --- run a command");
            usage.AppendLine("--render <sceneId> <query> <options> <filePath> --- render an image");
            usage.AppendLine("--thumbnail <sceneId> <filePath> --- get the thumbnail of a secne");
            return usage.ToString();
        }

        [VerbOption("set", HelpText = "Set the clara configuration.")]
        public SetSubOptions SetVerb { get; set; }
        [VerbOption("get", HelpText = "Get the clara configuration.")]
        public GetSubOptions GetVerb { get; set; }

        [VerbOption("job", HelpText = "Access the JOB resource of clara.")]
        public JobSubOptions JobVerb { get; set; }
        [VerbOption("user", HelpText = "Access the USER resource of clara.")]
        public UserSubOptions UserVerb { get; set; }
        [VerbOption("scene", HelpText = "Access the SCENE resource of clara.")]
        public SceneSubOptions SceneVerb { get; set; }


        public class SetSubOptions
        {
            [Option("username", HelpText = "The USERNAME in the clara configuration.")]
            public string username { get; set; }
            [Option("apiToken", HelpText = "The APITOKEN in the clara configuration.")]
            public string apiToken { get; set; }
            [Option("host", HelpText = "The HOST in the clara configuration.")]
            public string host { get; set; }
        }


        public class GetSubOptions
        {
            [Option("username", HelpText = "The USERNAME in the clara configuration.")]
            public bool username { get; set; }
            [Option("apiToken", HelpText = "The APITOKEN in the clara configuration.")]
            public bool apiToken { get; set; }
            [Option("host", HelpText = "The HOST in the clara configuration.")]
            public bool host { get; set; }
        }

        public class JobSubOptions
        {
            [Option("get", HelpText = "Get the job data.")]
            public string jobId { get; set; }
        }
        public class UserSubOptions
        {
            [Option("get", HelpText = "Get the user data.")]
            public string username { get; set; }
            [OptionArray("update", HelpText = "Update the user profile.")]
            public string[] updateQuery { get; set; }
            [OptionArray("listScenes", HelpText = "List the user's scenes.")]
            public string[] listScenesQuery { get; set; }
            [OptionArray("listJobs", HelpText = "List the user's jobs.")]
            public string[] listJobsQuery { get; set; }
        }
        public class SceneSubOptions
        {
            [Option("library", HelpText = "List the scenes in the library.")]
            public string libraryQuery { get; set; }
            [Option("get", HelpText = "Get the scene data.")]
            public string getSceneId { get; set; }
            [OptionArray("update", HelpText = "Update the scene data.")]
            public string[] updateQuery { get; set; }
            [Option("create", HelpText = "Create a new scene.")]
            public bool create { get; set; }
            [Option("clone", HelpText = "Clone a existing scene.")]
            public string cloneSceneId { get; set; }
            [Option("delete", HelpText = "Delete a scene.")]
            public string deleteSceneId { get; set; }
            [OptionArray("export", HelpText = "Export the model from a scene with specified format.")]
            public string[] exportParams { get; set; }
            [OptionArray("import", HelpText = "Import files into a scene.")]
            public string[] importParams { get; set; }
            [OptionArray("command", HelpText = "Excute a clara command.")]
            public string[] commandParams { get; set; }
            [OptionArray('r', "render", HelpText = "Render a image of a scene.")]
            public string[] renderParams { get; set; }
            [OptionArray('t', "thumbnail", HelpText = "Get the thumbnail of a scene.")]
            public string[] thumbnailParams { get; set; }
        }
    }

}
