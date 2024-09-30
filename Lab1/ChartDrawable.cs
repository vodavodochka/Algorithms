using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
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
            var xScale = chartWidth / _data[_data.Count-1].IterationNumber;
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
            // Определяем максимальное значение IterationNumber
            int maxIterationNumber = _data.Max(d => d.IterationNumber);

            // Определяем шаг для отображения меток
            int step = maxIterationNumber < 8 ? 1 : maxIterationNumber / 8;

            // Отображаем метки на нижней оси (итерации)
            for (int i = 0; i <= maxIterationNumber; i += step)
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
