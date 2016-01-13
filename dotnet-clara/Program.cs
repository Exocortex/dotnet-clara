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
        static void Main()
        {
            Console.Write(".Net Clara version 0.2\n");
            Config config = new Config();
            config.initializeConfig();
            lib.Clara clara = new lib.Clara(config);
            while (true)
            {
                Console.Write(">");
                string[] args = Console.ReadLine().Split(' ');
                if (args[0] == "set")
                    config.SetConfig(args[1], args[2]);
                if (args[0] == "get")
                    Console.WriteLine("Info {0}:{1}", args[1], config.GetOneConfigInfo(args[1]));
                if (args[0] == "help")
                {
                    Console.WriteLine("*************HELP*****************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();

                }
                if (args[0] == "render")
                {
                    Stream stream = clara.scene.Render(args[1], "{width:1200, height:600}", "{command:\"presets/polarCameraSetup\", data:{radius:100,azimuthAngle:10,polarAngle:20}}");

                    Stream file = File.Create("g:\\aaa.png");
                    stream.CopyTo(file);
                    file.Close();
                }
                if (args[0] == "command")
                {
                    clara.scene.Command(args[1], "{command:\"presets/polarCameraSetup\", data:{radius:100,azimuthAngle:10,polarAngle:20}}");
                }
                if (args[0] == "export")
                {
                    Stream stream = clara.scene.Export(args[1], args[2]);
                    Stream file = File.Create("g:\\test.zip");
                    stream.CopyTo(file);
                    file.Close();
                }
                if (args[0] == "clone")
                {
                    clara.scene.Clone(args[1]);
                }
                if (args[0] == "delete")
                {
                    clara.scene.Delete(args[1]);
                }

            }
        }

    }


}

