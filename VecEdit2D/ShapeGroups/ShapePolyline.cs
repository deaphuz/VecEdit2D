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
    public class ShapePolyline : ShapeGroup
    {
        public List<Point> contour;

        [JsonConstructor]
        public ShapePolyline(List<Point> points, Color primary, Color secondary)
        {
            contour = new List<Point>();
            double centerX = 0;
            double centerY = 0;
            foreach (Point p in points)
            {
                contour.Add(p);
                centerX += p.X;
                centerY += p.Y;
            }
            centerX /= points.Count;
            centerY /= points.Count;
            center = new Point(centerX, centerY);
            color = primary;
            strokeColor = secondary;
            name = "Ksztalt " + ++Globals.ShapeID;

        }

        /*
        public Polyline getWPFFigure()
        {
            return new Polyline
            {
                Stroke = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Points = new PointCollection(contour),
            };
        }
        */

        public override void translate(double dx, double dy)
        {
            center = PointHelper.Translate(center, dx, dy);
            for (int i = 0; i < contour.Count; i++)
            {
                contour[i] = PointHelper.Translate(contour[i], dx, dy);
            }
        }
        public override void rotate(double angleRad, Point rotCenter)
        {
            center = PointHelper.Rotate(center, rotCenter, angleRad);
            for (int i = 0; i < contour.Count; i++)
            {
                contour[i] = PointHelper.Rotate(contour[i], rotCenter, angleRad);
            }
        }
        public override void scale(double sx, double sy, Point scaleCenter)
        {
            center = PointHelper.Scale(center, scaleCenter, sx, sy);
            for (int i = 0; i < contour.Count; i++)
            {
                contour[i] = PointHelper.Scale(contour[i], scaleCenter, sx, sy);
            }
        }

        public override void draw(Canvas canvas)
        {
            canvas.Children.Add(new Polyline
            {
                Stroke = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Points = new PointCollection(contour),
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
