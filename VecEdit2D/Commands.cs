using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecEdit2D
{
    public interface Command
    {
        void execute();
    }

    public class TranslateCommand : Command
    {
        public TranslateCommand(Group group)
        {

        }

        public void execute()
        {

        }
    }

    public class RotateCommand : Command
    {
        public void execute()
        {

        }
    }

    public class ScaleCommand : Command
    {
        public void execute()
        {

        }
    }
}
