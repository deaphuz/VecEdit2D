using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public ShapeGroup refSelectedShapeGroup;

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

        private void HandleLMBClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape)
            {
                Shape activeRectangle = (Shape)e.OriginalSource;
                MainCanvas.Children.Remove(activeRectangle);
            }
            else
            {
                points.Add(new Point(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y));
                switch (toolboxInstance.currentShape)
                {
                    case "circle":
                        if (points.Count == 1)
                        {
                            ShapeCircle newShape = new ShapeCircle(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y, 30, toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            Ellipse figure = newShape.getWPFFigure();
                            MainCanvas.Children.Add(figure);
                            Canvas.SetLeft(figure, newShape.center.X - newShape.getWPFRadius());
                            Canvas.SetTop(figure, newShape.center.Y - newShape.getWPFRadius());
                            points = new List<Point>();
                        }
                        else if (points.Count > 1)
                            points = new List<Point>();
                        break;


                    case "rectangle":
                        if (points.Count == 2)
                        {
                            ShapeRectangle newShape = new ShapeRectangle(points[0], points[1], toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            Polygon figure = newShape.getWPFFigure();
                            MainCanvas.Children.Add(figure);
                            //Canvas.SetLeft(figure, newShape.center.X);
                           // Canvas.SetTop(figure, newShape.center.Y);
                            points = new List<Point>();
                        }
                        else if (points.Count > 2)
                            points = new List<Point>();
                        break;
                    case "straightLine":
                        if (points.Count == 2)
                        {
                            ShapeLine newShape = new ShapeLine(points[0], points[1], toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            Line figure = newShape.getWPFFigure();
                            MainCanvas.Children.Add(figure);
                            //Canvas.SetLeft(figure, newShape.center.X);
                            // Canvas.SetTop(figure, newShape.center.Y);
                            points = new List<Point>();
                        }
                        else if (points.Count > 2)
                            points = new List<Point>();
                        break;
                }
            }
            refImage.canvas.draw(MainCanvas);

            //update groupview
            groupViewInstance._Update(refImage.canvas);
        }

        private void HandleRMBClick(object sender, MouseButtonEventArgs e)
        {
            if (points.Count > 0)
            {
                if (points.Count > 2) //minimum 3
                {
                    switch (toolboxInstance.currentShape)
                    {
                        case "polygon":
                        {
                            ShapePolygon newShape = new ShapePolygon(points, toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            Polygon figure = newShape.getWPFFigure();
                            MainCanvas.Children.Add(figure);
                                //Canvas.SetLeft(figure, newShape.center.X);
                                // Canvas.SetTop(figure, newShape.center.Y);
                            points = new List<Point>();
                            break;
                        }


                        case "polyline":
                        {
                            ShapePolyline newShape = new ShapePolyline(points, toolboxInstance.primaryColor, toolboxInstance.secondaryColor);
                            appStateInstance.refSelectedShapeGroup.childGroups.Add(newShape);
                            Polyline figure = newShape.getWPFFigure();
                            MainCanvas.Children.Add(figure);
                                //Canvas.SetLeft(figure, newShape.center.X);
                                // Canvas.SetTop(figure, newShape.center.Y);
                            points = new List<Point>();
                            break;
                        }

                        default:
                            break;
                        
                    }
                    groupViewInstance._Update(refImage.canvas);
                    e.Handled = true;
                }
            }
            else //if there are no points, show contextmenu
            {
                ShowContextMenu();
                e.Handled = true;
            }
        }


        private void ShowContextMenu()
        {
            Point position = Mouse.GetPosition(MainCanvas);
            MainCanvas.ContextMenu.PlacementTarget = MainCanvas;
            MainCanvas.ContextMenu.Placement = PlacementMode.MousePoint;
            MainCanvas.ContextMenu.IsOpen = true;
        }

        //contextmenu item handlers

        private void TranslateItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //make translation
            appStateInstance.refSelectedShapeGroup.translate(10, 10);

            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);

        }

        private void RotateItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //make rotation
            appStateInstance.refSelectedShapeGroup.rotate(100, appStateInstance.refSelectedShapeGroup.center);

            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);
        }

        private void ScaleItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //scale
            appStateInstance.refSelectedShapeGroup.scale(1.2, 1.2, appStateInstance.refSelectedShapeGroup.center);
            
            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);
        }

        private void SetColorItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //set color
            appStateInstance.refSelectedShapeGroup.setColor(Color.FromArgb(0, 0, 0, 255));
            
            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);
        }

        private void SetOutlineItem_Click(object sender, EventArgs e)
        {
            //clear WPF canvas
            MainCanvas.Children.Clear();

            //set outline
            appStateInstance.refSelectedShapeGroup.setColor(Color.FromArgb(0, 255, 0, 255));
            
            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);
        }

        private void RemoveFillingItem_Click(object sender, EventArgs e)
        {
            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);
        }

        private void RemoveOutlineItem_Click(object sender, EventArgs e)
        {
            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);
        }
        private void RemoveFigureItem_Click(object sender, EventArgs e)
        {
            //update groupview
            groupViewInstance._Update(refImage.canvas);

            //redraw WPF canvas
            refImage.canvas.draw(MainCanvas);
        }

        //file new/read/save

        private void NewFileItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReadFileItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFileItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuitItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
