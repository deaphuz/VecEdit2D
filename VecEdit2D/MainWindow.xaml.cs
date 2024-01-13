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
        Brush customColor;
        Random r = new Random();
        

        public MainWindow()
        {
            toolboxInstance = Toolbox.Instance;
            toolboxInstance.Show();
        }

        private void AddShape(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Shapes.Shape)
            {
                System.Windows.Shapes.Shape activeRectangle = (System.Windows.Shapes.Shape)e.OriginalSource;
                MainCanvas.Children.Remove(activeRectangle);
            }
            else
            {
                customColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 255))); 
                System.Windows.Shapes.Shape newShape = new Polyline();
                switch (toolboxInstance.Shape)
                {
                    case "circle":
                        newShape = new System.Windows.Shapes.Ellipse()
                        {
                            Height = toolboxInstance.Radius,
                            Width = toolboxInstance.Radius,
                            Fill = customColor,
                            StrokeThickness = toolboxInstance.StrokeThickness,
                            Stroke = Brushes.Black
                        };
                        break;
                    case "triangle":
                        newShape = new System.Windows.Shapes.Path()
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
                        newShape = new System.Windows.Shapes.Line()
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
        }
    }
}
