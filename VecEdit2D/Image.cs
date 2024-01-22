using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;

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

        //read supports only JSON
        public void Read()
        {
            Serializer serializer = new JsonSerializer();
            canvas = serializer.Deserialize();
        }

        public void Save(System.Windows.Controls.Canvas wpfcanvas, string exportType)
        {
            Serializer serializer = null;
            Object data = null;
            switch (exportType)
            {
                case "json":
                    serializer = new JsonSerializer();
                    data = canvas;
                    break;
                case "xaml":
                    serializer = new XamlSerializer();
                    data = wpfcanvas;
                    break;
                case "svg":
                    serializer = new SvgSerializer();
                    data = wpfcanvas;
                    break;
            }
            serializer.Serialize(data);
        }
    }
}
