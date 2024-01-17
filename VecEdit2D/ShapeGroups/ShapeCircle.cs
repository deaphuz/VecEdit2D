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
    public class ShapeCircle : ShapeGroup
    {
        public double r { get; set; }

        [JsonConstructor]
        public ShapeCircle(double mousex, double mousey, double r, Color primary, Color secondary) : base(mousex, mousey, primary, secondary)
        {
            this.r = r;
        }

        public Ellipse getWPFFigure()
        {
            return new Ellipse
            {
                Stroke = new SolidColorBrush(StrokeColor),
                Fill = new SolidColorBrush(Color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 2 * r,
                Height = 2 * r,
            };
        }

        public double getWPFRadius()
        {
            return this.r;
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
    }
}
