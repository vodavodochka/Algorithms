using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Maui.Controls.StyleSheets;
using Lab1;
using MathNet.Numerics;
using MathNet.Numerics.Interpolation;

namespace Lab1
{
    public partial class MainPage : ContentPage
    {
        private List<IterationData> _data;
        private List<IterationData> _data_temp;
        private bool Drawing = true;
        public int dataMax = 1;
        public int loopNumber = 1;
        public int polynomPower = 1;
        public int baseNumber = 1;
        public int exponent = 1;

        public MainPage()
        {
            InitializeComponent();

            _data = new List<IterationData>();
            _data_temp = new List<IterationData>();

            // Создание графика
            var chartDrawable = new ChartDrawable(_data);
            graphicsView.Drawable = chartDrawable;
        }

        public void MatrixMulti(object sender, EventArgs e)
        {
            GraphDraw(new MatrixMultiplication());
        }

        public void LongestIncreasedSubsequence(object sender, EventArgs e)
        {
            GraphDraw(new LongestIncreasingSubsequence());
        }

        public void Pow(object sender, EventArgs e)
        {
            GraphDraw(new PowerAlgorithm());
        }

        public void FastPow(object sender, EventArgs e)
        {
            GraphDraw(new FastPower());
        }

        public void RecursivePow(object sender, EventArgs e)
        {
            GraphDraw(new RecursivePower());
        }

        public void BubbleSorting(object sender, EventArgs e)
        {
            GraphDraw(new BubbleSort());
        }

        public void ConstantFunc(object sender, EventArgs e)
        {
            GraphDraw(new ConstantFunction());
        }

        public void MultFunc(object sender, EventArgs e)
        {
            GraphDraw(new MultFunction());
        }

        public void PolynomHornerFunc(object sender, EventArgs e)
        {
            GraphDraw(new PolynomHorner());
        }

        public void PolynomNaiveFunc(object sender, EventArgs e)
        {
            GraphDraw(new PolynomNaive());
        }

        public void SummingFunc(object sender, EventArgs e)
        {
            GraphDraw(new SummingFunction());
        }

        public void QuickSorting(object sender, EventArgs e)
        {
            GraphDraw(new QuickSort());
        }

        public void TimSorting(object sender, EventArgs e)
        {
            GraphDraw(new Timsort());
        }

        public void RadixSorting(object sender, EventArgs e)
        {
            GraphDraw(new RadixSort());
        }

        public void GraphDraw(Algorithm algorithm)
        {
            // Очистка данных перед началом нового измерения
            _data.Clear();
            _data_temp.Clear();

            if (Drawing)
            {
                Drawing = false;
                algorithm.Start(dataMax);
                _data_temp = algorithm.GetIterationData();

                // Вычисление среднего арифметического значения и обновление графика
                CalculateAverageData();

                // Аппроксимация данных
                var fittedData = FitData(_data);

                // Обновление графика с аппроксимированными данными
                var chartDrawable = new ChartDrawable(_data, fittedData);
                graphicsView.Drawable = chartDrawable;
                Drawing = true;
            }
        }

        public void GraphDraw(AlgorithmWithSteps algorithm)
        {
            // Очистка данных перед началом нового измерения
            _data.Clear();

            if (Drawing)
            {
                Drawing = false;
                algorithm.Start(dataMax, baseNumber);
                _data = algorithm.GetIterationData();

                // Обновление графика с аппроксимированными данными
                var chartDrawable = new ChartDrawable(_data);
                graphicsView.Drawable = chartDrawable;
                Drawing = true;
            }
        }

        public void OnEntryBaseNumber(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                baseNumber = 1;
            }
            else
            {
                if (int.TryParse(text, out int number))
                {
                    baseNumber = number;
                }
                else
                {
                    baseNumber = 1;
                }
            }
        }

        public void OnEntryPolynomPower(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                polynomPower = 1;
            }
            else
            {
                if (int.TryParse(text, out int number))
                {
                    polynomPower = number;
                }
                else
                {
                    polynomPower = 1;
                }
            }
        }

        public void OnEntryIterations(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                loopNumber = 1;
            }
            else
            {
                if (int.TryParse(text, out int number))
                {
                    loopNumber = number;
                }
                else
                {
                    loopNumber = 1;
                }
            }
        }

        public void OnEntryData(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                dataMax = 1;
            }
            else
            {
                if (int.TryParse(text, out int number))
                {
                    dataMax = number;
                }
                else
                {
                    dataMax = 1;
                }
            }
        }

        private void CalculateAverageData()
        {
            if (_data_temp.Count == 0) return;

            var groupedData = _data_temp.GroupBy(d => d.IterationNumber).Select(g => new IterationData
            {
                IterationNumber = g.Key,
                TimeSpent = g.Average(d => d.TimeSpent)
            }).ToList();

            _data.AddRange(groupedData);
        }

        //private string GetPolynomialString(double[] coefficients)
        //{
        //    var terms = new List<string>();
        //    for (int i = coefficients.Length - 1; i >= 0; i--)
        //    {
        //        if (coefficients[i] != 0)
        //        {
        //            string term = $"{coefficients[i]:F2}";
        //            if (i > 0)
        //            {
        //                term += $" * x^{i}";
        //            }
        //            terms.Add(term);
        //        }
        //    }
        //    return string.Join(" + ", terms);
        //}


        private List<IterationData> FitData(List<IterationData> data)
        {
            if (data.Count < 3)
            {
                return new List<IterationData>();
            }

            double[] xData = data.Select(d => (double)d.IterationNumber).ToArray();
            double[] yData = data.Select(d => d.TimeSpent).ToArray();

            int degree = polynomPower; // Степень полинома
            double[] polynomialFit = Fit.Polynomial(xData, yData, degree);

            // Вывод полученного полинома
            //string polynomialString = GetPolynomialString(polynomialFit);
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    DisplayAlert("Polynomial Fit", $"Fitted Polynomial: {polynomialString}", "OK");
            //});

            var fittedData = new List<IterationData>();
            for (int i = 1; i <= dataMax; i++)
            {
                double yFit = polynomialFit.Zip(Enumerable.Range(0, degree + 1), (c, p) => c * Math.Pow(i, p)).Sum();
                fittedData.Add(new IterationData { IterationNumber = i, TimeSpent = yFit });
            }

            return fittedData;
        }


    }

    
}
