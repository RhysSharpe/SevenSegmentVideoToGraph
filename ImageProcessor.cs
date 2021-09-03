using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;

namespace SevenSegmentVideoToGraph
{
    // Learning creating an event
    // https://stackoverflow.com/questions/623451/how-can-i-make-my-own-event-in-c
    // Define a delegate that acts as a signature for the function that is called when the event is triggered.
    // The second parameter is of MyEventArgs type; this object will contain information about the triggered event.
    public delegate void OnImageProcessed(object source, EventArgsFrameDataPercent eventArgs);
    public delegate void OnImageProcessCompleted(object source, EventArgsSuccess eventArgs);
    public delegate void OnCriticalError(object source, ErrorEventArgs errorEventArgs);
    public class ImageProcessor
    {
        public event OnImageProcessed OnProcessed;
        public event OnImageProcessCompleted OnCompleted;
        public event OnCriticalError OnCriticalError;

        private FrameData[] frames;
        private readonly static float[][] grayMatrix = new float[][]
        {
            new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
            new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
            new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
            new float[] { 0,      0,      0,      1, 0 },
            new float[] { 0,      0,      0,      0, 1 }
        };

        public ImageProcessor(FrameData[] frames)
        {
            this.frames = frames;
        }

        public void StartProcess(double value)
        {
            StartProcess(Convert.ToSingle(value));
        }

        public void StartProcess(float threshold)
        {
            if (frames == null)
            {
                Debug.WriteLine("The 'frames' list is null!");
                MessageBox.Show(Properties.Resources.ProcessingError, Properties.Resources.AppTitleError, MessageBoxButton.OK, MessageBoxImage.Error);
                OnCompleted(this, new EventArgsSuccess(false));
                return;
            }

            new Thread(() =>
            {
                Thread.CurrentThread.Name = "ImageProcessingThread";
                Thread.CurrentThread.IsBackground = true;

                for (int i = 0; i < frames.Length; i++)
                {
                    ProcessImage(frames[i], threshold, true);
                    double percentageDone = (i + 1.0D) / frames.Length * 100.0D;
                    OnProcessed.Invoke(this, new EventArgsFrameDataPercent(frames[i], percentageDone));
                }

                OnCompleted(this, new EventArgsSuccess(true));

            }).Start();
        }

        // Can be used to display main application image which shouldn't be saved
        public Bitmap ProcessImage(FrameData frame, float threshold, bool shouldSave)
        {
            try
            {
                Bitmap sourceImage = new Bitmap(frame.OriginalPath);

                using (Graphics graphics = Graphics.FromImage(sourceImage))
                {
                    ImageAttributes imageAttributes = new ImageAttributes();
                    imageAttributes.SetColorMatrix(new ColorMatrix(grayMatrix));
                    imageAttributes.SetThreshold(threshold);
                    Rectangle bounds = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                    graphics.DrawImage(sourceImage, bounds, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, imageAttributes);

                    if (shouldSave)
                    {
                        StringBuilder stringBuilder = new StringBuilder(Path.GetDirectoryName(frame.OriginalPath));
                        stringBuilder.Append("\\processed\\");

                        Directory.CreateDirectory(stringBuilder.ToString());

                        stringBuilder.Append(Path.GetFileNameWithoutExtension(frame.OriginalPath));
                        stringBuilder.Append(".jpeg");

                        frame.ProcessedPath = stringBuilder.ToString();
                        sourceImage.Save(stringBuilder.ToString(), ImageFormat.Jpeg);
                    }
                }

                return sourceImage;
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(Properties.Resources.AppInfoMissingFiles, Properties.Resources.AppTitleError, MessageBoxButton.OK, MessageBoxImage.Error);
                OnCriticalError(this, new ErrorEventArgs(argEx));
                return new Bitmap(1, 1);
            }
        }
    }
}