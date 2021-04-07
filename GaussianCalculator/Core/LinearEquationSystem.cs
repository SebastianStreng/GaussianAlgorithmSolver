using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace GaussianCalculator.Core
{
    public class LinearEquationSystem
    {
        public LinearEquationSystem(Matrix<double> matrix, Vector<double> vector)
        {
            Matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
            Vector = vector ?? throw new ArgumentNullException(nameof(vector));
        }

        public Matrix<double> Matrix { get; set; }

        public Vector<double> Vector { get; set; }
    }
}