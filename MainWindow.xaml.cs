using MediaToolkit.Model;
using MetadataExtractor;
using MetadataExtractor.Formats.FileSystem;
using MetadataExtractor.Formats.QuickTime;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SevenSegmentVideoToGraph
{
    public partial class MainWindow : Window
    {
        private MediaFile video;
        private int videoFPS = 0;
        private bool imageBlackWhiteMode;

        private VideoExtractor videoExtractor;
        private FrameDisplayHelper frameDisplayHelper;
        private ImageProcessor imageProcessor;
        private OpticalCharacterRecognition opticalCharacterRecognition;

        public MainWindow()
        {
            InitializeComponent();
            SetupDefaultControls();
            frameDisplayHelper = new FrameDisplayHelper();
        }

        #region Row One - Select Video

        private void ButtonLocationVideoOpenClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxLocationVideo.Text))
                OpenFolder(Path.GetDirectoryName(textBoxLocationVideo.Text));
        }

        private void ButtonLocationVideoBrowseClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog videoDialog = BrowserDialog.GetMP4File();

            // https://stackoverflow.com/questions/4074585/attempted-to-read-or-write-protected-memory-this-is-often-an-indication-that-ot
            // In the odd case of an memory access violation, it seems like it is caused by NVidia Network Manager
            // Needs extra testing

            if (videoDialog.ShowDialog() == true)
            {
                video = new MediaFile(videoDialog.FileName);

                #region Label assignments

                textBoxLocationVideo.Text = videoDialog.FileName;
                buttonLocationVideoOpen.IsEnabled = true;

                IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(videoDialog.FileName);

                FileMetadataDirectory fileMetadataDirectory = directories.OfType<FileMetadataDirectory>().FirstOrDefault();
                labelVideoTitle.Content = string.Format(Properties.Resources.VideoTitle, fileMetadataDirectory.GetDescription(FileMetadataDirectory.TagFileName));

                // Milliseconds to seconds
                ShellFile shellFile = ShellFile.FromFilePath(videoDialog.FileName);
                labelVideoFPS.Content = string.Format(Properties.Resources.VideoFPS, videoFPS = (int)(shellFile.Properties.System.Video.FrameRate.Value / 1000));

                QuickTimeTrackHeaderDirectory quickTimeTrackHeaderDirectory = directories.OfType<QuickTimeTrackHeaderDirectory>().FirstOrDefault();
                labelVideoWidthHeight.Content = string.Format(Properties.Resources.VideoResolution, quickTimeTrackHeaderDirectory.GetInt32(QuickTimeTrackHeaderDirectory.TagWidth), quickTimeTrackHeaderDirectory.GetInt32(QuickTimeTrackHeaderDirectory.TagHeight));

                QuickTimeMovieHeaderDirectory quickTimeMovieHeaderDirectory = directories.OfType<QuickTimeMovieHeaderDirectory>().FirstOrDefault();
                labelVideoDuration.Content = string.Format(Properties.Resources.VideoDuration, quickTimeMovieHeaderDirectory.GetObject(QuickTimeMovieHeaderDirectory.TagDuration));

                // Convert bytes to MB
                double size = fileMetadataDirectory.GetInt64(FileMetadataDirectory.TagFileSize) / 1024000.0;
                labelVideoSize.Content = string.Format(Properties.Resources.VideoSize, size.ToString("0.##"));

                double frameTime = 1.0D / videoFPS;
                labelFrameInterval.Content = string.Format(Properties.Resources.FrameInterval, frameTime.ToString("0.####"));

                #endregion

                buttonExtractionBeginPause.IsEnabled = true;
                labelFrameInterval.IsEnabled = true;
                sliderFrameInterval.IsEnabled = true;
                sliderFrameInterval.Value = sliderFrameInterval.Minimum;

                // Initialise the video extraction helper ready for extraction upon getting the video object
                videoExtractor = new VideoExtractor(GetExtractionLocation(), video, frameTime);
                videoExtractor.OnStateChanged += VideoExtractor_OnStateChanged;
                videoExtractor.OnExtraction += VideoExtractor_OnExtraction;
                videoExtractor.OnCompleted += VideoExtractor_OnCompleted;
            }
        }

        #endregion

        #region Row Three - Select Extract Location

        private void ButtonLocationExtractionOpenClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxLocationExtraction.Text))
                OpenFolder(textBoxLocationExtraction.Text);
        }

        private void ButtonLocationExtractionBrowseClick(object sender, RoutedEventArgs e)
        {
            string path = BrowserDialog.GetFolderPath();
            textBoxLocationExtraction.Text = path;

            bool rooted = Path.IsPathRooted(path);
            buttonImageLoad.IsEnabled = rooted;
            buttonLocationExtractionOpen.IsEnabled = rooted;

            if (videoExtractor != null && rooted)
                videoExtractor.ExtractionPath = path;
        }

        #endregion

        #region Row Four - Image Controls

        private void ButtonImageToggleClick(object sneder, RoutedEventArgs e)
        {
            imageBlackWhiteMode = !imageBlackWhiteMode;
            UpdateImage();
        }

        private void ButtonImageLoadClick(object sneder, RoutedEventArgs e)
        {
            if (frameDisplayHelper.FindFiles(textBoxLocationExtraction.Text).Count > 0)
            {
                frameDisplayHelper.ImageIndex = 0;
                SetupImageProcessor(frameDisplayHelper.Frames.ToArray());

                MessageBoxResult messageBoxResult = MessageBox.Show(Properties.Resources.ImageLoadQuestion, Properties.Resources.AppTitleQuestion, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    for (int i = 0; i < frameDisplayHelper.Frames.Count; i++)
                        frameDisplayHelper.Frames[i].ProcessedPath = frameDisplayHelper.Frames[i].OriginalPath;

                    EnableImageProcessingControls(false);
                    EnableOcrControls(true);
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.ImageLoadFail, Properties.Resources.AppTitleError, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonImageSkipClick(object sneder, RoutedEventArgs e)
        {
            frameDisplayHelper.ToggleSkip();
            UpdateSkipButton();
        }

        #endregion

        #region Row Five - Frame Interval

        private void SliderFrameIntervalValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Can't divide by 0
            double frameTime = videoFPS <= 0 ? 0 : e.NewValue / videoFPS;

            if (videoExtractor != null)
                videoExtractor.FrameTime = frameTime;

            labelFrameInterval.Content = string.Format(Properties.Resources.FrameInterval, frameTime.ToString("0.####"));
        }

        #endregion

        #region Row Six - Begin Extract

        private void ButtonExtractionBeginPauseClick(object sender, RoutedEventArgs e1)
        {
            if (videoExtractor != null)
            {
                textBoxLocationExtraction.Text = GetExtractionLocation();
                buttonLocationExtractionOpen.IsEnabled = true;
                videoExtractor.BeginOrPause();
            }
        }

        #endregion

        #region Row Seven - Stop Extract

        private void ButtonExtractionStopClick(object sender, RoutedEventArgs e)
        {
            if (videoExtractor != null)
                videoExtractor.Stop();
        }

        #endregion

        #region Row Eight - Process Images

        private void ButtonImageProcessClick(object sender, RoutedEventArgs e)
        {
            buttonImageProcess.IsEnabled = false;
            buttonExtractionBeginPause.IsEnabled = false;
            buttonOcrBegin.IsEnabled = false;
            imageProcessor.StartProcess(sliderImageThreshold.Value);
        }

        #endregion

        #region Row Nine & Ten - OCR Range

        private void TextBoxNumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = InputHelper.IsInputNumbersOnly(e.Text);
        }

        private void TextBoxOcrValueLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    if (textBox.Name.Equals(textBoxOcrValueMin.Name))
                        opticalCharacterRecognition.Min = Convert.ToDouble(textBox.Text);
                    else if (textBox.Name.Equals(textBoxOcrValueMax.Name))
                        opticalCharacterRecognition.Max = Convert.ToDouble(textBox.Text);
                }
                else
                {
                    opticalCharacterRecognition.Min = null;
                    opticalCharacterRecognition.Max = null;
                }
            }
        }

        #endregion

        #region Row Eleven - OCR Begin

        private void ButtonOcrBeginClick(object sender, RoutedEventArgs e)
        {
            if (opticalCharacterRecognition != null)
            {
                opticalCharacterRecognition.Begin();
                buttonExtractionBeginPause.IsEnabled = false;
                buttonImageProcess.IsEnabled = false;
                buttonResultsShow.IsEnabled = false;
            }
        }

        #endregion

        #region Row Twelve - Results

        private void ButtonResultsShowClick(object sneder, RoutedEventArgs e)
        {
            ResultWindow resultWindow = new ResultWindow(frameDisplayHelper.Frames);
            resultWindow.Owner = this;
            resultWindow.ShowDialog();
        }

        #endregion

        #region Row Thirteen - Image Controls

        private void ButtonImageRandomClick(object sneder, RoutedEventArgs e)
        {
            frameDisplayHelper.GetRandom();
            UpdateImage();
        }

        private void SliderImageThresholdValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            imageBlackWhiteMode = true;
            labelImageThreshold.Content = string.Format(Properties.Resources.ImageThreshold, e.NewValue.ToString("0.##"));
            UpdateImage();
        }

        private void ButtonImagePreviousClick(object sneder, RoutedEventArgs e)
        {
            frameDisplayHelper.GetPrevious();
            UpdateImage();
        }

        private void ButtonImageNextClick(object sneder, RoutedEventArgs e)
        {
            frameDisplayHelper.GetNext();
            UpdateImage();
        }

        private void ButtonHelpClick(object sneder, RoutedEventArgs e)
        {
            ShowHelp();
        }

        #endregion

        #region UI Controls

        private void SetupDefaultControls()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory + @"Frames\";
            System.IO.Directory.CreateDirectory(directory);
            textBoxLocationExtraction.Watermark = directory;

            labelVideoTitle.Content = string.Format(Properties.Resources.VideoTitle, "N/A");
            labelVideoFPS.Content = string.Format(Properties.Resources.VideoFPS, 0);
            labelVideoWidthHeight.Content = string.Format(Properties.Resources.VideoResolution, 0, 0);
            labelVideoDuration.Content = string.Format(Properties.Resources.VideoDuration, 0);
            labelVideoSize.Content = string.Format(Properties.Resources.VideoSize, 0);

            labelFrameInterval.Content = string.Format(Properties.Resources.FrameInterval, "0");
            labelImageIndex.Content = string.Format(Properties.Resources.ImageIndex, "0", "0");
        }

        private void EnableImageProcessingControls(bool enabled)
        {
            buttonImageToggle.IsEnabled = enabled;
            buttonImageProcess.IsEnabled = enabled;
            labelImageThreshold.IsEnabled = enabled;
            sliderImageThreshold.IsEnabled = enabled;
        }

        private void EnableImageNavigationControls(bool enabled)
        {
            labelImageIndex.IsEnabled = enabled;
            buttonImageSkip.IsEnabled = enabled;
            buttonImageRandom.IsEnabled = enabled;
            buttonImagePevious.IsEnabled = enabled;
            buttonImageNext.IsEnabled = enabled;
        }

        private void EnableOcrControls(bool enabled)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                buttonOcrBegin.IsEnabled = enabled;
                textBoxOcrValueMin.IsEnabled = enabled;
                textBoxOcrValueMax.IsEnabled = enabled;

                opticalCharacterRecognition = new OpticalCharacterRecognition(frameDisplayHelper.Frames.ToArray());
                opticalCharacterRecognition.OnOcrProcessed += OpticalCharacterRecognition_OnProcessed;
                opticalCharacterRecognition.OnOcrCompleted += OpticalCharacterRecognition_OnCompleted;
            }));
        }

        #endregion

        #region Window Events

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                ShowHelp();
            }
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        #endregion

        #region Dialog
        private void ShowHelp()
        {
            MessageBox.Show(this, Properties.Resources.AppInstructions, Properties.Resources.AppTitleHelp, MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void OpenFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            try
            {
                Process.Start(path);
            }
            catch (Exception exception)
            {
                // The system cannot find the file specified...
                Debug.WriteLine(exception.Message);
                MessageBox.Show(Properties.Resources.AppTitleError, exception.Message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #endregion

        private string GetExtractionLocation()
        {
            return string.IsNullOrEmpty(textBoxLocationExtraction.Text) ? textBoxLocationExtraction.Watermark.ToString() : textBoxLocationExtraction.Text;
        }

        private void SetupImageProcessor(FrameData[] extractedFrames)
        {
            imageProcessor = new ImageProcessor(extractedFrames);
            imageProcessor.OnProcessed += ImageProcessor_OnProcessed;
            imageProcessor.OnCompleted += ImageProcessor_OnCompleted;
            imageProcessor.OnCriticalError += ImageProcessor_OnCriticalError;

            EnableImageProcessingControls(true);
            EnableImageNavigationControls(true);
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (frameDisplayHelper == null || imageProcessor == null)
                return;

            if (!imageBlackWhiteMode)
            {
                imageMain.Source = new ImageSourceConverter().ConvertFromString(frameDisplayHelper.GetCurrent().OriginalPath) as ImageSource;
            }
            else
            {
                imageMain.Source = ConversionHelper.ConvertToBitmapImage(imageProcessor.ProcessImage(frameDisplayHelper.GetCurrent(), Convert.ToSingle(sliderImageThreshold.Value), false));
            }

            labelImageIndex.Content = string.Format(Properties.Resources.ImageIndex, frameDisplayHelper.ImageIndex + 1, frameDisplayHelper.Frames.Count);
            UpdateSkipButton();
        }

        private void UpdateSkipButton()
        {
            if (frameDisplayHelper.GetCurrent().Skip)
                buttonImageSkip.Content = Properties.Resources.ImageInclude;
            else
                buttonImageSkip.Content = Properties.Resources.ImageSkip;
        }

        #region Video Extractor Events

        private void VideoExtractor_OnStateChanged(object source, EventArgsExtractionState eventArgs)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // Extraction state control logic
                if (eventArgs.GetCurrentState() == ExtractionState.Paused)
                {
                    buttonExtractionBeginPause.Content = Properties.Resources.ExtractionResume;
                }
                else if (eventArgs.GetCurrentState() == ExtractionState.Started && eventArgs.GetPreviousState() == ExtractionState.Stopped)
                {
                    buttonExtractionBeginPause.Content = Properties.Resources.ExtractionPause;
                    buttonExtractionStop.IsEnabled = true;
                }
                else if (eventArgs.GetCurrentState() == ExtractionState.Started)
                {
                    buttonExtractionBeginPause.Content = Properties.Resources.ExtractionPause;
                }
                else if (eventArgs.GetCurrentState() == ExtractionState.Stopped)
                {
                    buttonExtractionBeginPause.Content = Properties.Resources.ExtractionBegin;
                    buttonExtractionStop.IsEnabled = false;
                }
            }));
        }

        private void VideoExtractor_OnExtraction(object source, EventArgsFrameDataPercent eventArgs)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                labelAppInformation.Content = string.Format(Properties.Resources.AppInfoExtractingFrames, eventArgs.GetOverallPercentageComplete().ToString("0.##"));
                progressBarAppInformation.Value = eventArgs.GetOverallPercentageComplete();
            }));
        }

        private void VideoExtractor_OnCompleted(object source, EventArgsExtractionCompleted eventArgs)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                buttonExtractionStop.IsEnabled = false;

                if (!eventArgs.GetManuallyStopped())
                {
                    string extractionTimeSeconds = string.Format(Properties.Resources.AppInfoExtractingComplete, (eventArgs.GetTimeTakenMilliseconds() / 1000.0D).ToString("0.##"));
                    labelAppInformation.Content = extractionTimeSeconds;
                    progressBarAppInformation.Value = progressBarAppInformation.Maximum;
                    MessageBox.Show(extractionTimeSeconds, Properties.Resources.AppTitleFinished, MessageBoxButton.OK, MessageBoxImage.Information);

                    frameDisplayHelper = new FrameDisplayHelper(eventArgs.GetExtractedFrames());
                    SetupImageProcessor(eventArgs.GetExtractedFrames().ToArray());
                }
                else
                {
                    labelAppInformation.Content = string.Format(Properties.Resources.AppInfoExtractingStopped, eventArgs.GetPercentage().ToString("0.##"), (eventArgs.GetTimeTakenMilliseconds() / 1000.0D).ToString("0.##"));
                    progressBarAppInformation.Value = progressBarAppInformation.Minimum;
                }
            }));
        }

        #endregion

        #region Image Processor Events

        private void ImageProcessor_OnProcessed(object source, EventArgsFrameDataPercent eventArg)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                labelAppInformation.Content = string.Format(Properties.Resources.ProcessingFrames, eventArg.GetOverallPercentageComplete().ToString("0.##"));
                progressBarAppInformation.Value = eventArg.GetOverallPercentageComplete();
            }));
        }

        private void ImageProcessor_OnCompleted(object source, EventArgsSuccess eventArgs)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                buttonExtractionBeginPause.IsEnabled = true;
                buttonImageProcess.IsEnabled = true;
            }));

            if (eventArgs.GetSuccessful())
            {
                MessageBox.Show(Properties.Resources.AppInfoProcessingCompleted, Properties.Resources.AppTitleFinished, MessageBoxButton.OK, MessageBoxImage.Information);
                EnableOcrControls(true);
            }
        }

        private void ImageProcessor_OnCriticalError(object source, ErrorEventArgs errorEventArgs)
        {
            Debug.WriteLine(errorEventArgs.GetException().ToString());
            Close();
        }

        #endregion

        #region OCR Events

        public void OpticalCharacterRecognition_OnProcessed(object source, EventArgsFrameDataPercent eventArg)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                labelAppInformation.Content = string.Format(Properties.Resources.ProcessingOcr, eventArg.GetOverallPercentageComplete().ToString("0.##"));
                progressBarAppInformation.Value = eventArg.GetOverallPercentageComplete();
            }));
        }

        public void OpticalCharacterRecognition_OnCompleted(object source, EventArgsSuccessOcr eventArg)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(this, Properties.Resources.AppInfoOcrComplete, Properties.Resources.AppTitleFinished, MessageBoxButton.OK, MessageBoxImage.Information);

                if (eventArg.GetNeedsManualAssistance())
                {
                    UnprocessedFrameDialog unprocessedFrameDialog = new UnprocessedFrameDialog(frameDisplayHelper.Frames);
                    unprocessedFrameDialog.Owner = this;
                    unprocessedFrameDialog.ShowDialog();
                }

                buttonExtractionBeginPause.IsEnabled = true;
                buttonImageProcess.IsEnabled = true;
                buttonResultsShow.IsEnabled = true;
            }));
        }

        #endregion
    }
}