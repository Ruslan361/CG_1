using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Closing: MultiFilter
    {
        public Closing(bool[,] dilatationMask, bool[,] erosionMask)
        {
            Erosion erosion = new Erosion(erosionMask);
            Dilatation dilatation = new Dilatation(dilatationMask);
            this.actions.Add(dilatation);
            this.actions.Add(erosion);
        }
    }
}
