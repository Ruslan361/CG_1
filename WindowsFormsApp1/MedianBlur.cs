using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{

    class MedianBlur : Filters
    {
        private int radius;
        public MedianBlur(int radius)
        {
            this.radius = radius;
        }
        private int median(List<int> ints)
        {
            int median;
            ints.Sort();
            int len = ints.Count;
            if (len % 2 == 1)
                median = ints[len / 2];
            else
                median = Clamp((int)(0.5 * (ints[len / 2 - 1] + ints[len / 2])), 0, 255);
            return median;
        }
        protected override Color calculateNewPixelColor(Bitmap bitmap, int x, int y)
        {
            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();
            for (int i = -this.radius; i <= this.radius; i++)
            {
                for (int j = -this.radius; j <= this.radius; j++)
                {
                    //if (x + i > 0 && x + i < bitmap.Width)
                    //{
                    //    if (y + j > 0 && y + j < bitmap.Height)
                    //    {
                    int idx = Clamp(x + i, 0, bitmap.Width - 1);
                    int idy = Clamp(y + j, 0, bitmap.Height - 1);
                    Color color = bitmap.GetPixel(idx, idy);
                    R.Add(color.R);
                    G.Add(color.G);
                    B.Add(color.B);
                    //    }
                    //}
                }
            }
            return Color.FromArgb(median(R), median(G), median(B));

        }
    }
}
