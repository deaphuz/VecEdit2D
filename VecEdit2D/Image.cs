using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace VecEdit2D
{
    [Serializable]
    public class Image
    {
        //Singleton
        private static Image _instance;

        public ShapeGroup canvas { get; set; }
        private Image()
        {
            canvas = new ShapeGroup()
            {
                childGroups = new List<ShapeGroup>(),
                name = "canvas",
                center = new System.Windows.Point(0, 0),
                color = System.Windows.Media.Colors.White,
                strokeColor = System.Windows.Media.Colors.Black,
                strokeThickness = 2,
            };
        }
    
        public static Image Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Image();
                }
                return _instance;
            }
        }

        //for treeview
        public void Add(ShapeGroup group)
        {
            canvas.childGroups.Add(group);
        }

        public void Remove(string name)
        {
     /*       ShapeGroup itemToRemove = canvas.FirstOrDefault(item => item.name == name);
            if (itemToRemove != null)
            {
                WPFobservableCanvas.Remove(itemToRemove);
                canvas.RemoveAll(item => item.name == name);
            }*/
        }

        public void New()
        {
            canvas = new ShapeGroup()
            {
                childGroups = new List<ShapeGroup>(),
                name = "canvas",
                center = new System.Windows.Point(0, 0),
                color = System.Windows.Media.Colors.White,
                strokeColor = System.Windows.Media.Colors.Black,
                strokeThickness = 2,
            };
        }

        public void Read()
        {
            string filename = "";
            try { filename = OpenReadDialog(); }
            catch (Exception) { System.Windows.MessageBox.Show("Invalid file path", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            Serializer serializer = new Serializer();

            ShapeGroup output = new ShapeGroup();
            try { output = serializer.ReadFromJson(filename); }
            catch (Exception) { System.Windows.MessageBox.Show("Failed to read the image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            System.Windows.MessageBox.Show("Image read", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            canvas = output;
        }

        public void Save()
        {
            //save to db
            string filename = "";
            try { filename = OpenSaveDialog(); }
            catch (Exception) { System.Windows.MessageBox.Show("Invalid file path", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            ShapeGroup output = Image.Instance.canvas;
            Serializer serializer = new Serializer();
            try { serializer.SaveToJson(output, filename); }
            catch (Exception) { System.Windows.MessageBox.Show("Failed to save the image", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            System.Windows.MessageBox.Show("Image saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
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

        public string OpenSaveDialog()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "data"; // Default file name
            dialog.DefaultExt = ".json"; // Default extension
            dialog.Filter = "JSON files (.json)|*.json"; // Filter

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;
                return filename;
            }
            else return null;
        }
    }
}
