using System;
using System.Collections.Generic;
using System.Text;

namespace GaussianCalculator
{
    internal class Vector
    {
        private double[] b;
        internal readonly int rows;

        internal Vector(int rows)
        {
            this.rows = rows;
            b = new double[rows];
        }

        internal Vector(double[] initArray)
        {
            b = (double[])initArray.Clone();
            rows = b.Length;
        }

        internal Vector Clone()
        {
            Vector v = new Vector(b);
            return v;
        }

        internal double this[int row]
        {
            get { return b[row]; }
            set { b[row] = value; }
        }

        internal void SwapRows(int r1, int r2)
        {
            if (r1 == r2) return;
            double tmp = b[r1];
            b[r1] = b[r2];
            b[r2] = tmp;
        }

        internal double norm(double[] weights)
        {
            double sum = 0;
            for (int i = 0; i < rows; i++)
            {
                double d = b[i] * weights[i];
                sum += d * d;
            }
            return Math.Sqrt(sum);
        }

        internal void print()
        {
            for (int i = 0; i < rows; i++)
                Console.WriteLine(b[i]);
            Console.WriteLine();
        }

        public static Vector operator -(Vector lhs, Vector rhs)
        {
            Vector v = new Vector(lhs.rows);
            for (int i = 0; i < lhs.rows; i++)
                v[i] = lhs[i] - rhs[i];
            return v;
        }
    }
}
