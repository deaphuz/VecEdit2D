using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace VecEdit2D
{
    //Composite
    public interface Group
    {
        void translate(double dx, double dy);
        void rotate(double angleRad, Point rotCenter);
        void scale(double sx, double sy, Point scaleCenter);
        void setColor(Color color);
        void setBorder(Border border);
        void setStyle(Style style);

        //void draw();
    }

    [Serializable]
    public class ShapeGroup : Group
    {
        public List<Group> childGroups;

        //for scaling and rotating
        //group, has x and y
        public Point center;
        public Color Color { get; set; }
        public List<Color> GradientColors { get; set; }

        public Color StrokeColor { get; set; }
        public int StrokeThickness { get; set; }


        [JsonConstructor]
        public ShapeGroup(double centerx, double centery, Color primary, Color secondary)
        {
            childGroups = new List<Group>();
            center.X = centerx;
            center.Y = centery;
            Color = primary;
            StrokeColor = secondary;
        }

        //TODO NEW CONSTRUCTORS
        /*
        public ShapeGroup(double centerx, double centery)
        {
            childGroups = new List<Group>();
            center.X = centerx;
            center.Y = centery;
        }*/

        public ShapeGroup() { }
        public virtual void translate(double dx, double dy)
        {
            foreach (Group group in childGroups)
            {
                group.translate(dx, dy);
            }
        }
        public virtual void rotate(double angleRad, Point rotCenter)
        {
            foreach (Group group in childGroups)
            {
                group.rotate(angleRad, rotCenter);
            }
        }
        public virtual void scale(double sx, double sy, Point scaleCenter)
        {
            foreach (Group group in childGroups)
            {
                group.scale(sx, sy, scaleCenter);
            }
        }
        public virtual void setColor(Color color)
        {
            foreach (Group group in childGroups)
            {
                group.setColor(color);
            }
        }
        public virtual void setBorder(Border border)
        {
            foreach (Group group in childGroups)
            {
                group.setBorder(border);
            }
        }
        public virtual void setStyle(Style style)
        {
            foreach (Group group in childGroups)
            {
                group.setStyle(style);
            }
        }
    }
}
