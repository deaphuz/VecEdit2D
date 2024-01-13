using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VecEdit2D
{

    // Flyweight interface
    public interface IVectorFigure
    {

        //  void Draw(Canvas canvas, double x, double y, double dx, double dy);
    }

    public abstract class Shape
    {
        public Brushes Color { get; set; }
        public Brushes StrokeColor { get; set; }
        public int StrokeThickness { get; set; }

        public Shape(Shape source)
        {
            this.Color = source.Color;
            this.StrokeThickness = source.StrokeThickness;
            this.StrokeColor = source.StrokeColor;
        }

        public abstract Shape Clone();
    }

    public class Rectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(Rectangle source) : base(source)
        {
            this.Width = source.Width;
            this.Height = source.Height;
        }

        public override Shape Clone()
        {
            return new Rectangle(this);
        }
    }

    public class Circle : Shape
    {
        public int Radius { get; set; }

        public Circle(Circle source) : base(source)
        {
            this.Radius = source.Radius;
        }

        public override Shape Clone()
        {
            return new Circle(this);
        }
    }
}
