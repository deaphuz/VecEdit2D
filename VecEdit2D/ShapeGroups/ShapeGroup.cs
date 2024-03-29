﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using VecEdit2D.Utils;

namespace VecEdit2D
{
    public enum shapeStyle
    {
        NORMAL,
        OUTLINE,
        EMPTY
    }

    //Composite, Prototype
    public interface Group
    {
        void translate(double dx, double dy);
        void rotate(double angleRad, Point rotCenter);
        void scale(double sx, double sy, Point scaleCenter);
        void setColor(Color color);
        void setBorder(Color border);
        void setStyle(shapeStyle style);
        ShapeGroup find(string name);

        //for prototype
        ShapeGroup clone();
    }

    [Serializable]
    public class ShapeGroup : Group
    {
        // [JsonProperty]
        public List<ShapeGroup> childGroups;
        public string name;

        //for scaling and rotating
        //group, has x and y
        public Point center;
        public List<Color> gradientColors { get; set; }
        public SerializableColor color { get; set; }
        public SerializableColor strokeColor { get; set; }
        public int strokeThickness { get; set; }

        public shapeStyle style;

        [JsonConstructor]
        public ShapeGroup(
            List<ShapeGroup> childGroups,
            string name,
            Point center,
            SerializableColor color,
            List<Color> gradientColors,
            SerializableColor strokeColor,
            int strokeThickness,
            shapeStyle style
        )
        {
            this.childGroups = childGroups;
            this.name = name;
            this.center = center;
            this.color = color;
            this.gradientColors = gradientColors;
            this.strokeColor = strokeColor;
            this.strokeThickness = strokeThickness;
            this.style = style;
        }


        public ShapeGroup(double centerx, double centery, Color primary, Color secondary)
        {
            childGroups = new List<ShapeGroup>();
            center.X = centerx;
            center.Y = centery;
            color = primary;
            strokeColor = secondary;
            name = "Group " + ++Globals.ShapeID;
        }

        public ShapeGroup() { }

        public ShapeGroup(ShapeGroup original)
        {
            childGroups = original.childGroups?.Select(group => group.clone()).ToList();
            name = "Group " + ++Globals.ShapeID;
            center = new Point(original.center.X, original.center.Y);
            color = original.color;
            gradientColors = original.gradientColors?.ToList();
            strokeColor = original.strokeColor;
            strokeThickness = original.strokeThickness;
            style = original.style;
        }

        public virtual void translate(double dx, double dy)
        {
            if (childGroups != null)
                foreach (Group group in childGroups)
                    group.translate(dx, dy);
        }
        public virtual void rotate(double angleRad, Point rotCenter)
        {
            if (childGroups != null)
                foreach (Group group in childGroups)
                    group.rotate(angleRad, rotCenter);
        }
        public virtual void scale(double sx, double sy, Point scaleCenter)
        {
            if (childGroups != null)
                foreach (Group group in childGroups)
                    group.scale(sx, sy, scaleCenter);
        }
        public virtual void setColor(Color color)
        {
            if (childGroups != null)
                foreach (Group group in childGroups)
                    group.setColor(color);
        }
        public virtual void setBorder(Color border)
        {
            if (childGroups != null)
                foreach (Group group in childGroups)
                    group.setBorder(border);
        }
        public virtual void setStyle(shapeStyle style)
        {
            if (childGroups != null)
                foreach (Group group in childGroups)
                    group.setStyle(style);
        }

        public virtual void draw(Canvas canvas)
        {
            if (childGroups != null)
                foreach (ShapeGroup group in childGroups)
                    group.draw(canvas);
        }

        public virtual ShapeGroup find(string name)
        {
            if (this.name == name)
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

        public virtual bool remove(string name)
        {
            foreach (ShapeGroup group in childGroups)
            {
                if (group.name == name)
                {
                    childGroups.Remove(group);
                    return true;
                }
                group.remove(name);
            }
            return false;
        }

        public virtual void showSelection()
        {
            if (childGroups != null)
                foreach (ShapeGroup group in childGroups)
                    group.showSelection();
        }

        public virtual ShapeGroup clone()
        { return new ShapeGroup(this); }
    }
}