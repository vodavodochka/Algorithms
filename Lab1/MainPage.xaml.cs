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

    public class ChartDrawable : IDrawable
    {
        private readonly List<IterationData> _data;
        private readonly List<IterationData> _fittedData;

        public ChartDrawable(List<IterationData> data, List<IterationData> fittedData = null)
        {
            _data = data;
            _fittedData = fittedData;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (_data == null || _data.Count == 0)
                return;

            var maxTime = _data.Max(d => d.TimeSpent);
            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            // Отступы для меток
            var marginLeft = 90;
            var marginBottom = 90;
            var marginRight = 20;
            var marginTop = 20;

            var chartWidth = width - marginLeft - marginRight;
            var chartHeight = height - marginTop - marginBottom;
            var xScale = chartWidth / _data.Count;
            var yScale = chartHeight / maxTime;

            // Отрисовка осей
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;
            canvas.DrawLine(marginLeft, marginTop, marginLeft, height - marginBottom); // Левая вертикальная ось
            canvas.DrawLine(marginLeft, height - marginBottom, width - marginRight, height - marginBottom); // Нижняя горизонтальная ось

            // Отрисовка данных
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 1;

            for (int i = 0; i < _data.Count - 1; i++)
            {
                var x1 = marginLeft + _data[i].IterationNumber * xScale;
                var y1 = height - marginBottom - (float)_data[i].TimeSpent * yScale;
                var x2 = marginLeft + _data[i + 1].IterationNumber * xScale;
                var y2 = height - marginBottom - (float)_data[i + 1].TimeSpent * yScale;

                canvas.DrawLine(x1, (float)y1, x2, (float)y2);
            }

            // Отрисовка аппроксимированных данных
            if (_fittedData != null && _fittedData.Count > 0)
            {
                canvas.StrokeColor = Colors.Red;
                canvas.StrokeSize = 1;

                for (int i = 0; i < _fittedData.Count - 1; i++)
                {
                    var x1 = marginLeft + _fittedData[i].IterationNumber * xScale;
                    var y1 = height - marginBottom - (float)_fittedData[i].TimeSpent * yScale;
                    var x2 = marginLeft + _fittedData[i + 1].IterationNumber * xScale;
                    var y2 = height - marginBottom - (float)_fittedData[i + 1].TimeSpent * yScale;

                    canvas.DrawLine(x1, (float)y1, x2, (float)y2);
                }
            }

            // Отрисовка меток на осях
            canvas.FontColor = Colors.Black;
            canvas.FontSize = 12;

            // Метки на нижней оси (итерации)
            int step = _data.Count < 8 ? 1 : _data.Count / 8;
            for (int i = 0; i <= _data.Count; i += step)
            {
                var x = marginLeft + i * xScale;
                canvas.DrawString(i.ToString(), x, height - marginBottom + 20, HorizontalAlignment.Center);
            }

            // Метки на левой оси (время)
            var timeStep = maxTime / 10;
            for (int i = 0; i <= 10; i++)
            {
                var y = height - marginBottom - i * timeStep * yScale;
                var timeValue = i * timeStep;
                var timeString = timeValue < 1 ? timeValue.ToString("F6") : timeValue.ToString("F2");
                canvas.DrawString(timeString, marginLeft - 40, (float)y, HorizontalAlignment.Right);
            }
        }
    }
}
