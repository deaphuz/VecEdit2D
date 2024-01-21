using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace VecEdit2D
{
    //System.Text.Json
    /* public class Serializer
     {
         public Serializer() { }

         public void SaveToJson(ShapeGroup data, string filePath)
         {
             string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
             File.WriteAllText(filePath, json);
         }

         // Read database from a JSON file
         public ShapeGroup ReadFromJson(string filePath)
         {
             string json = File.ReadAllText(filePath);
             return JsonSerializer.Deserialize<ShapeGroup>(json);
         }

     }*/

    //Newtonsoft.Json

    public class Serializer
    {
        public void SaveToJson(ShapeGroup data, string filePath)
        {
            string json = SerializeToJson(data);
            File.WriteAllText(filePath, json);
        }

        // Read database from a JSON file
        public ShapeGroup ReadFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);


            return DeserializeFromJson<ShapeGroup>(json);
        }

        static string SerializeToJson<ShapeGroup>(ShapeGroup obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        static ShapeGroup DeserializeFromJson<ShapeGroup>(string json)
        {
            return JsonConvert.DeserializeObject<ShapeGroup>(json);
        }
    }
}


