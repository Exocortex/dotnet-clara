using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Drawing;
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

        public Program()
        {
            
            config = new Config();
            
            clara = new lib.Clara(config);
        }
        public async void AsyncTest()
        {
            Random rnd = new Random();
            Console.WriteLine("Begin Rendering Process>>>>>>>>>>>>>>>>");
            string sceneId = "c4afda13-1fa8-4179-a1ec-66c13346ba5a";
            Stream stream1;
            Task<Stream> getStream1 = clara.scene.Render(sceneId, "{width:1200, height:600}", "{command:\"presets/polarCameraSetup\", data:{radius:100,azimuthAngle:10,polarAngle:20}}");
            int filename = rnd.Next();
            Stream file1 = File.Create("g:\\"+filename+".png");
            
            stream1 = await getStream1;
            stream1.CopyTo(file1);
            file1.Close();
            Console.WriteLine("End Rendering Process<<<<<<<<<<<<<<<<<<");
        }
        static void Main()
        {

            Program p = new Program();
            while (true)
            {
                p.config.initializeConfig();

                Console.Write(".Net Clara version 0.2\n");
                Console.Write(">");
                string[] args = Console.ReadLine().Split(' ');
                if (args[0] == "set")
                    p.config.SetConfig(args[1], args[2]);
                if (args[0] == "get")
                    Console.WriteLine("Info {0}:{1}", args[1], p.config.GetOneConfigInfo(args[1]));
                if (args[0] == "help")
                {
                    Console.WriteLine("*************HELP*****************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();

                }
                if (args[0] == "async")
                {
                    p.AsyncTest();
                    p.AsyncTest();
                    p.AsyncTest();
                }
                if (args[0] == "render")
                {
                    Stream stream = p.clara.scene.Render(args[1], "{width:1200, height:600}", "{command:\"presets/polarCameraSetup\", data:{radius:100,azimuthAngle:10,polarAngle:20}}").Result;

                    Stream file = File.Create("g:\\aaa.png");
                    stream.CopyTo(file);
                    file.Close();
                }
                if (args[0] == "command")
                {
                    p.clara.scene.Command(args[1], "{command:\"presets/polarCameraSetup\", data:{radius:100,azimuthAngle:10,polarAngle:20}}").RunSynchronously();
                }
                if (args[0] == "import")
                {
                    string[] filelist = new string[4];
                    filelist[0] = args[2];
                    filelist[1] = args[3];
                    filelist[2] = args[4];
                    filelist[3] = args[5];
                    p.clara.scene.Import(args[1], filelist).RunSynchronously();
                }
                if (args[0] == "export")
                {
                    Stream stream = p.clara.scene.Export(args[1], args[2]).Result;
                    Stream file = File.Create("g:\\test.zip");
                    stream.CopyTo(file);
                    file.Close();
                }
                if (args[0] == "clone")
                {
                    p.clara.scene.Clone(args[1]).RunSynchronously();
                }
                if (args[0] == "delete")
                {
                    p.clara.scene.Delete(args[1]).RunSynchronously();
                }
                if (args[0] == "create")
                {
                    HttpResponseMessage resp = p.clara.scene.Create().Result;
                    Console.WriteLine(resp.StatusCode);
                }
                if (args[0] == "update")
                {
                    string sceneName = null;
                    if (args[2].Length != 0)
                        sceneName = args[2];
                    HttpResponseMessage resp = p.clara.scene.Update(args[1], sceneName);
                    Console.WriteLine(resp.StatusCode);
                }

            }
        }

    }


}

