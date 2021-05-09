using GaussianCalculator.Core;
using GaussianCalculator.Extensions;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GaussianAlgorithmSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private LinearEquationSystem linearEquationSystem;

        #region matrix 

        public double v11
        {
            get => linearEquationSystem.Matrix[0, 0];
            set
            {
                linearEquationSystem.Matrix[0, 0] = value;
                OnPropertyChanged(nameof(v11));
            }
        }

        public double v12
        {
            get => linearEquationSystem.Matrix[0, 1];
            set
            {
                linearEquationSystem.Matrix[0, 1] = value;
                OnPropertyChanged(nameof(v12));
            }
        }

        public double v13
        {
            get => linearEquationSystem.Matrix[0, 2];
            set
            {
                linearEquationSystem.Matrix[0, 2] = value;
                OnPropertyChanged(nameof(v13));
            }
        }

        public double v14
        {
            get => linearEquationSystem.Matrix[0, 3];
            set
            {
                linearEquationSystem.Matrix[0, 3] = value;
                OnPropertyChanged(nameof(v14));
            }
        }

        public double v15
        {
            get => linearEquationSystem.Vector[0];
            set
            {
                linearEquationSystem.Vector[0] = value;
                OnPropertyChanged(nameof(v14));
            }
        }

        public double v21
        {
            get => linearEquationSystem.Matrix[1, 0];
            set
            {
                linearEquationSystem.Matrix[1, 0] = value;
                OnPropertyChanged(nameof(v21));
            }
        }

        public double v22
        {
            get => linearEquationSystem.Matrix[1, 1];
            set
            {
                linearEquationSystem.Matrix[1, 1] = value;
                OnPropertyChanged(nameof(v22));
            }
        }

        public double v23
        {
            get => linearEquationSystem.Matrix[1, 2];
            set
            {
                linearEquationSystem.Matrix[1, 2] = value;
                OnPropertyChanged(nameof(v23));
            }
        }

        public double v24
        {
            get => linearEquationSystem.Matrix[1, 3];
            set
            {
                linearEquationSystem.Matrix[1, 3] = value;
                OnPropertyChanged(nameof(v24));
            }
        }

        public double v25
        {
            get => linearEquationSystem.Vector[1];
            set
            {
                linearEquationSystem.Vector[1] = value;
                OnPropertyChanged(nameof(v25));
            }
        }

        public double v31
        {
            get => linearEquationSystem.Matrix[2, 0];
            set
            {
                linearEquationSystem.Matrix[2, 0] = value;
                OnPropertyChanged(nameof(v31));
            }
        }

        public double v32
        {
            get => linearEquationSystem.Matrix[2, 1];
            set
            {
                linearEquationSystem.Matrix[2, 1] = value;
                OnPropertyChanged(nameof(v32));
            }
        }

        public double v33
        {
            get => linearEquationSystem.Matrix[2, 2];
            set
            {
                linearEquationSystem.Matrix[2, 2] = value;
                OnPropertyChanged(nameof(v33));
            }
        }

        public double v34
        {
            get => linearEquationSystem.Matrix[2, 3];
            set
            {
                linearEquationSystem.Matrix[2, 3] = value;
                OnPropertyChanged(nameof(v34));
            }
        }

        public double v35
        {
            get => linearEquationSystem.Vector[2];
            set
            {
                linearEquationSystem.Vector[2] = value;
                OnPropertyChanged(nameof(v35));
            }
        }        
        
        public double v41
        {
            get => linearEquationSystem.Matrix[3, 0];
            set
            {
                linearEquationSystem.Matrix[3, 0] = value;
                OnPropertyChanged(nameof(v41));
            }
        }

        public double v42
        {
            get => linearEquationSystem.Matrix[3, 1];
            set
            {
                linearEquationSystem.Matrix[3, 1] = value;
                OnPropertyChanged(nameof(v42));
            }
        }

        public double v43
        {
            get => linearEquationSystem.Matrix[3, 2];
            set
            {
                linearEquationSystem.Matrix[3, 2] = value;
                OnPropertyChanged(nameof(v43));
            }
        }

        public double v44
        {
            get => linearEquationSystem.Matrix[3, 3];
            set
            {
                linearEquationSystem.Matrix[3, 3] = value;
                OnPropertyChanged(nameof(v44));
            }
        }

        public double v45
        {
            get => linearEquationSystem.Vector[3];
            set
            {
                linearEquationSystem.Vector[3] = value;
                OnPropertyChanged(nameof(v45));
            }
        }

        #endregion matrix

        public MainWindow()
        {
            InitializeComponent();
            linearEquationSystem = GetRandom4x5();
        }

        private LinearEquationSystem GetRandom4x5()
        {
            Random rnd = new Random();

            int r() => rnd.Next(1, 20);


            double[,] a = {
                { r(), r(), r(), r() },
                { r(), r(), r(), r() },
                { r(), r(), r(), r() },
                { r(), r(), r(), r() },
            };

            var A = Matrix.Build.DenseOfArray(a);

            double[] b = {
                r(),
                r(),
                r(),
                r()
            };

            var B = MathNet.Numerics.LinearAlgebra.Double.Vector.Build.DenseOfArray(b);

            return new LinearEquationSystem(A, B);
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            linearEquationSystem = GetRandom4x5();
            OnPropertyChanged("");
        }

        private async void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                void onPropChanged() => OnPropertyChanged("");

                if (!double.TryParse(txtIntervall.Text, out var interval))
                    throw new Exception("Please input a valid number for intervall.");

                var system = await linearEquationSystem.SolveGauss(TimeSpan.FromMilliseconds(interval), onPropChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
