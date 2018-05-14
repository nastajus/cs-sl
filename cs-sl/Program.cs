﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;


namespace cs_sl
{
    class Program
    {
        static void Main(string[] args)
        {

            //step 1 : create normal object (POCO)

            Cat felix = new Cat() { name = "felix", birth = new DateTime(2012, 12, 24) } ;


            // step 2 : serialize object (to persist in file system)
            string jsonCat = JsonConvert.SerializeObject(felix);

            string filename = "serialized.txt";

            string path = Path.Combine(Directory.GetCurrentDirectory(), filename);

            File.WriteAllText(path, jsonCat);

            //Directory.GetCurrentDirectory
            //Environment.CurrentDirectory


            // step 3 : deserialize file (into object again)

            string readText = File.ReadAllText(path);

            object deserializedObject = JsonConvert.DeserializeObject(readText);

            //'Unable to cast object of type 'Newtonsoft.Json.Linq.JObject' to type 'cs_sl.Cat'.'
            Cat schrodinger = (Cat) deserializedObject;


        }
    }

    class Cat
    {
        public string name;
        public DateTime birth;

    }
}