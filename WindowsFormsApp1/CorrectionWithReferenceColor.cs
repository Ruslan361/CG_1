using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class CorrectionWithReferenceColor : Filters
    {
        private float Rratio;
        private float Gratio;
        private float Bratio;
        private Color? distColor = null;
        private Color? srcColor = null;
        public CorrectionWithReferenceColor()
        {

        }
        void updateRatios()
        {
            if (distColor is null) return;
            if (srcColor is null) return;
            Color dist = distColor.Value;
            Color src = srcColor.Value;
            Rratio = (float)dist.R / (float)src.R;
            Gratio = (float)dist.G / (float)src.G;
            Bratio = (float)dist.B / (float)src.B;
        }
        public Color DistColor 
        { 
            get
            {
                if (distColor is null)
                    throw new Exception("distColor is not defined");
                return distColor.Value;
            }
            set
            {
                distColor = value;
                updateRatios();
            }
        }
        public Color SourceColor
        {
            get
            {
                if (srcColor is null)
                    throw new Exception("distColor is not defined");
                return srcColor.Value;
            }
            set
            {
                srcColor = value;
                updateRatios();
            }
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (distColor is null) throw new Exception("distColor is not defined");
            if (srcColor is null) throw new Exception("srcColor is not defined");
            return base.processImage(sourceImage, worker);
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            float R = (float)sourceColor.R * Rratio;
            float G = (float)sourceColor.G * Gratio;
            float B = (float)sourceColor.B * Bratio;
            int r = Clamp((int)R, 0, 255);
            int g = Clamp((int)G, 0, 255);
            int b = Clamp((int)B, 0, 255);
            return Color.FromArgb(r, g, b);
        }
    }
}
