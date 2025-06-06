﻿using Emgu.CV;
using Emgu.CV.Structure;

using System.Collections.Generic;

using OxyPlot;
using OxyPlot.Axes;
using LinearAxis = OxyPlot.Axes.LinearAxis;
using LineSeries = OxyPlot.Series.LineSeries;

using static Framework.Utilities.DataProvider;

namespace Framework.ViewModel
{
    internal class ColumnColorLevelsVM : ColorLevelsVM
    {
        public override PlotModel PlotImage(IImage image)
        {
            var plot = new PlotModel();
            plot.Series.Clear();

            plot.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Maximum = image.Size.Height + 30,
                Minimum = -1,
            });

            plot.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 300,
                Minimum = -1,
            });

            if (image is Image<Gray, byte> grayImage)
            {
                LineSeries series = CreateSeries(grayImage, 0, OxyColors.Blue);
                plot.Series.Add(series);
            }
            else if (image is Image<Bgr, byte> colorImage)
            {
                LineSeries seriesBlue = CreateSeries(colorImage, 0, OxyColors.Blue);
                LineSeries seriesGreen = CreateSeries(colorImage, 1, OxyColors.Green);
                LineSeries seriesRed = CreateSeries(colorImage, 2, OxyColors.Red);

                plot.Series.Add(seriesBlue);
                plot.Series.Add(seriesGreen);
                plot.Series.Add(seriesRed);
            }

            return plot;
        }

        protected override LineSeries CreateSeries<TColor>(Image<TColor, byte> image, int channel, OxyColor color)
        {
            List<int> values = new List<int>();

            if (LastMouseClick.X < image.Width)
            {
                for (int y = 0; y < image.Height; y++)
                    values.Add(image.Data[y, (int)LastMouseClick.X, channel]);
            }

            LineSeries series = CreateSeries(values, color);
            return series;
        }

        public override CLevelsType Type { get; set; } = CLevelsType.Column;

        public override string Title { get; set; } = "Color Levels on column";
    }
}