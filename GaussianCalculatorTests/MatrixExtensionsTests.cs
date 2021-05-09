using GaussianCalculator;
using GaussianCalculator.Extensions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;

namespace GaussianCalculatorTests
{
    public class Tests
    {
        private Matrix<double> matrix;

        private Vector<double> vector;

        [SetUp]
        public void Setup()
        {
            double[,] a = {
                { 1.0, 2.0, 4.0 },
                { 3.0, 4.0, 9.0 },
                { 5.0, 1.0, 12.0 }
            };

            matrix = Matrix.Build.DenseOfArray(a);

            double[] b = {
                3.0, 4.0, 9.0
            };

            vector = Vector.Build.DenseOfArray(b);
        }

        [Test]
        public void SwapRowsMatrixTest()
        {
            double[] b = {
                3.0, 4.0, 9.0
            };

            var B = Vector.Build.DenseOfArray(b);

            var swapped = matrix.SwapRows(0, 1);

            var row1 = swapped.Row(0);

            Assert.That(row1.Equals(B));
        }

        [Test]
        public void SwapRowsVectorTest()
        {
            var swapped = vector.SwapRows(0, 1);

            Assert.That(swapped[0] == vector[1]);
        }

        [Test]
        public void TestProgram()
        {
            Program.Main(new[] { "" });
        }
    }
}