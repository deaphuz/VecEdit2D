using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        // public ObservableCollection<ShapeGroup> WPFobservableCanvas { get; set; }
        private Image()
        {
            canvas = new ShapeGroup()
            {
                childGroups = new List<ShapeGroup>(),
                name = "canvas",
                center = new System.Windows.Point(0, 0),
                Color = System.Windows.Media.Colors.White,
                StrokeColor = System.Windows.Media.Colors.Black,
                StrokeThickness = 2,
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
          //  WPFobservableCanvas.Add(group);
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
    }
}
