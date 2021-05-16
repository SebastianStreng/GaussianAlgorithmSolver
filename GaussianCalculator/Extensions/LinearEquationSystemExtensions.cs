using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GaussianCalculator.Core;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GaussianCalculator.Extensions
{
    public static class LinearEquationSystemExtensions
    {
        public static LinearEquationSystem AddRow(this LinearEquationSystem system)
        {
            var rowCount = system.Matrix.RowCount;
            var vector = Enumerable.Repeat(0.0, rowCount + 1);

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

            //Für jede Reihe, betrachte Diagonalwert
            for (int diag = 0; diag < rows; diag++)
            {
                //Suche Reihe mit höchstem Wert in der Spalte, die den Diagonalwert enthält
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

                //Tausche Reihen, sodass Reihe mit höchstem Wert jetzt in der Reihe der aktuellen Diagonale ist.
                system.Matrix = system.Matrix.SwapRows(diag, highestValueRow);
                system.Vector = system.Vector.SwapRows(diag, highestValueRow);

                //*Haltepunkt* -> Reihen werden vertauscht
                await WaitAndRefreshDisplay();

                //Teile jeden Wert in der Reihe durch den Diagonalwert, sodass eine 1 entsteht
                double divider = 1 / system.Matrix[diag, diag];
                for (int col = 0; col < cols; col++)
                {
                    system.Matrix[diag, col] *= divider;
                    //*Haltepunkt* -> Jeder Wert der Reihe wird durch Diagonalwert geteilt
                    await WaitAndRefreshDisplay();
                }
                system.Vector[diag] *= divider;

                //Ziehe diese Reihe von jeder Folgenden so oft ab, dass in der Spalte eine 0 entsteht
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

                        //*Haltepunkt* -> Nullen werden erzeugt
                        await WaitAndRefreshDisplay();
                    }
                }
            }

            return system;

            //Aktualisiere Matrix in User-Oberfläche und warte kurz
            async Task WaitAndRefreshDisplay()
            {
                await Task.Delay(delayPerStep);
                onPropertyChanged?.Invoke();
            }
        }
    }
}