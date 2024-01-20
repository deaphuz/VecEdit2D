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

namespace VecEdit2D
{
    [Serializable]
    public class ShapeLine : ShapeGroup
    {
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }

        [JsonConstructor]
        public ShapeLine(Point p1, Point p2, Color primary, Color secondary)
        {
            startPoint = new Point(p1.X, p1.Y);
            endPoint = new Point(p2.X, p2.Y);
            double centerX = (p1.X + p2.X) / 2;
            double centerY = (p1.Y + p2.Y) / 2;

            center = new Point(centerX, centerY);
            color = primary;
            strokeColor = secondary;

            name = "Ksztalt " + ++Globals.ShapeID;

        }

        public Line getWPFFigure()
        {
            return new Line
            {
                Stroke = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                X1 = startPoint.X,
                X2 = endPoint.X,
                Y1 = startPoint.Y,
                Y2 = endPoint.Y,
            };
        }

        public override void translate(double dx, double dy)
        {
            center = PointHelper.Translate(center, dx, dy);
            startPoint = PointHelper.Translate(startPoint, dx, dy);
            endPoint = PointHelper.Translate(endPoint, dx, dy);
        }
        public override void rotate(double angleRad, Point rotCenter)
        {

            center = PointHelper.Rotate(center, rotCenter, angleRad);
            startPoint = PointHelper.Rotate(startPoint, rotCenter, angleRad);
            endPoint = PointHelper.Rotate(endPoint, rotCenter, angleRad);
        }
        public override void scale(double sx, double sy, Point scaleCenter)
        {
            center = PointHelper.Scale(center, scaleCenter, sx, sy);
            startPoint = PointHelper.Scale(startPoint, scaleCenter, sx, sy);
            endPoint = PointHelper.Scale(endPoint, scaleCenter, sx, sy);
        }

        public override void draw(Canvas canvas)
        {
            canvas.Children.Add(new Line
            {
                Stroke = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                X1 = startPoint.X,
                X2 = endPoint.X,
                Y1 = startPoint.Y,
                Y2 = endPoint.Y,
            });
        }

        public override ShapeGroup find(string name)
        {
            if (this.name == name)
            {
                return this;
            }
            return null;
        }
    }
}
