using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class GlassFilter: Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Random random = new Random();
            int x_r = Clamp((int)(x + (random.NextDouble() - 0.5f) * 10), 0, sourceImage.Width - 1);
            int y_r = Clamp((int)(y + (random.NextDouble() - 0.5f) * 10), 0, sourceImage.Height - 1);
            return sourceImage.GetPixel(x_r, y_r);
        }
    }
}
