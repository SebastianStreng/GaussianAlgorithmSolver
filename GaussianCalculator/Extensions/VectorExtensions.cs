using MathNet.Numerics.LinearAlgebra;

namespace GaussianCalculator.Extensions
{
    public static class VectorExtensions
    {
        public static Vector<double> SwapRows(this Vector<double> vector, int rowA, int rowB)
        {
            var tempVector = vector.Clone();

            tempVector.SetSubVector(rowA, 1, vector.SubVector(rowB, 1));
            tempVector.SetSubVector(rowB, 1, vector.SubVector(rowA, 1));

            return tempVector;
        }
    }
}