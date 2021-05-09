using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace GaussianCalculator.Extensions
{
    public static class MatrixExtensions
    {
        public static Matrix<double> SwapRows(this Matrix<double> matrix, int rowA, int rowB)
        {
            var tempMatrix = matrix.Clone();

            tempMatrix.SetRow(rowA, matrix.Row(rowB));
            tempMatrix.SetRow(rowB, matrix.Row(rowA));

            return tempMatrix;
        }
    }
}