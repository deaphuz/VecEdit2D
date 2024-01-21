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
using System.Windows.Forms.VisualStyles;

namespace VecEdit2D
{
    [Serializable]
    public class ShapeLine : ShapeGroup
    {
        public Point startPoint { get; set; }
        public Point endPoint { get; set; }

        [JsonConstructor]
        public ShapeLine(
            List<ShapeGroup> childGroups,
            string name,
            Point center,
            Color color,
            List<Color> gradientColors,
            Color strokeColor,
            int strokeThickness,
            shapeStyle style,
            Point startPoint,
            Point endPoint
        ) : base(childGroups, name, center, color, gradientColors, strokeColor, strokeThickness, style)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        public ShapeLine(Point p1, Point p2, Color primary, Color secondary)
        {
            startPoint = new Point(p1.X, p1.Y);
            endPoint = new Point(p2.X, p2.Y);
            double centerX = (p1.X + p2.X) / 2;
            double centerY = (p1.Y + p2.Y) / 2;

            center = new Point(centerX, centerY);
            color = primary;
            strokeColor = secondary;

            name = "Shape " + ++Globals.ShapeID;

        }

        [JsonConstructor]
        public ShapeLine() : base()
        {

        }

        public ShapeLine(ShapeLine shapeLine)
        {
            childGroups = null;

            startPoint = new Point(shapeLine.startPoint.X, shapeLine.startPoint.Y);
            endPoint = new Point(shapeLine.endPoint.X, shapeLine.endPoint.Y);
            center = new Point(shapeLine.center.X, shapeLine.center.Y);
            color = shapeLine.color;
            strokeColor = shapeLine.strokeColor;

            name = "Shape " + ++Globals.ShapeID;
        }

        public override void showSelection()
        {
            MainWindow.Instance.showDot(startPoint.X, startPoint.Y);
            MainWindow.Instance.showDot(center.X, center.Y);
            MainWindow.Instance.showDot(endPoint.X, endPoint.Y);
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

        public override bool remove(string name)
        {
            return false;
        }

        public override void draw(Canvas canvas)
        {
            canvas.Children.Add(new Line
            {
                Stroke = new SolidColorBrush(color),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
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

        public override ShapeGroup clone()
        { return new ShapeLine(this); }
    }
}
