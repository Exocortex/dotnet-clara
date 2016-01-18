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
        static void Main(string[] args)
        {
            string invokedVerb = null;
            object invokedVerbInstance = null;
            
            Config config = new Config();
            lib.Clara clara = new lib.Clara(config);
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
                if (invokedVerb == "job")
                {
                    PropertyInfo[] properties = typeof(Options.JobsSubOptions).GetProperties();
                    var usage = new StringBuilder();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(invokedVerbInstance) != null)
                        {                          
                            usage.Append(clara.jobs.Get((string)property.GetValue(invokedVerbInstance)).Result);
                        }
                        Console.WriteLine("[INFO]:{0}", usage);
                    }
                }


            }
            else
            {
                Console.WriteLine(options.GetUsage());
            }


        }

    }


}
