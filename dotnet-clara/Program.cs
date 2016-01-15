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
            Program p = new Program();
            var result = Parser.Default.ParseArguments<Cli>(args);
            var str = result;
        }

    }


}

