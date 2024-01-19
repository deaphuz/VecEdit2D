using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;

namespace VecEdit2D
{   
    public enum shapeStyle
    {
        NORMAL,
        OUTLINE,
        EMPTY
    }

    //Composite
    public interface Group
    {
        void translate(double dx, double dy);
        void rotate(double angleRad, Point rotCenter);
        void scale(double sx, double sy, Point scaleCenter);
        void setColor(Color color);
        void setBorder(Color border);
        void setStyle(shapeStyle style);
        ShapeGroup find(string name);
    }

    [Serializable]
    public class ShapeGroup : Group
    {
        public List<ShapeGroup> childGroups;
        public string name;

        //for scaling and rotating
        //group, has x and y
        public Point center;
        public Color color { get; set; }
        public List<Color> gradientColors { get; set; }

        public Color strokeColor { get; set; }
        public int strokeThickness { get; set; }


        [JsonConstructor]
        public ShapeGroup(double centerx, double centery, Color primary, Color secondary)
        {
            childGroups = new List<ShapeGroup>();
            center.X = centerx;
            center.Y = centery;
            color = primary;
            strokeColor = secondary;
            name = "Ksztalt " + ++Globals.ShapeID;
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
        public virtual void setBorder(Color border)
        {
            foreach (Group group in childGroups)
            {
                group.setBorder(border);
            }
        }
        public virtual void setStyle(shapeStyle style)
        {
            foreach (Group group in childGroups)
            {
                group.setStyle(style);
            }
        }

        public virtual ShapeGroup find(string name)
        {
            if(this.name == name)
            {
                return this;
            }
            foreach (Group group in childGroups)
            {
                ShapeGroup tmp = group.find(name);
                if (tmp != null)
                {
                    return tmp;
                }
            }
            return null;
        }
    }
}
