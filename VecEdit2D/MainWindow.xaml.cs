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
        private ContextMenu contextMenuInstance;
        Brush customColor;
        Random r = new Random();

        List<Group> canvas;

        List<Point> points;

        public MainWindow()
        {
            toolboxInstance = Toolbox.Instance;
           // contextMenu = ContextMenu.Instance;
            toolboxInstance.Show();
            canvas = new List<Group>();
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
                customColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 255)));
                points.Add(new Point(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y));
                switch (toolboxInstance.Shape)
                {
                    case "circle":
                        if (points.Count == 1)
                        {
                            ShapeCircle newShape = new ShapeCircle(Mouse.GetPosition(MainCanvas).X, Mouse.GetPosition(MainCanvas).Y, 30);
                            canvas.Add(newShape);
                            Ellipse figure = newShape.getWPFFigure();
                            MainCanvas.Children.Add(figure);
                            Canvas.SetLeft(figure, newShape.center.X - newShape.getWPFRadius());
                            Canvas.SetTop(figure, newShape.center.Y - newShape.getWPFRadius());
                        }
                        else if (points.Count > 1)
                            points = new List<Point>();
                        break;


                    case "rectangle":
                        if(points.Count == 2)
                        {

                        }
                        else if (points.Count > 2)
                            points = new List<Point>();
                        break;
                }
        }

        private void HandleRMBClick(object sender, MouseButtonEventArgs e)
        {
            //TODO show popup menu
        }

        private void AddShape(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape)
            {
                Shape activeRectangle = (Shape)e.OriginalSource;
                MainCanvas.Children.Remove(activeRectangle);
            }
            else
            {
                
                // new Polyline();
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
                          };*/
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
                        break;*/
                }
            /*    Canvas.SetLeft(newShape, Mouse.GetPosition(MainCanvas).X - newShape.Width/2);
                Canvas.SetTop(newShape, Mouse.GetPosition(MainCanvas).Y - newShape.Height/2);

                MainCanvas.Children.Add(newShape);*/
            }
        }

        private void ShowPopupMenu(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
