using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VecEdit2D
{
    //Composite
    public interface Group
    {
        void translate(double dx, double dy);
        void rotate(double angle);
        void scale(double sx, double sy);
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
        public Brushes Color { get; set; }
        public Brushes StrokeColor { get; set; }
        public int StrokeThickness { get; set; }


        [JsonConstructor]
        public ShapeGroup(ShapeGroup source)
        {
            childGroups = new List<Group>();
            this.Color = source.Color;
            this.StrokeThickness = source.StrokeThickness;
            this.StrokeColor = source.StrokeColor;
        }

        public ShapeGroup(double mousex, double mousey)
        {
            childGroups = new List<Group>();
            center.X = mousex;
            center.Y = mousey;
        }
        public void translate(double dx, double dy)
        {
            foreach (Group group in childGroups)
            {
                group.translate(dx, dy);
            }
        }
        public void rotate(double angle)
        {
            foreach (Group group in childGroups)
            {
                group.rotate(angle);
            }
        }
        public void scale(double sx, double sy)
        {
            foreach (Group group in childGroups)
            {
                group.scale(sx, sy);
            }
        }
        public void setColor(Color color)
        {
            foreach (Group group in childGroups)
            {
                group.setColor(color);
            }
        }
        public void setBorder(Border border)
        {
            foreach (Group group in childGroups)
            {
                group.setBorder(border);
            }
        }
        public void setStyle(Style style)
        {
            foreach (Group group in childGroups)
            {
                group.setStyle(style);
            }
        }


    }
    /*  public void draw()
      {
          foreach (Group group in childGroups)
          {
              group.setStyle(style);
          }
      }*/

    /*  public abstract class ReadyShape : ShapeGroup
      {

          /*
          public ReadyShape(Shape source)
          {
              this.Color = source.Color;
              this.StrokeThickness = source.StrokeThickness;
              this.StrokeColor = source.StrokeColor;
  */

    [Serializable]
    public class ShapeRectangle : VecEdit2D.ShapeGroup
    {
        public Point leftTop { get; set; }
        public Point rightTop { get; set; }
        public Point leftBottom { get; set; }
        public Point rightBottom { get; set; }

        public ShapeRectangle(double mousex, double mousey): base(mousex, mousey)
        {

        }

     /*   public ShapeRectangle(double x1, double y1, double x2, double y2)
        {

        }*/

        /*  public ShapeRectangle(Rectangle source) : base()
          {
              this.Width = source.Width;
              this.Height = source.Height;
          }

          public Shape Draw()
          {
              return new Rectangle
              {
                  Width = this.Width,
                  Height = this.Height;

              }
          }*/
    }

    [Serializable]
    public class ShapeCircle : VecEdit2D.ShapeGroup
    {
        public double r { get; set; }


        public ShapeCircle(double mousex, double mousey, double r) : base(mousex, mousey)
        {
            this.r = r;
        }

        public Ellipse getWPFFigure()
        {
            return new Ellipse
            {
                Stroke = System.Windows.Media.Brushes.Black,
                Fill = System.Windows.Media.Brushes.DarkBlue,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = r,
                Height = r,
            };
        }

        public double getWPFRadius()
        {
            return this.r;
        }

    }
}

