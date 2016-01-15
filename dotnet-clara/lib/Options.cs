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
        [Option('r', "read", Required = true,
          HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        // Omitting long name, default --verbose
        [Option(
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option(Default = "中文",
          HelpText = "Content language.")]
        public string Language { get; set; }

        [Value(0, MetaName = "offset",
          HelpText = "File offset.")]
        public long? Offset { get; set; }
    }

}
