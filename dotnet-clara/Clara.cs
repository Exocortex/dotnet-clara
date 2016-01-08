using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Drawing;
using System.IO;
using dotnet_clara.lib;
using CommandLine;
using CommandLine.Text;

namespace dotnet_clara
{
    class Clara
    {
        static void Main()
        {
            Console.Write(".Net Clara version 0.1\n");
            Config config = new Config();
            config.initializeConfig();
            Request request = new Request();
            while (true)
            {
                Console.Write(">");
                string[] args = Console.ReadLine().Split(' ');
                if (args[0] == "set")
                    config.SetConfig(args[1], args[2]);
                if (args[0] == "get")
                    Console.WriteLine("info {0}:{1}", args[1], config.GetOneConfigInfo(args[1]));
                else
                    request.Send(args);

            }
        }

    }


}

