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
using System.Reflection;
using dotnet_clara.lib;
using CommandLine;
using CommandLine.Text;

namespace dotnet_clara
{
    class Program
    {
        lib.Clara clara;
        
        public Program()
        {           
            
            clara = new lib.Clara();
            
        }
        
        static void Main(string[] args)
        {
            string invokedVerb = null;
            object invokedVerbInstance = null;

            Config config = new Config();
            var options = new Options();

            if (CommandLine.Parser.Default.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  // if parsing succeeds the verb name and correct instance
                  // will be passed to onVerbCommand delegate (string,object)
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              }))
            {
                if (invokedVerb == "set")
                {
                    PropertyInfo[] properties = typeof(Options.SetSubOptions).GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(invokedVerbInstance) != null)
                        {
                            config.SetConfig(property.Name, (string)property.GetValue(invokedVerbInstance));
                            Console.WriteLine("[INFO]: new {0}:{1}", property.Name, config.GetOneConfigInfo(property.Name));
                        }                            
                    }
                }
                if (invokedVerb == "get")
                {
                    PropertyInfo[] properties = typeof(Options.GetSubOptions).GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        if ((bool)property.GetValue(invokedVerbInstance) == true)
                            Console.WriteLine("[INFO]:{0}:{1}", property.Name, config.GetOneConfigInfo(property.Name));
                    }
                }

            }
            else
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }


        }

    }


}

