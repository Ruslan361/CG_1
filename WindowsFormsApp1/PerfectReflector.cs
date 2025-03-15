using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class PerfectReflector : LinearExtension
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[] min, int[] max)
        {
            Color source = sourceImage.GetPixel(x, y);
            int[] sourceChannels = { source.R, source.G, source.B };
            int[] result = new int[3];
            for (int i = 0; i < sourceChannels.Length; i++)
            {
                result[i] = max[i] > 0 ? Clamp((int)((float)sourceChannels[i] * 255.0f / (float)max[i]), 0, 255) : 0;

            }
            return Color.FromArgb(result[0], result[1], result[2]);
        }
    }
}
