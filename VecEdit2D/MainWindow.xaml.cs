using System;
using System.Collections.Generic;
using System.Linq;
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





    // Concrete flyweight class
    class Circle : IVectorFigure
    {
        private Brush brush;

        public Circle(Color color)
        {
            this.brush = new SolidColorBrush(color);
        }

        public void Draw(Canvas canvas, double x, double y, double dx, double dy)
        {
            Ellipse ellipse = new Ellipse
            {
                w = dx,
                h = dy,
                Fill = brush
            };


        }
    }

    // Main window
    public partial class MainWindow : Window
    {
        private List<IVectorFigure> figures;

        public MainWindow()
        {
            InitializeComponent();
            figures = new List<IVectorFigure>();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IVectorFigure circle = new Circle(Colors.Blue); // You can create different figure types
            figures.Add(circle);
            DrawCanvas();
        }

        private void DrawCanvas()
        {
            MainCanvas.Children.Clear();
            foreach (var figure in figures)
            {
                figure.Draw(MainCanvas, 0, 0);
            }
        }
    }
}
