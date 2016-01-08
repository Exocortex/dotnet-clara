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
    class Program
    {
        static void Main()
        {
            Console.Write(".Net Clara version 0.1\n");
            Config config = new Config();
            config.initializeConfig();
            while (true)
            {
                Console.Write(">");
                string[] args = Console.ReadLine().Split(' ');
                if (args[0] == "set")
                    config.SetConfig(args[1], args[2]);
                if (args[0] == "get")
                    Console.WriteLine("info {0}:{1}",args[1], config.GetOneConfigInfo(args[1]));
            }


            /*Config config = new Config();
            
            var client = new RestClient();
            client.BaseUrl = new Uri("http://clara.io/api/scenes/");
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator("godlzr", "546559a8-eeb3-4376-be59-08628b9606bd");

            var request = new RestRequest();
            request.Resource = "c4afda13-1fa8-4179-a1ec-66c13346ba5a/render?width=800$height=600";

            IRestResponse response = client.Execute(request);

            var imagePath = "g:\\aaa.png";
            var bytes = response.RawBytes;
            using (var imageFile = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }*/
        }

    }


}

