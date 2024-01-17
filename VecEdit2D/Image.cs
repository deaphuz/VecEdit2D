using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecEdit2D
{
    [Serializable]
    public class Image
    {

        public List<Group> canvas {  get; set; }
        public Image()
        {
            canvas = new List<Group>();
        }

      //  public void Add()
    }
}
