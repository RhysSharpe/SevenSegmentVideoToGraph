using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SevenSegmentVideoToGraph
{
    public partial class UnprocessedFrameDialog : Window
    {
        private List<ComboBoxItemValue> comboBoxSelectionValues;
        private UnprocessedFrameDisplayHelper frameDisplayHelper;
        private List<int> unprocessedFramesIndex = new List<int>();

        public UnprocessedFrameDialog()
        {
            InitializeComponent();
        }

        public UnprocessedFrameDialog(List<FrameData> frames) : this()
        {
            for (int i = 0; i < frames.Count; i++)
            {
                if (frames[i].IsOutsideRange() || frames[i].Skip || frames[i].GetOcrFailed())
                    unprocessedFramesIndex.Add(i);
            }

            frameDisplayHelper = new UnprocessedFrameDisplayHelper(frames, unprocessedFramesIndex.ToArray());
            UpdateUserInterface();

            // Add a skip in future
            comboBoxSelectionValues = new List<ComboBoxItemValue>();
            comboBoxSelectionValues.Add(new ComboBoxItemValuePrevious(frameDisplayHelper));
            comboBoxSelectionValues.Add(new ComboBoxItemValueNext(frameDisplayHelper));
            comboBoxSelectionValues.Add(new ComboBoxItemValueAverage(frameDisplayHelper));
            comboBoxSelectionValues.Add(new ComboBoxItemValue("comboBoxItemCustom", Properties.Resources.UnprocessedFrameCustom));
            comboBoxUnprocessedFrame.ItemsSource = comboBoxSelectionValues;
            comboBoxUnprocessedFrame.SelectedIndex = 3;
        }

        private void ComboBoxUnprocessedFrameSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxUnprocessedFrame.SelectedItem is ComboBoxItemValue)
            {
                ComboBoxItemValue comboBoxItemValue = comboBoxUnprocessedFrame.SelectedItem as ComboBoxItemValue;

                if (comboBoxItemValue.Calculate() != null)
                    textBoxValue.Text = comboBoxItemValue.Calculate().ToString();
            }
        }

        private void TextBoxNumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = InputHelper.IsInputNumbersOnly(e.Text);
        }

        private void textBoxValueTextChanged(object sender, RoutedEventArgs e)
        {
            buttonContinue.IsEnabled = !string.IsNullOrEmpty(textBoxValue.Text);

            if (string.IsNullOrEmpty(textBoxValue.Text))
            {
                comboBoxUnprocessedFrame.SelectedIndex = 3;
                return;
            }

            for (int i = 0; i < comboBoxSelectionValues.Count; i++)
            {
                if (comboBoxSelectionValues[i].Calculate() == Convert.ToDouble(textBoxValue.Text))
                {
                    comboBoxUnprocessedFrame.SelectedIndex = i;
                    return;
                }
            }

            comboBoxUnprocessedFrame.SelectedIndex = 3;
        }

        private void ButtonToggleClick(object sender, RoutedEventArgs e)
        {
            UpdateContinueButton();
        }

        private void ButtonContinueClick(object sender, RoutedEventArgs e)
        {
            if (toggleButtonRepeat.IsChecked == true)
            {
                while (frameDisplayHelper.UnprocessedIndex < frameDisplayHelper.UnprocessedFramesIndex.Length)
                {
                    ComboBoxItemValue comboBoxItemValue = comboBoxUnprocessedFrame.SelectedItem as ComboBoxItemValue;
                    frameDisplayHelper.GetCurrent().Value = comboBoxItemValue.Calculate() == null ? Convert.ToDouble(textBoxValue.Text) : comboBoxItemValue.Calculate();
                    frameDisplayHelper.GetCurrent().OcrConfidence = null;

                    if (frameDisplayHelper.UnprocessedIndex == frameDisplayHelper.UnprocessedFramesIndex.Length - 1)
                        break;

                    frameDisplayHelper.UnprocessedIndex++;
                }

                MessageBox.Show(Properties.Resources.UnprocessedFrameFinished, Properties.Resources.AppTitleFinished, MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                frameDisplayHelper.GetCurrent().Value = Convert.ToDouble(textBoxValue.Text);
                frameDisplayHelper.GetCurrent().OcrConfidence = null;

                if (frameDisplayHelper.UnprocessedIndex == frameDisplayHelper.UnprocessedFramesIndex.Length - 1)
                    Close();

                frameDisplayHelper.GetNext();
                UpdateUserInterface();
            }
        }

        private void UpdateUserInterface()
        {
            imageMain.Source = new ImageSourceConverter().ConvertFromString(frameDisplayHelper.GetCurrent().OriginalPath) as ImageSource;

            labelFrameIndex.Content = string.Format(Properties.Resources.UnprocessedFrameIndex, frameDisplayHelper.UnprocessedIndex + 1,
                frameDisplayHelper.UnprocessedFramesIndex.Length);

            labelFramePath.Content = frameDisplayHelper.GetCurrent().ProcessedPath;
            toggleButtonRepeat.Content = string.Format(Properties.Resources.UnprocessedFrameRepeat, frameDisplayHelper.UnprocessedFramesIndex.Length);

            if (frameDisplayHelper.GetCurrent().Skip)
                textBlockInformation.Text = string.Format(Properties.Resources.UnprocessedFrameSkipped);
            else if (frameDisplayHelper.GetCurrent().IsOutsideRange())
                textBlockInformation.Text = string.Format(Properties.Resources.UnprocessedFrameRange, frameDisplayHelper.GetCurrent().Value,
                    frameDisplayHelper.GetCurrent().OcrMin, frameDisplayHelper.GetCurrent().OcrMax);
            else if (frameDisplayHelper.GetCurrent().GetOcrFailed())
                textBlockInformation.Text = Properties.Resources.UnprocessedFrameFailed;

            textBoxValue.Text = string.Empty;
            UpdateContinueButton();
        }

        private bool UpdateContinueButton()
        {
            if (toggleButtonRepeat.IsChecked == true || frameDisplayHelper.UnprocessedIndex == frameDisplayHelper.UnprocessedFramesIndex.Length - 1)
            {
                buttonContinue.Content = Properties.Resources.UnprocessedFrameFinish;
                return true;
            }
            else
            {
                buttonContinue.Content = Properties.Resources.UnprocessedFrameContinue;
                return false;
            }
        }
    }
}