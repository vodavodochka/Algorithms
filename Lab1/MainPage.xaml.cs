using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab1
{
    public partial class MainPage : ContentPage
    {
        private List<IterationData> _data;
        private List<IterationData> _data_temp;
        private Stopwatch _stopwatch;
        private bool Drawing = true;
        public int dataMax = 1;
        public int loopNumber = 1;

        public MainPage()
        {
            InitializeComponent();

            _data = new List<IterationData>();
            _data_temp = new List<IterationData>();
            _stopwatch = new Stopwatch();

            // Создание графика
            var chartDrawable = new ChartDrawable(_data);
            graphicsView.Drawable = chartDrawable;
            TestText.Text = "asd";

            // Запуск измерения времени выполнения итераций
        }

        public void GraphDraw(object sender, EventArgs args)
        {
            // Очистка данных перед началом нового измерения
            _data.Clear();
            _data_temp.Clear();

            if (Drawing)
            {
                Drawing = false;
                for (int iteration = 1; iteration <= loopNumber; iteration++)
                {
                    MeasureIterations(1, iteration);
                }

                // Вычисление среднего арифметического значения и обновление графика
                CalculateAverageData();
                var chartDrawable = new ChartDrawable(_data);
                graphicsView.Drawable = chartDrawable;
                Drawing = true;
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

            // Обновление графика при изменении количества итераций
            //GraphDraw(sender, e);
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

            // Обновление графика при изменении максимального количества данных
            //GraphDraw(sender, e);
        }

        private void MeasureIterations(int process_id, int it)
        {
            for (int n = 1; n <= dataMax; n++)
            {
                _stopwatch.Restart();

                // Симуляция выполнения итерации
                SimulateIteration(n, process_id);

                _stopwatch.Stop();
                _data_temp.Add(new IterationData { IterationNumber = n, TimeSpent = _stopwatch.Elapsed.TotalSeconds });
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

        private void SimulateIteration(int n, int id)
        {
            if (id == 1)
            {
                Thread.Sleep(10);
            }
            //....
        }
    }

    public class ChartDrawable : IDrawable
    {
        private readonly List<IterationData> _data;

        public ChartDrawable(List<IterationData> data)
        {
            _data = data;
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

    public class IterationData
    {
        public int IterationNumber { get; set; }
        public double TimeSpent { get; set; }
    }
}
