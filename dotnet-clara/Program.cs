using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.IO;
using dotnet_clara.lib;
using CommandLine;
using CommandLine.Text;

namespace dotnet_clara
{
    class Program
    {
        lib.Clara clara;
        Config config;
        Cli options;
        public Program()
        {           
            config = new Config();
            clara = new lib.Clara(config);
            options = new Cli();
        }
        
        static void Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args);
            var exitCode = result
              .MapResult(
                options = > {
                if (options.Verbose) Console.WriteLine("Filenames: {0}", string.Join(",", options.InputFiles.ToArray()));
                return 0;
            },
      errors => {
          LogHelper.Log(errors);
          return 1;
      });
            return exitCode;
        }

    }


}

