using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Drawing;
using System.IO;

namespace dotnet_clara
{
    class Program
    {
        static void Main(string[] args)
        {
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
            }
        }

    }


}

