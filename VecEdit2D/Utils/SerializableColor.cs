using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecEdit2D.Utils
{
    [Serializable]
    public class SerializableColor
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public static implicit operator System.Windows.Media.Color(SerializableColor sColor)
        {
            return System.Windows.Media.Color.FromArgb(sColor.A, sColor.R, sColor.G, sColor.B);
        }

        public static implicit operator SerializableColor(System.Windows.Media.Color color)
        {
            return new SerializableColor()
            {
                A = color.A,
                R = color.R,
                G = color.G,
                B = color.B
            };
        }
    }
}
