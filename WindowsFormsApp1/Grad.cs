using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Grad: Action
    {
        private Erosion erosion;
        private Dilatation dilatation;
        public Grad(bool[,] mask)
        {
            this.erosion = new Erosion(mask);
            this.dilatation = new Dilatation(mask);
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap dilatationResult = dilatation.processImage(sourceImage, worker);
            Bitmap erosionResult = erosion.processImage(sourceImage, worker);
            return difference(dilatationResult, erosionResult, worker);
        }
        private Bitmap difference(Bitmap left, Bitmap right, BackgroundWorker worker)
        {
            Bitmap result = new Bitmap(left.Width, left.Height);
            for (int x = 0; x < left.Width; x ++)
            {
                worker.ReportProgress((int)((float)x / left.Width * 100));
                for (int y = 0; y < left.Height; y++)
                {
                    Color leftPixel = left.GetPixel(x, y);
                    Color rightPixel = right.GetPixel(x, y);
                    Color difference = Color.FromArgb(leftPixel.R - rightPixel.R,
                                                      leftPixel.G - rightPixel.G,
                                                      leftPixel.B - rightPixel.B);
                    result.SetPixel(x, y, difference);
                }
            }
            return result;
        }
    }
}
