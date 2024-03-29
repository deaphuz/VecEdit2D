﻿using System;
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
    public class ShapeText : ShapeGroup
    {
        public string text;
        public double fontSize;

        [JsonConstructor]
        public ShapeText(
            List<ShapeGroup> childGroups,
            string name,
            Point center,
            Color color,
            List<Color> gradientColors,
            Color strokeColor,
            int strokeThickness,
            shapeStyle style,
            string text,
            double fontSize
        ) : base(childGroups, name, center, color, gradientColors, strokeColor, strokeThickness, style)
        {
            this.text = text;
            this.fontSize = fontSize;
        }

        public ShapeText() : base()
        {

        }

        public ShapeText(Point p1, Color primary, Color secondary, string text, double fontSize)
        {
            childGroups = null;
            center = new Point(p1.X, p1.Y);
            color = primary;
            strokeColor = secondary;
            this.text = text;
            this.fontSize = fontSize;
            name = "Shape " + ++Globals.ShapeID;
        }

        public ShapeText(ShapeText original) : base(original)
        {
            text = original.text;
            fontSize = original.fontSize;
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
            fontSize *= (sx + sy) / 2;
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


        public override bool remove(string name)
        {
            return false;
        }
        public override void draw(Canvas canvas)
        {
            TextBlock element = new TextBlock
            {
                FontSize = fontSize,
                Text = text,
                Foreground = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
            };
            canvas.Children.Add(element);
          //  Canvas.SetLeft(element, center.X - r);
           // Canvas.SetTop(element, center.Y - r);
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
        { return new ShapeText(this); }
    }
}
