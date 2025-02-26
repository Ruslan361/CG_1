using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class SobelFilter : MatrixFilter
    {
        public static int vertical {
            get { return 1; }
        }
        public static int horizontal
        {
            get { return 2; }
        }
        public SobelFilter(int direction)
        {
            if (direction == SobelFilter.horizontal)
            {
                this.kernel = new float[,] { { -1.0f, -2.0f, -1.0f},
                                             { 0.0f, 0.0f, 0.0f},
                                             { 1.0f, 2.0f, 1.0f} };
            }
            if (direction == SobelFilter.vertical)
            {
                this.kernel = new float[,] { { -1.0f, 0.0f, 1.0f},
                                             { -2.0f, 0.0f, 2.0f},
                                             { -1.0f, 0.0f, 1.0f }};
            }
        }
    }
}
