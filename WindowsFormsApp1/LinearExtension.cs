using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class LinearExtension: Action
    {
        protected Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int [] min, int[] max )
        {
            Color source = sourceImage.GetPixel(x, y);
            int[] sourceChannels = { source.R, source.G, source.B };
            int[] result = new int[3];
            for (int i = 0; i < sourceChannels.Length; i++)
            {
                result[i] = Clamp((int)((float)(sourceChannels[i] - min[i]) * (255) / (max[i] - min[i])), 0, 255);
            }
            return Color.FromArgb(result[0], result[1], result[2]);
        }
        private static (int [], int[]) calculateMinAndMax(Bitmap bitmap)
        {
            int [] min = new int[] { 255, 255, 255 };
            int [] max = new int[] { 0, 0, 0 };
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color current = bitmap.GetPixel(x, y);
                    int[] colors = new int[] { current.R, current.G, current.B };
                    for (int i = 0; i < colors.Length; i++)
                    {
                        min[i] = colors[i] < min[i] ? colors[i] : min[i];
                        max[i] = colors[i] > max[i] ? colors[i] : max[i];
                    }
                }
            }
            return (min, max);
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            int[] min;
            int[] max;
            (min, max) = calculateMinAndMax(sourceImage);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j, min, max));
                }
            }
            return resultImage;
        }
    }

}
