using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VecEdit2D
{
    public static class PointHelper
    {
        internal static Point Translate(Point p, double dx, double dy)
        {
            p.X += dx;
            p.Y += dy;
            return p;
        }

        internal static Point Rotate(Point point, Point referencePoint, double rad)
        {
            point.X -= referencePoint.X;
            point.Y -= referencePoint.Y;

            double x = point.X * Math.Cos(rad) - point.Y * Math.Sin(rad);
            double y = point.X * Math.Sin(rad) + point.Y * Math.Cos(rad);

            point.X = x + referencePoint.X;
            point.Y = y + referencePoint.Y;
            return point;
        }

        internal static Point Scale(Point point, Point referencePoint, double sx, double sy)
        {
            point.X -= referencePoint.X;
            point.Y -= referencePoint.Y;

            point.X *= sx;
            point.Y *= sy;

            point.X += referencePoint.X;
            point.Y += referencePoint.Y;
            return point;
        }

    }
}
