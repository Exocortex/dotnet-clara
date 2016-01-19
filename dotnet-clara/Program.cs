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
            //var resource = args[0];
            //string[] newArgs = new string[args.Length-1];
            if (CommandLine.Parser.Default.ParseArguments(args, options,
              (verb, subVerbs) =>
              {
                  // if parsing succeeds the verb name and correct instance
                  // will be passed to onVerbCommand delegate (string,object)
                  invokedVerb = verb;
                  invokedVerbInstance = subVerbs;
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
                    PropertyInfo[] properties = typeof(Options.JobSubOptions).GetProperties();
                    var usage = new StringBuilder();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(invokedVerbInstance) != null)
                        {
                            HttpResponseMessage resp = clara.jobs.Get((string)property.GetValue(invokedVerbInstance)).Result;
                            StreamReader stream = new StreamReader(resp.Content.ReadAsStreamAsync().Result);
                            string str = stream.ReadToEnd();
                            usage.Append(str);
                        }
                        Console.WriteLine("[INFO]:{0}", usage);
                    }
                }
                if (invokedVerb == "user")
                {
                    PropertyInfo[] properties = typeof(Options.UserSubOptions).GetProperties();
                    var usage = new StringBuilder();
                    HttpResponseMessage resp = null;




                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(invokedVerbInstance) != null)
                        {
                            resp = clara.user.Get((string)property.GetValue(invokedVerbInstance)).Result;
                        }
                    }

                    StreamReader stream = new StreamReader(resp.Content.ReadAsStreamAsync().Result);
                    string str = stream.ReadToEnd();
                    usage.Append(str);
                    Console.WriteLine("[INFO]:{0}", usage);
                }


            }
            else
            {
                Console.WriteLine(options.GetUsage());
            }


        }

    }


}
