using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Drawing;
using System.IO;
using System.Reflection;
using dotnet_clara.lib;

namespace dotnet_clara
{
    class Program
    {  
        static void Main(string[] args)
        {          
            Config config = new Config();
            lib.Clara clara = new lib.Clara(config);
           
        }
    }
}
