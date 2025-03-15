using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Dilatation : Morphological
    {
        public Dilatation(bool[,] mask)
        {
            this.mask = mask;
        }
        protected override Color applyMask(Bitmap sourceImage, int x, int y)
        {
            int MH = mask.GetLength(1);
            int MW = mask.GetLength(0);
            Color initColor = Color.FromArgb(0, 0, 0);
            byte maxR = initColor.R;
            byte maxG = initColor.G;
            byte maxB = initColor.B;

            for (int j = -MH / 2; j <= MH / 2; j++)
            {
                for (int i = -MW / 2; i <= MW / 2; i++)
                {
                    if (mask[MH / 2 + i, MW / 2 + j])
                    {
                        int idx = Clamp(x + i, 0, sourceImage.Width - 1);
                        int idy = Clamp(y + j, 0, sourceImage.Height - 1);
                        Color current = sourceImage.GetPixel(idx, idy);
                        if (current.R > maxR) maxR = current.R;
                        if (current.G > maxG) maxG = current.G;
                        if (current.B > maxB) maxB = current.B;
                    }
                }
            }
            return Color.FromArgb(maxR, maxG, maxB);
        }
    }
}
