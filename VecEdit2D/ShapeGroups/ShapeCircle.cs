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
    public class ShapeCircle : ShapeGroup
    {
        public double r { get; set; }

        [JsonConstructor]

        public ShapeCircle(double centerx, double centery, double r, Color primary, Color secondary)
        {
            childGroups = new List<ShapeGroup>();
            center.X = centerx;
            center.Y = centery;
            color = primary;
            strokeColor = secondary;
            this.r = r;
            name = "Shape " + ++Globals.ShapeID;
        }

        /*
        public Ellipse getWPFFigure()
        {
            return new Ellipse
            {
                Stroke = new SolidColorBrush(strokeColor),
                Fill = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 2 * r,
                Height = 2 * r,
            };
        }
        */

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
            Canvas.SetLeft(el, center.X - getWPFRadius());
            Canvas.SetTop(el, center.Y - getWPFRadius());
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
