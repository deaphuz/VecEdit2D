using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using System.Windows.Documents;

namespace VecEdit2D
{
    [Serializable]
    public class ShapeCircle : ShapeGroup
    {
        public double r { get; set; }

        [JsonConstructor]
        public ShapeCircle(
            List<ShapeGroup> childGroups,
            string name,
            Point center,
            Color color,
            List<Color> gradientColors,
            Color strokeColor,
            int strokeThickness,
            shapeStyle style,
            double r
        ) : base(childGroups, name, center, color, gradientColors, strokeColor, strokeThickness, style)
        {
            this.r = r;
        }

        [JsonConstructor]
        public ShapeCircle() : base()
        {

        }

        public ShapeCircle(Point p1, Point p2, Color primary, Color secondary)
        {
            childGroups = null;
            center.X = p1.X;
            center.Y = p1.Y;
            color = primary;
            strokeColor = secondary;

            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;

            r = Math.Sqrt(dx*dx + dy * dy);
            name = "Shape " + ++Globals.ShapeID;
        }

        public ShapeCircle(double centerx, double centery, double r, Color primary, Color secondary)
        {
            childGroups = null;
            center.X = centerx;
            center.Y = centery;
            color = primary;
            strokeColor = secondary;
            this.r = r;
            name = "Shape " + ++Globals.ShapeID;
        }


        public ShapeCircle(ShapeCircle shapeCircle)
        {
            //TODO
        }

        public override void translate(double dx, double dy)
        {
            center = PointHelper.Translate(center, dx, dy);
        }
        public override void rotate(double angleRad, Point rotCenter)
        {
            center = PointHelper.Rotate(center, rotCenter, angleRad);
        }
        public override void scale(double sx, double sy, Point scaleCenter)
        {
            center = PointHelper.Scale(center, scaleCenter, sx, sy);
            r *= (sx + sy) / 2;
        }

        public override void setColor(Color color)
        {
            this.color = color;
        }
        public override void setBorder(Color border)
        {
            this.strokeColor = border;
        }
        public override void setStyle(shapeStyle style)
        {
            this.style = style;
        }

        public override void showSelection()
        {
            MainWindow.Instance.showDot(center.X, center.Y);
        }

        public override void draw(Canvas canvas)
        {
            Ellipse el = new Ellipse
            {
                Stroke = new SolidColorBrush(strokeColor),
                Fill = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 2 * r,
                Height = 2 * r,
            };
            canvas.Children.Add(el);
            Canvas.SetLeft(el, center.X - r);
            Canvas.SetTop(el, center.Y - r);
        }

        public override ShapeGroup find(string name)
        {
            if (this.name == name)
            {
                return this;
            }
            return null;
        }

        public override bool remove(string name)
        {
            return false;
        }

        public override ShapeGroup clone()
        { return new ShapeCircle(this); }
    }
}
