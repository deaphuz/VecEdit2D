using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VecEdit2D.Views;

namespace VecEdit2D
{
    // Main window
    public partial class MainWindow : Window
    {
        private static MainWindow _instance;
        private Toolbox toolboxInstance;
        private GroupView groupViewInstance;
        private AppState appStateInstance;
        private Image refImage;

        private List<Point> points;

        public MainWindow()
        {
            InitializeComponent();
            toolboxInstance = Toolbox.Instance;
            toolboxInstance.Show();
            groupViewInstance = GroupView.Instance;
            groupViewInstance.Show();
            appStateInstance = AppState.Instance;
            refImage = Image.Instance;
            points = new List<Point>();
            appStateInstance.refSelectedShapeGroup = refImage.canvas;
        }

        public static MainWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainWindow();
                }
                return _instance;
            }
        }

        private void RedrawImage()
        {
            MainCanvas.Children.Clear();
            refImage.canvas.draw(MainCanvas);
        }

        public void showDot(double x, double y)
        {
            Ellipse tmp = new Ellipse
            {
                Width = 5,
                Height = 5,
                Stroke = System.Windows.Media.Brushes.DarkBlue,
                StrokeThickness = 1,
                Fill = null,
            };
            MainCanvas.Children.Add(tmp);
            Canvas.SetLeft(tmp, x);
            Canvas.SetTop(tmp, y);
        }

        private void ClearSelectionHandler(object sender, RoutedEventArgs e)
        {
            points = new List<Point>();
            RedrawImage();
        }

        private void ClearSelection()
        {
            points = new List<Point>();
            RedrawImage();
        }

        private void HandleLMBClick(object sender, MouseButtonEventArgs e)
        {

            //stop user if he not selected group
            Type type = typeof(ShapeGroup);
            if (appStateInstance.refSelectedShapeGroup.GetType() != type )
            {
                System.Windows.MessageBox.Show("Select group instead of shape", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

                points.Add(new Point(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y));

                //show dot  : )
                showDot(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y);

                switch (toolboxInstance.currentShape)
                {
                    case "group":
                        if (points.Count == 1)
                        {
                            //add shape
                            ShapeGroup newShape = new ShapeGroup(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y, toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            ClearSelection();
                        }
                        else if (points.Count > 1)
                            ClearSelection();
                        break;
                    case "circle":
                        if (points.Count == 2)
                        {
                            //add shape
                            ShapeCircle newShape = new ShapeCircle(points[0], points[1], toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            ClearSelection();
                        }
                        else if (points.Count > 1)
                            ClearSelection();
                        break;


                    case "rectangle":
                        if (points.Count == 2)
                        {
                            //add shape
                            ShapeRectangle newShape = new ShapeRectangle(points[0], points[1], toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            ClearSelection();
                        }
                        else if (points.Count > 2)
                            ClearSelection();
                        break;
                    case "straightLine":
                        if (points.Count == 2)
                        {
                            //add shape
                            ShapeLine newShape = new ShapeLine(points[0], points[1], toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);

                            ClearSelection();
                        }
                        else if (points.Count > 2)
                            ClearSelection();
                        break;
                }
            
            if (points.Count == 0)
                RedrawImage();


            //update GroupView bar
            groupViewInstance._Update(refImage.canvas);
        }


        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HandlePolygonDrawing();
            }
            else if (e.Key == Key.Escape)
            {
                ClearSelection();
            }
        }

        private void HandleRMBClick(object sender, MouseButtonEventArgs e)
        {
            ShowContextMenu();
        }

        void HandlePolygonDrawing()
        {
            if (points.Count > 0)
            {
                if (points.Count > 2) //minimum 3
                {
                    switch (toolboxInstance.currentShape)
                    {
                        case "polygon":
                            {
                                //add shape
                                ShapePolygon newShape = new ShapePolygon(points, toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                                appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);

                                ClearSelection();
                                break;
                            }
                        case "polyline":
                            {
                                //add shape
                                ShapePolyline newShape = new ShapePolyline(points, toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                                appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);

                                ClearSelection();
                                break;
                            }

                        default:
                            break;

                    }
                    //redraw image
                    if (points.Count == 0)
                        RedrawImage();

                    groupViewInstance._Update(refImage.canvas);
                }
            }
        }


        private void ShowContextMenu()
        {
            Point position = Mouse.GetPosition(MainCanvas);
            MainCanvas.ContextMenu.PlacementTarget = MainCanvas;
            MainCanvas.ContextMenu.Placement = PlacementMode.MousePoint;
        }

        //contextmenu item handlers

        private void TranslateItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //make translation
            Command translateCommand = new TranslateCommand(appStateInstance.refSelectedShapeGroup, toolboxInstance.trX, toolboxInstance.trY);
            translateCommand.execute();
            //redraw image
            RedrawImage();

        }

        private void RotateItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //make rotation
            Command rotateCommand = new RotateCommand(appStateInstance.refSelectedShapeGroup, toolboxInstance.rotAngle);
            rotateCommand.execute();
            //redraw image
            RedrawImage();
        }

        private void ScaleItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //scale
            Command scaleCommand = new ScaleCommand(appStateInstance.refSelectedShapeGroup, toolboxInstance.scaleFactor);
            scaleCommand.execute();

            //redraw image
            RedrawImage();
        }

        private void SetColorItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //set color
            appStateInstance.refSelectedShapeGroup.setColor(toolboxInstance.primaryColor);

            //redraw image
            RedrawImage();
        }

        private void SetOutlineItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //set outline
            appStateInstance.refSelectedShapeGroup.setBorder(toolboxInstance.secondaryColor);

            //redraw image
            RedrawImage();
        }

        private void RemoveFillingItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //set outline
            appStateInstance.refSelectedShapeGroup.setColor(Color.FromArgb(0, 255, 255, 255));

            //redraw image
            RedrawImage();

        }

        private void RemoveOutlineItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //set outline
            appStateInstance.refSelectedShapeGroup.setBorder(Color.FromArgb(0, 255, 255, 255));

            //redraw image
            RedrawImage();
        }
        private void RemoveFigureItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //TODO remove figure
            appStateInstance.refSelectedShapeGroup.setBorder(Color.FromArgb(0, 255, 255, 255));

            //redraw image
            RedrawImage();
        }

        //file new/read/save

        private void NewFileItem_Click(object sender, RoutedEventArgs e)
        {
            points = new List<Point>();
            Image.Instance.New();

            RedrawImage();

            //update GroupView bar
            groupViewInstance._Update(refImage.canvas);
        }

        private void ReadFileItem_Click(object sender, RoutedEventArgs e)
        {
            Image.Instance.Read();

            RedrawImage();

            //update GroupView bar
            groupViewInstance._Update(refImage.canvas);
        }

        private void SaveFileItem_Click(object sender, RoutedEventArgs e)
        {
            Image.Instance.Save();
        }

        private void QuitItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RemoveFigureItem_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog("Info", "Enter name of item to delete (must be in selected group): ");

            if (inputDialog.ShowDialog() == true)
            {
                if(appStateInstance.refSelectedShapeGroup.remove(inputDialog.outputBox.Text) == false)
                {
                    System.Windows.MessageBox.Show("Item not found \n(notice that to remove an item, you must activate\ndelete option in the group in which it is located)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                RedrawImage();
                //update GroupView bar
                groupViewInstance._Update(refImage.canvas);
            }
        }
    }
}
