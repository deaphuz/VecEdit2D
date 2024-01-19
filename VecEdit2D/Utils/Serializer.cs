using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VecEdit2D
{
    public class Serializer
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

    }

}
