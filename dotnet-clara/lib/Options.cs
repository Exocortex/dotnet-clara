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
<<<<<<< HEAD
        [Option('r', "read", Required = true,
          HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        // Omitting long name, default --verbose
        [Option(
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

=======
        public Options()
        {
            // Since we create this instance the parser will not overwrite it
            SetVerb = new SetSubOptions();
        }

        [VerbOption("set", HelpText = "Record changes to the repository.")]
        public SetSubOptions SetVerb { get; set; }

        [VerbOption("get", HelpText = "Update remote refs along with associated objects.")]
        public GetSubOptions AddVerb { get; set; }


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
>>>>>>> commandline
    }

}
