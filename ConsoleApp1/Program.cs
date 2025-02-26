using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filter
{
    public class Filter
    {
        private static int Clamp(int input)
        {
            if (input > 255)
            {
                return 255;
            }
            return input;
        }
        public static Bitmap Execute(Bitmap sourceImage)
        {
            Bitmap invertedImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int K = 20;
            for (int x = 0; x < sourceImage.Width; x++)
            {
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    Color originalColor = sourceImage.GetPixel(x, y);
                    int grayColor = (int)(0.299 * originalColor.R + 0.587 * originalColor.G + 0.114 * originalColor.B);
                    if (grayColor > 255)
                    {
                        grayColor = 255;
                    }
                    Color invertedColor = Color.FromArgb(Clamp(grayColor + 2 * K),
                    Clamp((int)(grayColor + 0.5 * K)), Clamp(grayColor - K));
                    invertedImage.SetPixel(x, y, invertedColor);
                }
            }

            return invertedImage;
        }
    }
}


