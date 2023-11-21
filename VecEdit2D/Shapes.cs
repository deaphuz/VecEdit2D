using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace VecEdit2D
{

    // Flyweight interface
    public interface IVectorFigure
    {

      //  void Draw(Canvas canvas, double x, double y, double dx, double dy);
    }

    abstract class Fig2D : IVectorFigure
    {
        double x;
        double y;
        double dx;
        double dy;

        protected Fig2D()
        {

        }

      /*  void Draw(Canvas canvas, double x, double y, double dx, double dy)
        {
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);

            canvas.Children.Add(ellipse);
        }*/
    }

    class Ellipse : Fig2D
    {

    }
}
