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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VecEdit2D.Views;

namespace VecEdit2D
{
    public partial class GroupView : Window
    {
        private static GroupView _instance;
        private ShapeGroup selectedPreviousItem;
        private Style treeViewItemStyle;
        private GroupView()
        {
            InitializeComponent();
            treeViewItemStyle = new Style(typeof(TreeViewItem));
            treeViewItemStyle.Setters.Add(new EventSetter(TreeViewItem.PreviewMouseRightButtonDownEvent, new MouseButtonEventHandler(TreeItem_RMBClick)));

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
                IsExpanded = true,
                ItemContainerStyle = treeViewItemStyle,

            };
            treeItem.Selected += TreeItem_Selected;
            if(item.childGroups != null) //!!!
            {
                foreach (var shapeGroup in item.childGroups)
                {
                    treeItem.Items.Add(Update(shapeGroup));
                }
            }

            return treeItem;
        }

        private void TreeItem_Selected(object sender, RoutedEventArgs e)
        {
            // Get the selected TreeViewItem
            if (sender is TreeViewItem selectedItem)
            {
                if (selectedItem.IsSelected)
                {
                    selectedPreviousItem = AppState.Instance.refSelectedShapeGroup;
                    ShapeGroup foundedShapeGroup = selectedItem.Tag as ShapeGroup;
                    AppState.Instance.refSelectedShapeGroup = foundedShapeGroup;


                    /*
                    if (selectedItem.Tag is ShapeGroup selectedShapeGroup)
                    {
                        MessageBox.Show($"Selected: {selectedItem.name}");
                    }*/
                }
            }
        }

        private void TreeItem_RMBClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // Get the TreeViewItem that was right-clicked
                if (sender is TreeViewItem clickedItem)
                {
                    if (clickedItem.IsSelected)
                    {

                        // Access the associated ShapeGroup using the Tag property
                        if (clickedItem.Tag is ShapeGroup selectedShapeGroup)
                        {
                            var changeNameDialog = new InputDialog("Info", "Enter new Shape/Group name: ");

                            if(changeNameDialog.ShowDialog() == true)
                            {
                                if(Image.Instance.canvas.find(changeNameDialog.outputBox.Text) != null)
                                {
                                    System.Windows.MessageBox.Show("Group with that name already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }

                                selectedShapeGroup.name = changeNameDialog.outputBox.Text;
                                _Update(Image.Instance.canvas);
                            }
                            
                        }

                        e.Handled = true;
                    }
                }
            }
        }
    }
}