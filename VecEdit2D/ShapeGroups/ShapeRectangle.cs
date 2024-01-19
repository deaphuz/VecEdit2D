using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Text.Json.Serialization;

namespace VecEdit2D
{
    [Serializable]
    public class ShapeRectangle : ShapeGroup
    {
        public Point p1 { get; set; }
        public Point p2 { get; set; }
        public Point p3 { get; set; }
        public Point p4 { get; set; }

        [JsonConstructor]
        public ShapeRectangle(Point p1, Point p2, Color primary, Color secondary)
        {
            this.p1 = new Point(p1.X, p1.Y);
            this.p2 = new Point(p2.X, p1.Y);
            this.p3 = new Point(p2.X, p2.Y);
            this.p4 = new Point(p1.X, p2.Y);


            double centerX = (p1.X + p2.X + p3.X + p4.X) / 4;
            double centerY = (p1.Y + p2.Y + p3.Y + p4.Y) / 4;

            center = new Point(centerX, centerY);
            color = primary;
            strokeColor = secondary;


        }

        public Polygon getWPFFigure()
        {
            return new Polygon
            {
                Stroke = new SolidColorBrush(strokeColor),
                Fill = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Points = new PointCollection { p1, p2, p3, p4 },
            };
        }

        public override void translate(double dx, double dy)
        {
            center = PointHelper.Translate(center, dx, dy);
            p1 = PointHelper.Translate(p1, dx, dy);
            p2 = PointHelper.Translate(p2, dx, dy);
            p3 = PointHelper.Translate(p3, dx, dy);
            p4 = PointHelper.Translate(p4, dx, dy);
        }
        public override void rotate(double angleRad, Point rotCenter)
        {
            center = PointHelper.Rotate(center, rotCenter, angleRad);
            p1 = PointHelper.Rotate(p1, rotCenter, angleRad);
            p2 = PointHelper.Rotate(p2, rotCenter, angleRad);
            p3 = PointHelper.Rotate(p3, rotCenter, angleRad);
            p4 = PointHelper.Rotate(p4, rotCenter, angleRad);
        }
        public override void scale(double sx, double sy, Point scaleCenter)
        {
            center = PointHelper.Scale(center, scaleCenter, sx, sy);
            p1 = PointHelper.Scale(p1, scaleCenter, sx, sy);
            p2 = PointHelper.Scale(p2, scaleCenter, sx, sy);
            p3 = PointHelper.Scale(p3, scaleCenter, sx, sy);
            p4 = PointHelper.Scale(p4, scaleCenter, sx, sy);
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
