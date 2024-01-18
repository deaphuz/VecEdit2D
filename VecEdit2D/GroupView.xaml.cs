using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VecEdit2D
{
    public partial class GroupView : Window
    {
        private static GroupView _instance;
        private GroupView()
        {
            InitializeComponent();
            //  DataContext = Image.Instance.WPFobservableCanvas;
        }
        public static GroupView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GroupView();
                }
                return _instance;
            }
        }

        public void Clear()
        {
            LayersView.Items.Clear();
        }

        public void _Update(ShapeGroup mainCanvas)
        {
            LayersView.Items.Clear();
            LayersView.Items.Add(Update(mainCanvas));
        }

        public TreeViewItem Update(ShapeGroup item)
        {
            TreeViewItem treeItem = new TreeViewItem()
            {
                Header = item.name,
                Tag = item, //its only a reference  : )

            };
            treeItem.Selected += TreeItem_Selected;
            foreach (var shapeGroup in item.childGroups)
            {
                treeItem.Items.Add(Update(shapeGroup));
            }
            return treeItem;
        }

        private void TreeItem_Selected(object sender, RoutedEventArgs e)
        {
            // Get the selected TreeViewItem
            if (sender is TreeViewItem selectedItem)
            {
                ShapeGroup foundedShapeGroup = 
                refSelectedShapeGroup
                selectedItem.Name
                // Access the associated ShapeGroup using the Tag property
                if (selectedItem.Tag is ShapeGroup selectedShapeGroup)
                {
                    // Do something with the selected ShapeGroup
                    MessageBox.Show($"Selected: {selectedShapeGroup.name}");
                }
            }
        }
    }
}