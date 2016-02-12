using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using dotnet_clara.lib;
using Ionic.Zip;
using ICSharpCode.SharpZipLib.GZip;

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
            string sceneId = "00330083-d9ea-4013-a7bc-bc26c605991f";
            lib.Clara clara = new lib.Clara(username, apiToken, host);
            string jobId = "8ca62d7f-2fbc-4d3a-9894-7fbe513548ee";


            var bytes = clara.scenes.Command(sceneId, "{command:\"presets/polarCameraSetup\", data:{radius:0.5,azimuthAngle:0,polarAngle:0}}");
            //var bytes = clara.scenes.Render(sceneId, "{height:600, width:800}", "{command:\"presets/polarCameraSetup\", data:{radius:0.5,azimuthAngle:0,polarAngle:0}}");
            //var bytes = resp.RawBytes;
            //Stream stream = new MemoryStream(bytes);

            /*using (var client = new WebClient())
            {
                client.DownloadFile(url, "g:\\2.zip");
            }*/


            /*byte[] file = File.ReadAllBytes("g:\\2.zip");
            byte[] decompressed = Decompress(bytes);
            Stream stream = new MemoryStream(decompressed);
            using (ZipFile zip = ZipFile.Read(stream))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract("g:\\test\\");
                }
            }*/
            /*var imagePath = "g:\\aaa.png";

            using (var imageFile = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }*/
            var a = 6;
           
        }

        static void ExtractGZipSample(string gzipFileName, string targetDir)
        {

            // Use a 4K buffer. Any larger is a waste.    
            byte[] dataBuffer = new byte[4096];

            using (System.IO.Stream fs = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read))
            {
                using (GZipInputStream gzipStream = new GZipInputStream(fs))
                {

                    // Change this to your needs
                    string fnOut = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(gzipFileName));

                    using (FileStream fsOut = File.Create(fnOut))
                    {
                        ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(gzipStream, fsOut, dataBuffer);
                    }
                }
            }
        }
        static byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }
}
