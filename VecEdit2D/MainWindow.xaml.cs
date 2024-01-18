using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

            appStateInstance.
            refSelectedShapeGroup = refImage.canvas;

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
                            refSelectedShapeGroup.childGroups.Add(newShape);
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
                            refSelectedShapeGroup.childGroups.Add(newShape);
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
                            refSelectedShapeGroup.childGroups.Add(newShape);
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
                            refSelectedShapeGroup.childGroups.Add(newShape);
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
                            refSelectedShapeGroup.childGroups.Add(newShape);
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

                        //if its polyline or polygon, draw it
                }
            }
        }
        /*
        private void AddShape(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape)
            {
                Shape activeRectangle = (Shape)e.OriginalSource;
                MainCanvas.Children.Remove(activeRectangle);
            }
            else
            {
                switch (toolboxInstance.Shape)
                {
                    case "circle":
                        ShapeCircle newShape = new ShapeCircle(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y, 30);
                        canvas.Add(newShape);
                        Ellipse figure = newShape.getWPFFigure();
                        MainCanvas.Children.Add(figure);
                        Canvas.SetLeft(figure, newShape.center.X - newShape.getWPFRadius());
                        Canvas.SetTop(figure, newShape.center.Y - newShape.getWPFRadius());
                        // canvas.Add(new ShapeCircle(30));
                        /*  newShape = new System.Windows.Shapes.Ellipse
                          {
                              Height = toolboxInstance.Radius,
                              Width = toolboxInstance.Radius,
                              Fill = customColor,
                              StrokeThickness = toolboxInstance.StrokeThickness,
                              Stroke = Brushes.Black
                          };
                        break;
                  /*  case "triangle":
                        newShape = new System.Windows.Shapes.Path
                        {
                            Width = 50,
                            Height = 50,
                            Fill = customColor,
                            StrokeThickness = 3,
                            Stroke = Brushes.Black
                        };
                        break;
                    case "rectangle":
                        newShape = new System.Windows.Shapes.Rectangle
                        {
                            Width = toolboxInstance.Width,
                            Height = toolboxInstance.Height,
                            Fill = customColor,
                            StrokeThickness = toolboxInstance.StrokeThickness,
                            Stroke = Brushes.Black
                        };
                        break;
                    case "straightLine":
                        newShape = new System.Windows.Shapes.Line
                        {
                            Width = 50,
                            Height = 50,
                            Fill = customColor,
                            StrokeThickness = 3,
                            Stroke = Brushes.Black
                        };
                        break;
                }
                Canvas.SetLeft(newShape, Mouse.GetPosition(MainCanvas).X - newShape.Width/2);
                Canvas.SetTop(newShape, Mouse.GetPosition(MainCanvas).Y - newShape.Height/2);

                MainCanvas.Children.Add(newShape);
            }
        }*/

        private void ShowPopupMenu(object sender, MouseButtonEventArgs e)
        {

        }

        private void TranslateItem_Click(object sender, EventArgs e)
        {

        }

        private void RotateItem_Click(object sender, EventArgs e)
        {

        }

        private void ScaleItem_Click(object sender, EventArgs e)
        {

        }

        private void SetColorItem_Click(object sender, EventArgs e)
        {

        }

        private void SetOutlineItem_Click(object sender, EventArgs e)
        {

        }

        private void RemoveFillingItem_Click(object sender, EventArgs e)
        {

        }

        private void RemoveOutlineItem_Click(object sender, EventArgs e)
        {

        }
        private void RemoveFigureItem_Click(object sender, EventArgs e)
        {

        }
        private void RemoveFigureItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewFileItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFileItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFileItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuitItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
