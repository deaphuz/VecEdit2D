using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.AccessControl;
using Newtonsoft.Json.Bson;
using System.Windows.Controls;
using System.Windows;
using VecEdit2D;
using System.Xml;
using System.Windows.Markup;

namespace VecEdit2D
{
    public abstract class Serializer
    {
        //template method
        public virtual void Serialize(Object data)
        {
            try
            {
                string filePath = OpenSaveDialog("");
            }
            catch (Exception e)
            { System.Windows.MessageBox.Show("Invalid file path", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            return;

        }

        //only json
        public ShapeGroup Deserialize()
        {
            string filePath = "";
            try
            {
                filePath = OpenReadDialog();
            }
            catch (Exception e)
            { System.Windows.MessageBox.Show("Invalid file path", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return null; }

            ShapeGroup data = null;
            try
            {
                data = ReadFromJson(filePath);
            }
            catch (Exception e)
            { System.Windows.MessageBox.Show("Error while reading file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return null; }

            System.Windows.MessageBox.Show("Image read", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            return data;
        }


        //deserialization
        public ShapeGroup ReadFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);

            return DeserializeFromJson<ShapeGroup>(json);
        }

        static ShapeGroup DeserializeFromJson<ShapeGroup>(string json)
        {
            ShapeGroup deserializedItems = JsonConvert.DeserializeObject<ShapeGroup>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return deserializedItems;
        }

        public string OpenReadDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "data"; // Default file name
            dialog.DefaultExt = ".json"; // Default extension
            dialog.Filter = "JSON files (.json)|*.json"; // Filter

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                return filename;
            }
            else return null;
        }

        public string OpenSaveDialog(string extension)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            switch (extension)
            {
                case "json":
                    dialog.DefaultExt = ".json";
                    dialog.Filter = "JSON files (.json)|*.json"; // Filter
                    break;
                case "svg":
                    dialog.DefaultExt = ".svg";
                    dialog.Filter = "SVG files (.svg)|*.svg"; // Filter
                    break;
                case "xml":
                    dialog.DefaultExt = ".xml";
                    dialog.Filter = "XML files (.xml)|*.xml"; // Filter
                    break;
                default:
                    dialog.DefaultExt = "";
                    dialog.Filter = ""; // Filter
                    break;
            }

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                return filename;
            }
            else return null;
        }
    }



    public class JsonSerializer : Serializer
    {
        //template method
        public override void Serialize(Object data)
        {
            string filePath = "";
            try
            {
                filePath = OpenSaveDialog("json");
            }
            catch (Exception e)
            {
                { System.Windows.MessageBox.Show("Failed to save image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            }

            try
            {
                SaveToJson((ShapeGroup)data, filePath);
            }
            catch (Exception e)
            {
                { System.Windows.MessageBox.Show("Failed to save image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            }

        }

        //serialization
        public void SaveToJson(ShapeGroup data, string filePath)
        {
            string json = SerializeToJson(data);

            File.WriteAllText(filePath, json);
        }

        static string SerializeToJson<ShapeGroup>(ShapeGroup obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }

    public class XamlSerializer : Serializer
    {
        //template method
        public override void Serialize(Object data)
        {
            string filePath = "";
            try
            {
                filePath = OpenSaveDialog("xml");
            }
            catch (Exception e)
            {
                { System.Windows.MessageBox.Show("Failed to export image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            }

            try
            {
                SaveToXaml((Canvas)data, filePath);
            }
            catch (Exception e)
            {
                { System.Windows.MessageBox.Show("Failed to export image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            }

        }


        //export WPF canvas to svg
        public void SaveToXaml(Canvas data, string filePath)
        {
            string xaml = XamlWriter.Save(data);

            File.WriteAllText(filePath, xaml);
        }
    }

    public class SvgSerializer : Serializer
    {
        //template method
        public override void Serialize(Object data)
        {
            string filePath = "";
            try
            {
                filePath = OpenSaveDialog("xml");
            }
            catch (Exception e)
            {
                { System.Windows.MessageBox.Show("Failed to export image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            }

            try
            {
                //SaveToXml((Canvas)data, filePath);
            }
            catch (Exception e)
            {
                { System.Windows.MessageBox.Show("Failed to export image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            }

        }

    }
}