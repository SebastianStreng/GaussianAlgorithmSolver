using System;
using System.Collections.Generic;
using System.Text;
using GaussianCalculator.Extentions;

namespace GaussianCalculator
{
    class Matrix
    {
        private double[] b;
        internal readonly int rows, cols;

        internal Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            b = new double[rows * cols];

        }

        internal Matrix(int size)
        {
            this.rows = size;
            this.cols = size;
            b = new double[rows * cols];
            for (int i = 0; i < size; i++)
                this[i, i] = 1;
        }

        internal Matrix(int rows, int cols, double[] initArray)
        {
            this.rows = rows;
            this.cols = cols;
            b = (double[])initArray.Clone();
            if (b.Length != rows * cols) throw new Exception("bad init array");
        }

        internal double this[int row, int col]
        {
            get { return b[row * cols + col]; }
            set { b[row * cols + col] = value; }
        }

        public static Vector operator *(Matrix lhs, Vector rhs)
        {
            if (lhs.cols != rhs.rows) throw new Exception("I can't multiply matrix by vector");
            Vector v = new Vector(lhs.rows);
            for (int i = 0; i < lhs.rows; i++)
            {
                double sum = 0;
                for (int j = 0; j < rhs.rows; j++)
                    sum += lhs[i, j] * rhs[j];
                v[i] = sum;
            }
            return v;
        }

        internal void SwapRows(int r1, int r2)
        {
            if (r1 == r2) return;
            int firstR1 = r1 * cols;
            int firstR2 = r2 * cols;
            for (int i = 0; i < cols; i++)
            {
                double tmp = b[firstR1 + i];
                b[firstR1 + i] = b[firstR2 + i];
                b[firstR2 + i] = tmp;
            }
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
                SwapRows(diag, max_row);
                B.SwapRows(diag, max_row);
                double invd = 1 / this[diag, diag];
                for (int col = diag; col < cols; col++)
                    this[diag, col] *= invd;
                B[diag] *= invd;
                for (int row = 0; row < rows; row++)
                {
                    d = this[row, diag];
                    if (row != diag)
                    {
                        for (int col = diag; col < cols; col++)
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
                for (int j = 0; j < cols; j++)
                    Console.Write(this[i, j].ToString() + "  ");
                Console.WriteLine();
            }
        }
    }
}

