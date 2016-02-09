﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using RestSharp;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using dotnet_clara.lib.resources;

namespace dotnet_clara.lib
{
    public class Method
    {
        private string resource;
        private RestClient client;
        private Config config;
        private Config.ConfigInfo configInfo;

        public Method(string resource)
        {
            this.resource = resource;
            this.config = new Config();
            this.configInfo = config.ReadConfig(null);
            this.client = new RestClient("https://" + configInfo.host + configInfo.basePath + "/" + this.resource + "/");
            this.client.Authenticator = new RestSharp.HttpBasicAuthenticator(configInfo.username, configInfo.apiToken);
        }

        public class NewtonsoftJsonSerializer : RestSharp.Serializers.ISerializer, RestSharp.Deserializers.IDeserializer
        {
            private Newtonsoft.Json.JsonSerializer jsonSerializer;

            public NewtonsoftJsonSerializer()
            {
                this.jsonSerializer = new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
            }

            public string ContentType
            {
                get { return "application/json"; } // Probably used for Serialization?
                set { }
            }

            public string DateFormat { get; set; }

            public string Namespace { get; set; }

            public string RootElement { get; set; }

            public string Serialize(object obj)
            {
                return JsonConvert.SerializeObject(obj);
            }

            public T Deserialize<T>(RestSharp.IRestResponse response)
            {
                var content = response.Content;

                using (var stringReader = new StringReader(content))
                {
                    using (var jsonTextReader = new JsonTextReader(stringReader))
                    {
                        return jsonSerializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }
        }

        public IRestResponse Request(string method, RestRequest request, bool reqOutput = false)
        {
            
            IRestResponse response = null;
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
            switch (method)
            {
                case "post":
                    request.Method = RestSharp.Method.POST;                   
                    response = this.client.Execute(request);
                    string location = "";
                    foreach (var header in response.Headers)
                    {
                        if (header.Name == "Location")
                            location = header.Value.ToString();
                    }
                    if (reqOutput && location != "")
                    {
                        RestRequest newRequest = new RestRequest(RestSharp.Method.GET);

                        this.client.BaseUrl = location;
                        IRestResponse outputResponse = this.client.Execute(newRequest);

                        while (outputResponse.ResponseUri.ToString() == this.client.BaseUrl)
                        {
                            Thread.Sleep(2000);
                            outputResponse = this.client.Execute(newRequest);
                        }                      
                        return outputResponse;
                    }
                    break;
                case "get":
                    request.Method = RestSharp.Method.GET;
                    response = this.client.Execute(request);
                    break;
                case "delete":
                    request.Method = RestSharp.Method.DELETE;
                    response = this.client.Execute(request);
                    break;
                case "put":
                    request.Method = RestSharp.Method.PUT;
                    response = this.client.Execute(request);
                    break;
            }
            return response;
        }

        public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain, look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                        }
                    }
                }
            }
            return isOk;
        }
    }
}
