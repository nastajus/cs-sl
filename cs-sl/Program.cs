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

            // step 2 : to actually deserialize, must convert to a known type.  Wrote my own "known type" : RateLimitResponseContent class, per the sample json response commented below.

            RateLimitResponseContent rateLimit = JsonConvert.DeserializeObject<RateLimitResponseContent>(response.Content);

            // step 3 : create an instance with sensible defaults and overwrite it with json data

            RateLimitResponseContent rateLimit2 =
                new RateLimitResponseContent
                {
                    rate = new RateLimitResponseContent.Thing {limit = 0, remaining = 0, reset = 0},
                    resources = new RateLimitResponseContent.Resources
                    {
                        core = new RateLimitResponseContent.Thing {limit = 0, remaining = 0, reset = 0},
                        search = new RateLimitResponseContent.Thing {limit = 0, remaining = 0, reset = 0}
                    }
                };

            JsonConvert.PopulateObject(response.Content, rateLimit2);

            // step 4 : Delete the class / don't use, instead use an anonymous object (to accomplish the same thing).

            var rateLimit3 = new
            {
                rate = new {limit = 0, remaining = 0, reset = 0}, 
                //resources = new
                //{
                //    core = new {limit = 0, remaining = 0, reset = 0},
                //    search = new {limit = 0, remaining = 0, reset = 0}
                //}
            };

            // nothing happens, type remains with all defaults unchanged
            JsonConvert.PopulateObject(response.Content, rateLimit3);
            JsonConvert.DeserializeAnonymousType(response.Content, rateLimit3);

            //invalid, ref keyword must match as both parameter and argument.
            //JsonConvert.DeserializeAnonymousType(response.Content, ref rateLimit3);

            int foo = 3;
            var x = 5;
            var str =
@"name:dante
age:26
website:http";

            Dog dog = new Doberman();
            dog.name = "fido";


            Type xxx = 1.GetType();
            Console.WriteLine(xxx);
            //"System.Int32"

            Type type1 = dog.GetType();
            Console.WriteLine(type1);

            Type type2 = typeof(Dog);
            Console.WriteLine(type2);
            //"sl_cs.Program.Dog"

            Type type3 = typeof(Dog).GetType();
            Console.WriteLine(type3);
            //"sl_cs.Program.Dog"

            Type type4 = type1.GetType();
            Console.WriteLine(type4);
            //"sl_cs.Program.Dog"

            var typeType = typeof(Type);

            var test1 = type1 == type2;
            var test2 = type2 == type3;
            var test3 = type3 == type1;


            var dante = new Person();

            
            var lassie = new Dog();
            SetNameOfThing(dante, "Dnate");
            SetNameOfThing(lassie, "Lassie");


            DmlPopulate(str,dante);
        }

        static void SetNameOfThing(object p, string name)
        {
            Type pType = p.GetType();
            if (pType.Name == typeof(Dog).Name || pType.Name == typeof(Person).Name)
            {
                //remember the ... doesn't have any info on the instance itself, despite ... coming from instance
                FieldInfo fi = pType.GetField("name");
                fi.SetValue(pType, name);
            }  //System.ArgumentException: 'Field 'name' defined on type 'cs_sl.Program+Person' is not a field on the target object which is of type 'System.RuntimeType'.'


        }

        public class Person
        {
            public string name;
            public string age;
            public string website;

        }
        public class Dog
        {
            public string name;
            public string age;
        }

        public class Doberman : Dog
        {
            public void Bark()
            {
                Console.WriteLine("Woof!");
            }
        }

        static void DmlPopulate(string str, object obj) 
        {
            var lines = str.Split('\n');

            var fields = new Dictionary<string, FieldInfo>();

            foreach (var fieldInfo in obj.GetType().GetFields())
            {
                fields.Add(fieldInfo.Name, fieldInfo);
            }

            foreach (var line in lines)
            {
                var pieces = line.Split(':');
                if(pieces.Length != 2) throw new ArgumentException("The parameter is not valid DML", nameof(str));
                var fieldName = pieces[0];
                if (!fields.ContainsKey(fieldName)) continue;
                var info = fields[fieldName];
                info.SetValue(obj, pieces[1]);

                 
                //"sl_cs.Program.Person.website; Type:string, ByteAlignment:2; Flags:Instace|public"
            }
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
