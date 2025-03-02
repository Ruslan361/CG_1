using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class WaweFilter : Filters
    {
        //private float k;
        //public WaweFilter(float k)
        //{
        //    this.k = k;
        //}
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            //Color sourceColor = sourceImage.GetPixel(x, y);
            //int intensity = (int)(0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B);
            int x_r = Clamp((int)(x + 20 * Math.Sin(2 * Math.PI * y / 60)), 0, sourceImage.Width-1);
            int y_r = y;
            //Color resultColor = sourceImage.GetPixel()
            return sourceImage.GetPixel(x_r, y_r);
        }
    }
}
