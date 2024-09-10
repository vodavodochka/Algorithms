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
        private Stopwatch _stopwatch;

        public MainPage()
        {
            InitializeComponent();

            _data = new List<IterationData>();
            _stopwatch = new Stopwatch();

            // Создание графика
            var chartDrawable = new ChartDrawable(_data);
            graphicsView.Drawable = chartDrawable;

            // Запуск измерения времени выполнения итераций
            MeasureIterations();
        }

        private void MeasureIterations()
        {
            for (int i = 1; i <= 2000; i++)
            {
                _stopwatch.Restart();

                //Будут if-ы которые подвязаны к кнопкам чтоы выполнять различные алгоритмы :)

                // Симуляция выполнения итерации
                SimulateIteration();

                _stopwatch.Stop();
                _data.Add(new IterationData { IterationNumber = i, TimeSpent = _stopwatch.Elapsed.TotalSeconds });
            }

            // Обновление графика
            var chartDrawable = new ChartDrawable(_data);
            graphicsView.Drawable = chartDrawable;
        }

        private void SimulateIteration()
        {
            
            // Симуляция выполнения итерации (например, задержка)
            System.Threading.Thread.Sleep(1); // Задержка в 1 миллисекунду
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
            var xScale = chartWidth / 2000;
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
            for (int i = 0; i <= 2000; i += 250)
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
                var timeString = timeValue < 1 ? timeValue.ToString("F4") : timeValue.ToString("F2");
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
