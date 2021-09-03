using CsvHelper;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SevenSegmentVideoToGraph
{
    public partial class ResultWindow : Window
    {
        public ZoomingOptions ZoomingMode { get; set; }

        private SeriesCollection seriesCollection;
        private ChartValues<ObservablePoint> chartValues;
        private StepLineSeries stepLineSeries;
        private DispatcherTimer timer = new DispatcherTimer();
        private int timerCounter = 0;
        private FrameData[] frames;

        public ResultWindow()
        {
            InitializeComponent();
            ZoomingMode = ZoomingOptions.X;

            timer.Tick += TimerTick;
            sliderGraphInterval.Value = 250;
        }

        public ResultWindow(List<FrameData> frames) : this()
        {
            this.frames = frames.ToArray();
            dataGridMain.ItemsSource = frames;

            chartValues = new ChartValues<ObservablePoint>();
            stepLineSeries = new StepLineSeries();
            seriesCollection = new SeriesCollection();

            CreateGraphComponents(ref stepLineSeries, ref chartValues);

            seriesCollection.Add(stepLineSeries);
            cartesianChartMain.Series = seriesCollection;

            cartesianChartMain.DataContext = this;
        }

        #region Exports

        private void ButtonExportCsvClick(object sneder, RoutedEventArgs e)
        {
            SaveFileDialog resultsDialog = BrowserDialog.GetSaveFileDialogCsv();
            if (resultsDialog.ShowDialog() == true)
            {
                string path = resultsDialog.FileName;

                if (!Path.GetExtension(path).Equals(".csv"))
                    path += ".csv";

                using (var writer = new StreamWriter(path))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(frames);
                    }
                }
            }
        }

        private void ButtonExportImageClick(object sneder, RoutedEventArgs e)
        {
            SaveFileDialog resultsDialog = BrowserDialog.GetSaveFileDialogPng();
            if (resultsDialog.ShowDialog() == true)
            {
                string path = resultsDialog.FileName;

                if (!Path.GetExtension(path).Equals(".png"))
                    path = path + ".png";

                // Need to recreate the graph
                StepLineSeries stepLineSeries1 = new StepLineSeries();
                ChartValues<ObservablePoint> chartValues1 = new ChartValues<ObservablePoint>();
                CreateGraphComponents(ref stepLineSeries1, ref chartValues1);

                SeriesCollection seriesCollection1 = new SeriesCollection();
                seriesCollection1.Add(stepLineSeries1);

                AxesCollection axisX = new AxesCollection
                {
                    new Axis() { Title = cartesianChartMain.AxisX[0].Title, FontSize = 24 }
                };

                AxesCollection axisY = new AxesCollection
                {
                    new Axis() { Title = cartesianChartMain.AxisY[0].Title, FontSize = 24 }
                };

                stepLineSeries1.Title = stepLineSeries.Title;

                CartesianChart cartesianChartExport = new CartesianChart
                {
                    DisableAnimations = true,
                    Width = Convert.ToDouble(textBoxImageWidth.Text),
                    Height = Convert.ToDouble(textBoxImageHeight.Text),
                    Series = seriesCollection1,
                    AxisX = axisX,
                    AxisY = axisY
                };

                // Code used from live chart samples
                Viewbox viewbox = new Viewbox();
                viewbox.Child = cartesianChartExport;
                viewbox.Measure(cartesianChartExport.RenderSize);
                viewbox.Arrange(new Rect(new Point(0, 0), cartesianChartExport.RenderSize));
                // Force chart redraw
                cartesianChartExport.Update(true, true);
                viewbox.UpdateLayout();

                SaveToPng(cartesianChartExport, path);
            }
        }

        private void TextBoxNumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = InputHelper.IsInputPositiveNumbersOnly(e.Text);
        }

        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }

        #endregion

        #region Graph Controls

        private void ButtonGraphAnimateStartClick(object sneder, RoutedEventArgs e)
        {
            timerCounter = 0;
            seriesCollection[0].Values.Clear();
            ShowGraph();
            timer.Start();
        }

        private void ButtonGraphAnimatePauseClick(object sneder, RoutedEventArgs e)
        {
            timer.Stop();
            ShowGraph();
        }

        private void ButtonGraphAnimateFinishClick(object sneder, RoutedEventArgs e)
        {
            timer.Stop();
            seriesCollection[0].Values.Clear();
            ShowGraph();

            for (int i = 0; i < frames.Length; i++)
                seriesCollection[0].Values.Add(new ObservablePoint(frames[i].Timestamp / 1000, (double)frames[i].Value));
        }

        private void SliderGraphIntervalValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(e.NewValue));
            labelGraphInterval.Content = string.Format(Properties.Resources.FrameInterval, e.NewValue.ToString("0.####"));
        }

        private void TimerTick(object sender, EventArgs e)
        {
            seriesCollection[0].Values.Add(new ObservablePoint(frames[timerCounter].Timestamp / 1000, (double)frames[timerCounter].Value));

            if (timerCounter < frames.Length - 1)
                timerCounter++;
        }

        private void GraphSizeChanged(object sender, SizeChangedEventArgs e)
        {
            textBoxImageWidth.Text = e.NewSize.Width.ToString();
            textBoxImageHeight.Text = e.NewSize.Height.ToString();
        }

        private void textBoxGraphTitleTextChanged(object sender, TextChangedEventArgs args)
        {
            stepLineSeries.Title = textBoxGraphTitle.Text;
        }

        private void textBoxGraphTitleYTextChanged(object sender, TextChangedEventArgs args)
        {
            cartesianChartMain.AxisY[0].Title = textBoxGraphYTitle.Text;
        }

        #endregion

        #region Views

        private void ButtonViewChangeClick(object sneder, RoutedEventArgs e)
        {
            if (dataGridMain.Visibility == Visibility.Hidden)
                ShowDataGrid();
            else
                ShowGraph();
        }

        private void ShowDataGrid()
        {
            cartesianChartMain.Visibility = Visibility.Hidden;
            dataGridMain.Visibility = Visibility.Visible;
        }

        private void ShowGraph()
        {
            dataGridMain.Visibility = Visibility.Hidden;
            cartesianChartMain.Visibility = Visibility.Visible;
        }

        #endregion

        private void ButtonBackClick(object sneder, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateGraphComponents(ref StepLineSeries stepLineSeries, ref ChartValues<ObservablePoint> chartValues)
        {
            // https://lvcharts.net/App/examples/v1/wpf/Performance%20Tips
            ObservablePoint[] temporalChartValues = new ObservablePoint[frames.Length];
            for (int i = 0; i < frames.Length; i++)
                temporalChartValues[i] = new ObservablePoint(frames[i].Timestamp / 1000, (double)frames[i].Value);
            chartValues.AddRange(temporalChartValues);

            stepLineSeries.Title = Properties.Resources.ResultsGraphTitle;
            stepLineSeries.StrokeThickness = 2;
            stepLineSeries.Stroke = new SolidColorBrush(Color.FromRgb(0, 120, 255));
            stepLineSeries.AlternativeStroke = new SolidColorBrush(Color.FromRgb(0, 120, 255));
            stepLineSeries.PointGeometrySize = 0;
            stepLineSeries.Values = chartValues;
        }
    }
}