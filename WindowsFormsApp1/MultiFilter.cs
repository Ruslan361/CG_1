using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class MultiFilter: Action
    {
        protected List<Action> actions;
        public MultiFilter(List<Action> actions)
        {
            this.actions = actions;
        }
        protected MultiFilter()
        {
            this.actions = new List<Action>();
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap result = new Bitmap(sourceImage);
            foreach (Action action in this.actions)
            {
                result = action.processImage(result, worker);
            }
            return result;
        }
    }
}
