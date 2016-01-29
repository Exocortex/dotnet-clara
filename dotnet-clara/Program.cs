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
            //Config config = new Config();
            string username = "godlzr";
            string apiToken = "546559a8-eeb3-4376-be59-08628b9606bd";
            string host = "clara.io";
            string sceneId = "8757e35c-0d22-4281-8432-420f6bde3f24";
            lib.Clara clara = new lib.Clara(username, apiToken, host);
            string jobId = "8ca62d7f-2fbc-4d3a-9894-7fbe513548ee";
            //IRestResponse response = clara.jobs.Get(jobId);

            var bytes = clara.scene.Render(sceneId, "{width:800, height:600}", "{}");
            var imagePath = "g:\\aaa.png";

            using (var imageFile = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
            var a = 6;
           
        }
    }
}
