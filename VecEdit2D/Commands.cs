using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace VecEdit2D
{
    public interface Command
    {
        void execute();
    }

    public class TranslateCommand : Command
    {
        private ShapeGroup group;
        private double trX, trY;

        public TranslateCommand(ShapeGroup group, double trX, double trY)
        {
            this.group = group;
            this.trX = trX;
            this.trY = trY;
        }

        public void execute()
        {
            group.translate(trX, trY);
        }
    }

    public class RotateCommand : Command
    {
        private ShapeGroup group;
        private double angle;

        public RotateCommand(ShapeGroup group, double angle)
        {
            this.group = group;
            this.angle = angle;
        }

        public void execute()
        {
            group.rotate(angle * (Math.PI / 180.0), group.center);

        }
    }

    public class ScaleCommand : Command
    {
        private ShapeGroup group;
        private double scaleFactor;

        public ScaleCommand(ShapeGroup group, double scaleFactor)
        {
            this.group = group;
            this.scaleFactor = scaleFactor;
        }

        public void execute()
        {
            group.scale(scaleFactor, scaleFactor, group.center);
        }
    }
}
