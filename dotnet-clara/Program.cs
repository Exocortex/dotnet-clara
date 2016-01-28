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
                            HttpResponseMessage resp = clara.jobs.GetAsync((string)property.GetValue(invokedVerbInstance)).Result;
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
                    //  Need better method for Function mapping
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(invokedVerbInstance) != null)
                        {
                            if (property.Name == "username")
                            {
                                resp = clara.user.Get((string)property.GetValue(invokedVerbInstance)).Result;
                                break;
                            }     
                            if (property.Name == "updateQuery")
                            {
                                string[] updateQuery = (string[])property.GetValue(invokedVerbInstance);
                                resp = clara.user.Update(updateQuery[0],updateQuery[1]).Result;
                                break;
                            }
                            if (property.Name == "listScenesQUery")
                            {
                                string[] listScenesQUery = (string[])property.GetValue(invokedVerbInstance);
                                resp = clara.user.ListScenes(listScenesQUery[0], listScenesQUery[1]).Result;
                                break;
                            }
                            if (property.Name == "listJobsQuery")
                            {
                                string[] listJobsQuery = (string[])property.GetValue(invokedVerbInstance);
                                resp = clara.user.ListJobs(listJobsQuery[0], listJobsQuery[1]).Result;
                                break;
                            }
                        }
                    }
                    StreamReader stream = new StreamReader(resp.Content.ReadAsStreamAsync().Result);
                    string str = stream.ReadToEnd();
                    usage.Append(str);
                    Console.WriteLine("[INFO]:{0}", usage);
                }
                if (invokedVerb == "scene")
                {
                    PropertyInfo[] properties = typeof(Options.SceneSubOptions).GetProperties();
                    var usage = new StringBuilder();
                    HttpResponseMessage resp = null;
                    Stream output = null;
                    string filePath = null;
                    //  Need better method for Function mapping
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(invokedVerbInstance) != null)
                        {
                            if (property.Name == "libraryQuery")
                            {
                                resp = clara.scene.Library((string)property.GetValue(invokedVerbInstance)).Result;
                                break;
                            }  
                            if (property.Name == "getSceneId")
                            {
                                string sceneId = (string)property.GetValue(invokedVerbInstance);
                                resp = clara.scene.Get(sceneId).Result;
                                break;
                            }
                            if (property.Name == "updateQuery")
                            {
                                string[] updateQuery = (string[])property.GetValue(invokedVerbInstance);
                                resp = clara.scene.Update(updateQuery[0], updateQuery[1]).Result;
                                break;
                            }
                            if (property.Name == "create")
                            {
                                if((bool)property.GetValue(invokedVerbInstance))
                                    resp = clara.scene.Create().Result;
                            }
                            if (property.Name == "cloneSceneId")
                            {
                                string cloneSceneId = (string)property.GetValue(invokedVerbInstance);
                                resp = clara.scene.Clone(cloneSceneId).Result;
                                break;
                            }
                            if (property.Name == "deleteSceneId")
                            {
                                string deleteSceneId = (string)property.GetValue(invokedVerbInstance);
                                resp = clara.scene.Delete(deleteSceneId).Result;
                                break;
                            }
                            if (property.Name == "commandParams")
                            {
                                string[] commandParams = (string[])property.GetValue(invokedVerbInstance);
                                resp = clara.scene.Command(commandParams[0], commandParams[1]).Result;
                                break;
                            }
                            if (property.Name == "importParams")
                            {
                                string[] importParams = (string[])property.GetValue(invokedVerbInstance);
                                string[] fileList = new string[importParams.Length - 1];
                                Array.Copy(importParams, 1, fileList, 0, fileList.Length);
                                resp = clara.scene.Import(importParams[0], fileList).Result;
                                break;
                            }
                            if (property.Name == "exportParams")
                            {
                                string[] exportParams = (string[])property.GetValue(invokedVerbInstance);
                                filePath = exportParams[2];
                                Console.WriteLine("[INFO]:Exporting...");
                                output = clara.scene.Export(exportParams[0], exportParams[1]).Result;
                                break;
                            }
                            if (property.Name == "renderParams")
                            {
                                string[] renderParams = (string[])property.GetValue(invokedVerbInstance);
                                filePath = renderParams[3];
                                Console.WriteLine("[INFO]:Rendering...");
                                output = clara.scene.Render(renderParams[0], renderParams[1], renderParams[2]).Result;
                                break;
                            }
                            if (property.Name == "thumbnailParams")
                            {
                                string[] thumbnailParams = (string[])property.GetValue(invokedVerbInstance);
                                string sceneId = thumbnailParams[0];
                                filePath = thumbnailParams[1];
                                output = clara.scene.Thumbnail(sceneId).Result;
                                break;
                            }
                        }
                    }
                    if (resp != null)
                    {
                        StreamReader stream = new StreamReader(resp.Content.ReadAsStreamAsync().Result);
                        string str = stream.ReadToEnd();
                        usage.Append(str);
                        Console.WriteLine("[INFO]:{0}", usage);
                    }
                    if (output != null)
                    {
                        Stream file = File.Create(filePath);
                        output.CopyTo(file);
                        Console.WriteLine("[INFO]:File saved in {0}", filePath);
                        file.Close();
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
