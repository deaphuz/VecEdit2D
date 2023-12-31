﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VecEdit2D
{
    public class CanvasImage
    {
        Canvas c;
        List<IVectorFigure> shapes;
        public CanvasImage() {
            shapes = new List<IVectorFigure>();
        }
 
        void addShape(IVectorFigure shape)
        {
            shapes.Add(shape);
        }

        void Draw()
        {
            foreach (IVectorFigure shape in shapes)
            {
                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);

                c.Children.Add(ellipse);
            }
        }
       
    }
}
