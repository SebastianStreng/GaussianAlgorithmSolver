using System;
using System.Collections.Generic;
using System.Text;
using GaussianCalculator.Core;
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

        public static LinearEquationSystem SolveGauss(this LinearEquationSystem system)
        {
            var rows = system.Matrix.RowCount;
            var cols = system.Matrix.ColumnCount;

            for (int diag = 0; diag < rows; diag++)
            {
                int highestValueRow = diag;
                double highestValue = Math.Abs(system.Matrix[diag, diag]);

                double d = 0.0;

                for (int row = diag + 1; row < rows; row++)
                {
                    if (Math.Abs(system.Matrix[row, diag]) > highestValue)
                    {
                        highestValue = Math.Abs(system.Matrix[row, diag]);
                        highestValueRow = row;
                    }
                }

                system.Matrix = system.Matrix.SwapRows(diag, highestValueRow);
                system.Vector = system.Vector.SwapRows(diag, highestValueRow);

                double divider = 1 / system.Matrix[diag, diag];

                for (int col = 0; col < cols; col++)
                {
                    system.Matrix[diag, col] *= divider;
                    system.Vector[diag] *= divider;
                }

                for (int row = 0; row < rows; row++)
                {
                    d = system.Matrix[row, diag];
                    if (row != diag)
                    {
                        for (int col = diag; col < cols; col++)
                        {
                            system.Matrix[row, col] -= d * system.Matrix[diag, col];
                        }
                        system.Vector[row] -= d * system.Vector[diag];
                    }
                }
            }

            return system;
        }
    }

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