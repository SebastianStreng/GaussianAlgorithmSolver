using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GaussianCalculator.Core;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GaussianCalculator.Extensions
{
    public static class LinearEquationSystemExtensions { 

        public static LinearEquationSystem AddRow(this LinearEquationSystem system)
        {
            var rowCount = system.Matrix.RowCount;
            var vector = Enumerable.Repeat(0.0,  rowCount + 1);

            //Add Column
            system.Matrix = system.Matrix.InsertColumn(system.Matrix.ColumnCount, Vector.Build.DenseOfEnumerable(vector));
            
            //Add Row
            system.Matrix = system.Matrix.InsertRow(system.Matrix.ColumnCount, Vector.Build.DenseOfEnumerable(vector));

            //Add Row
            system.Vector = Vector.Build.DenseOfEnumerable(system.Vector.Append(0.0));

            return system;
        }

        public static double[][] GetArrays(this LinearEquationSystem system)
        {
            var matrix = system.Matrix.InsertColumn(system.Matrix.ColumnCount, system.Vector);

            return matrix.AsRowArrays();
        }

        public static async Task<LinearEquationSystem> SolveGauss(this LinearEquationSystem system, TimeSpan delayPerStep, Action onPropertyChanged = null)
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
                    onPropertyChanged?.Invoke();
                    await Task.Delay(delayPerStep);
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
                        onPropertyChanged?.Invoke();
                        await Task.Delay(delayPerStep);
                    }
                }
            }

            return system;
        }
    }
}