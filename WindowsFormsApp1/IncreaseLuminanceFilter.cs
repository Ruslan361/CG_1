using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class IncreaseLuminanceFilter : Filters
    {
        private int step;
        public IncreaseLuminanceFilter(int step)
        {
            this.step = step;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int R = Clamp(sourceColor.R + this.step, 0, 255);
            int G = Clamp(sourceColor.G + this.step, 0, 255);
            int B = Clamp(sourceColor.B + this.step, 0, 255);
            Color resultColor = Color.FromArgb(R, G, B);
            return resultColor;
        }
    }
}
