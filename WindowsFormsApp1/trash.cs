using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class trash
    {
    }
}


namespace Filter
{
    public class Filter
    {
        protected static float[,] kernel = null;
        //protected Filter() { }
        //public Filter(float[,] kernel)
        //{
        //    this.kernel = kernel;
        //}
        protected static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
        //public static Bitmap Execute(Bitmap sourceImage)
        //{
        //    Filter.kernel
        //}

        protected static Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            float resultR = 0;
            float resultG = 0;
            float resultB = 0;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idx = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idy = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighbourColor = sourceImage.GetPixel(idx, idy);
                    resultR += neighbourColor.R * kernel[radiusX + k, radiusY + l];
                    resultG += neighbourColor.G * kernel[radiusX + k, radiusY + l];
                    resultB += neighbourColor.B * kernel[radiusX + k, radiusY + l];
                }
            }
            return Color.FromArgb(Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255));
        }
    }
}