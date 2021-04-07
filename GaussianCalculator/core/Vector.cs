using System;
using System.Collections.Generic;
using System.Text;

namespace GaussianCalculator
{
    internal class Vector
    {
        private double[] vectorNumbers;
        internal readonly int rows;

        internal Vector(int rows)
        {
            this.rows = rows;
            vectorNumbers = new double[rows];
        }

        internal Vector(double[] initArray)
        {
            vectorNumbers = (double[])initArray.Clone();
            rows = vectorNumbers.Length;
        }

        internal Vector Clone()
        {
            Vector v = new Vector(vectorNumbers);
            return v;
        }

        internal double this[int row]
        {
            get { return vectorNumbers[row]; }
            set { vectorNumbers[row] = value; }
        }

        internal void SwapRows(int r1, int r2)
        {
            if (r1 == r2) return;
            double tmp = vectorNumbers[r1];
            vectorNumbers[r1] = vectorNumbers[r2];
            vectorNumbers[r2] = tmp;
        }

        internal double norm(double[] weights)
        {
            double sum = 0;
            for (int i = 0; i < rows; i++)
            {
                double d = vectorNumbers[i] * weights[i];
                sum += d * d;
            }
            return Math.Sqrt(sum);
        }

        internal void print()
        {
            for (int i = 0; i < rows; i++)
                Console.WriteLine(vectorNumbers[i]);
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
