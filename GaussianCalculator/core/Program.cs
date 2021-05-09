using MathNet.Numerics.LinearAlgebra.Double;
using System;
using GaussianCalculator.Core;
using GaussianCalculator.Extensions;

namespace GaussianCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[,] a = {
                { 1.0, 2.0, 4.0 },
                { 3.0, 4.0, 9.0 },
                { 5.0, 1.0, 12.0 },
            };

            var A = Matrix.Build.DenseOfArray(a);

            double[] b = {
                1.0, 2.0, 3.0
            };

            var B = Vector.Build.DenseOfArray(b);

            A.SwapRows(1, 2);

            var system = new LinearEquationSystem(A, B);

            var solution = system.SolveGauss();

            foreach (var row in solution.Matrix.EnumerateRows())
            {
                Console.WriteLine(string.Join(", ", row));
            }

            foreach (var row in solution.Vector)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }
    }
}