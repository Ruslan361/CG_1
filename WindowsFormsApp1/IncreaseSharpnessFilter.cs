using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class IncreaseSharpnessFilter : MatrixFilter
    {
        public IncreaseSharpnessFilter()
        {

            this.kernel = new float[,] { { 0.0f, -1.0f, 0.0f},
                                            { -1.0f, 5.0f, -1.0f},
                                            { 0.0f, -1.0f, 0.0f} };
            
        }
    }
}
