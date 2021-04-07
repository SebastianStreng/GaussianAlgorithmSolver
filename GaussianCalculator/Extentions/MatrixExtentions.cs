using System;
using System.Collections.Generic;
using System.Text;

namespace GaussianCalculator.Extentions
{
    class MatrixExtentions
    {
        public static void SwapRows (int row1, int row2, int cols, double [] matrixnumber)
        {
            if (row1 == row2) return;
            int firstR1 = row1 * cols;
            int firstR2 = row2 * cols;
            for (int i = 0; i < cols; i++)
            {
                double tmp = matrixnumber[firstR1 + i];
                matrixnumber[firstR1 + i] = matrixnumber[firstR2 + i];
                matrixnumber[firstR2 + i] = tmp;
            }
        }



    }


}
