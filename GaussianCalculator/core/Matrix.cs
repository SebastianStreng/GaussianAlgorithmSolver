using System;
using System.Collections.Generic;
using System.Text;
using GaussianCalculator.Extentions;

namespace GaussianCalculator
{
    class Matrix
    {
        private double[] number;
        internal readonly int rows, columns;

        internal Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.columns = cols;
            number = new double[rows * cols];

        }

        internal Matrix(int size)
        {
            this.rows = size;
            this.columns = size;
            number = new double[rows * columns];
            for (int i = 0; i < size; i++)
                this[i, i] = 1;
        }

        internal Matrix(int rows, int cols, double[] initArray)
        {
            this.rows = rows;
            this.columns = cols;
            number = (double[])initArray.Clone();
            if (number.Length != rows * cols) throw new Exception("bad init array");
        }

        internal double this[int row, int col]
        {
            get { return number[row * columns + col]; }
            set { number[row * columns + col] = value; }
        }

        public static Vector operator *(Matrix leftHandSide, Vector rightHandSide)
        {
            if (leftHandSide.columns != rightHandSide.rows) throw new Exception("I can't multiply matrix by vector");
            Vector v = new Vector(leftHandSide.rows);
            for (int i = 0; i < leftHandSide.rows; i++)
            {
                double sum = 0;
                for (int j = 0; j < rightHandSide.rows; j++)
                    sum += leftHandSide[i, j] * rightHandSide[j];
                v[i] = sum;
            }
            return v;
        }



        //with partial pivot
        internal void ElimPartial(Vector B)
        {
            for (int diag = 0; diag < rows; diag++)
            {
                int max_row = diag;
                double max_val = Math.Abs(this[diag, diag]);
                double d;
                for (int row = diag + 1; row < rows; row++)
                    if ((d = Math.Abs(this[row, diag])) > max_val)
                    {
                        max_row = row;
                        max_val = d;
                    }
                MatrixExtentions.SwapRows(diag, max_row, columns, number);
                B.SwapRows(diag, max_row);
                double invd = 1 / this[diag, diag];
                for (int col = diag; col < columns; col++)
                    this[diag, col] *= invd;
                B[diag] *= invd;
                for (int row = 0; row < rows; row++)
                {
                    d = this[row, diag];
                    if (row != diag)
                    {
                        for (int col = diag; col < columns; col++)
                            this[row, col] -= d * this[diag, col];
                        B[row] -= d * B[diag];
                    }
                }
            }
        }

        internal void print()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(this[i, j].ToString() + "  ");
                Console.WriteLine();
            }
        }
    }
}

