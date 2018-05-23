using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using RestSharp;
using RestSharp.Authenticators;


namespace cs_sl
{
    class Program
    {
        static void Main(string[] args)
        {

            Phase1();
            Phase2();
            Phase3();

        }

        static void Phase1()
        {
            //step 1 : create normal object (POCO)

            Cat felix = new Cat() { name = "felix", birth = new DateTime(2012, 12, 24) };


            // step 2 : serialize object (to persist in file system)
            string jsonCat = JsonConvert.SerializeObject(felix);

            string filename = "serialized.txt";

            string path = Path.Combine(Directory.GetCurrentDirectory(), filename);

            File.WriteAllText(path, jsonCat);

            //Directory.GetCurrentDirectory
            //Environment.CurrentDirectory


            // step 3 : deserialize file (into object again)

            string readText = File.ReadAllText(path);

            Cat schrodinger = JsonConvert.DeserializeObject<Cat>(readText);

            //object deserializedObject = JsonConvert.DeserializeObject(readText);
            //'Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'cs_sl.Cat'.'
            //Cat schrodinger = (Cat) deserializedObject;


            // interesting to dos later as bonus to deepen understanding: 
            // later 1: nest object of same type, see if system gets confused.
            // later 2: can try using "serializer settings" i discovered to <<store types and apply those types>> when deserialized.

        }

        static void Phase2()
        {
            //step 1 : make a rest call to an endpoint. Selected github, this one: https://api.github.com/rate_limit

            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.github.com");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");

            var request = new RestRequest();
            request.Resource = "rate_limit";

            IRestResponse response = client.Execute(request);
            //client.Ex

            // step 2 : to actually deserialize, must convert to a known type.  Wrote my own "known type" : RateLimitResponseContent class, per the sample json response commented below.

            RateLimitResponseContent rateLimit = JsonConvert.DeserializeObject<RateLimitResponseContent>(response.Content);

            // step 3 : create an instance with sensible defaults and overwrite it with json data

            RateLimitResponseContent rateLimit2 =
                new RateLimitResponseContent
                {
                    rate = new RateLimitResponseContent.Thing { limit = 0, remaining = 0, reset = 0 },
                    resources = new RateLimitResponseContent.Resources
                    {
                        core = new RateLimitResponseContent.Thing { limit = 0, remaining = 0, reset = 0 },
                        search = new RateLimitResponseContent.Thing { limit = 0, remaining = 0, reset = 0 }
                    }
                };

            JsonConvert.PopulateObject(response.Content, rateLimit2);

            // step 4 : Delete the class / don't use, instead use an anonymous object (to accomplish the same thing).

            var rateLimit3 = new
            {
                rate = new { limit = 0, remaining = 0, reset = 0 },
                //resources = new
                //{
                //    core = new {limit = 0, remaining = 0, reset = 0},
                //    search = new {limit = 0, remaining = 0, reset = 0}
                //}
            };

            // nothing happens, type remains with all defaults unchanged
            JsonConvert.PopulateObject(response.Content, rateLimit3);
            JsonConvert.DeserializeAnonymousType(response.Content, rateLimit3);
        }
        
        static void Phase3()
        {

        }
    }

    class Cat
    {
        public string name;
        public DateTime birth;

    }

    class RateLimitResponseContent
    {
        public Resources resources { get; set; }
        public Thing rate { get; set; }

        public class Resources
        {
            public Thing core { get; set; }
            public Thing search { get; set; }
        }

        public class Thing
        {
            public uint limit;
            public uint remaining;
            public uint reset;
        }
    }



    /*
    {
        "resources": {
            "core": {
                "limit": 60,
                "remaining": 54,
                "reset": 1526329026
            },
            "search": {
                "limit": 10,
                "remaining": 10,
                "reset": 1526328488
            }
       },
        "rate": {
            "limit": 60,
            "remaining": 54,
            "reset": 1526329026
        }
    }
    */

}
