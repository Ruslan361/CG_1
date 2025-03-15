using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Erosion: Morphological
    {
        public Erosion(bool[,] mask)
        {
            this.mask = mask;
        }
        protected override Color applyMask(Bitmap sourceImage, int x, int y)
        {
            int MH = mask.GetLength(0);
            int MW = mask.GetLength(1);
            Color initColor = Color.FromArgb(255, 255, 255);
            byte minR = initColor.R;
            byte minG = initColor.G;
            byte minB = initColor.B;

            for (int j = -MH / 2; j <= MH / 2; j++)
            {
                for (int i = -MW / 2; i <= MW / 2; i++)
                {
                    if (mask[j + MH / 2, i + MW / 2])
                    {
                        int idx = Clamp(x + i, 0, sourceImage.Width - 1);
                        int idy = Clamp(y + j, 0, sourceImage.Height - 1);
                        Color current = sourceImage.GetPixel(idx, idy);
                        if (current.R < minR) minR = current.R;
                        if (current.G < minG) minG = current.G;
                        if (current.B < minB) minB = current.B;
                    }
                }
            }
            return Color.FromArgb(minR, minG, minB);
        }
    }
}

