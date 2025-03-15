using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Opening: MultiFilter
    {
        public Opening(bool[,] erosionMask, bool[,] dilatationMask)
        {
            Erosion erosion = new Erosion(erosionMask);
            Dilatation dilatation = new Dilatation(dilatationMask);
            this.actions.Add(erosion);
            this.actions.Add(dilatation);
        }
    }
}
