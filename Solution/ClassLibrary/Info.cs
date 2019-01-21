using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class Info
    {
        public void fInfo()
        {
                string path = "/public/APIInfo.json";
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();

                JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable map = new Hashtable();
                foreach (JProperty col in jo.Properties())
                {
                    //Console.WriteLine("{0} : {1}", col.Name, col.Value);
                    map.Add(col.Name, col.Value);
                }

                string SelectInfo = string.Format("{0}", map["select"]);
                string InsertInfo = string.Format("{0}", map["insert"]);
                string UpdateInfo = string.Format("{0}", map["update"]);
                string DeleteInfo = string.Format("{0}", map["delete"]);
        }
    }
}
