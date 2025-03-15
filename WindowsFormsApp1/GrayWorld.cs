using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class GrayWorld: Action
    {
        protected Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, float[] mean, float avg)
        {
            Color source = sourceImage.GetPixel(x, y);
            int[] sourceChannels = { source.R, source.G, source.B };
            int[] result = new int[3];
            for (int i = 0; i < sourceChannels.Length; i++)
            {
                result[i] = Clamp((int)((float)(sourceChannels[i]) * (avg) / (mean[i])), 0, 255);
            }
            return Color.FromArgb(result[0], result[1], result[2]);
        }
        private static float[] calculateMean(Bitmap bitmap)
        {
            float[] mean = new float[] { 0, 0, 0 };
            float amountOfpixels = bitmap.Width * bitmap.Height;
            float coeff = 1 / amountOfpixels;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color current = bitmap.GetPixel(x, y);
                    int[] colors = new int[] { current.R, current.G, current.B };
                    for (int i = 0; i < colors.Length; i++)
                    {
                        mean[i] += coeff * colors[i];
                    }
                }
            }
            return mean;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            float[] mean = calculateMean(sourceImage);
            float avg = (mean[0] + mean[1] + mean[2]) / 3;
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j, mean, avg));
                }
            }
            return resultImage;
        }
    }
}
