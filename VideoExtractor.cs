using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;

namespace SevenSegmentVideoToGraph
{
    public delegate void OnStateChanged(object source, EventArgsExtractionState eventArgs);
    public delegate void OnExtraction(object source, EventArgsFrameDataPercent eventArgs);
    public delegate void OnExtractionCompleted(object source, EventArgsExtractionCompleted eventArgs);

    public class VideoExtractor
    {
        public event OnStateChanged OnStateChanged;
        public event OnExtraction OnExtraction;
        public event OnExtractionCompleted OnCompleted;

        private ExtractionState extractionState;
        private List<FrameData> frames;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly ManualResetEvent manualResetEvent = new ManualResetEvent(true);

        public string ExtractionPath { get; set; }
        public MediaFile Video { get; set; }
        public double FrameTime { get; set; }

        public VideoExtractor(string extractionPath, MediaFile video, double frameTime)
        {
            ExtractionPath = extractionPath;
            Video = video;
            FrameTime = frameTime;
            extractionState = ExtractionState.Stopped;
        }

        public void BeginOrPause()
        {
            if (Video == null)
            {
                Debug.WriteLine("Error! VideoExtraction 'video' is null! This means it was instantiated without a valid video.");
                MessageBox.Show(Properties.Resources.ExtractionBeginError, Properties.Resources.AppTitleError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (extractionState)
            {
                case ExtractionState.Started:

                    manualResetEvent.Reset();
                    stopwatch.Stop();
                    extractionState = ExtractionState.Paused;
                    OnStateChanged(this, new EventArgsExtractionState(extractionState, ExtractionState.Started));
                    break;

                case ExtractionState.Paused:

                    manualResetEvent.Set();
                    stopwatch.Start();
                    extractionState = ExtractionState.Started;
                    OnStateChanged(this, new EventArgsExtractionState(extractionState, ExtractionState.Paused));
                    break;

                case ExtractionState.Stopped:

                    if (string.IsNullOrEmpty(ExtractionPath))
                        throw new ArgumentNullException("extractionPath", "Error! The 'extractionPath' field must be set first.");

                    if (Path.IsPathRooted(ExtractionPath))
                    {
                        extractionState = ExtractionState.Started;
                        OnStateChanged(this, new EventArgsExtractionState(extractionState, ExtractionState.Stopped));
                        ExtractFrames();
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.ExtractionLocationError, Properties.Resources.AppTitleError, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
            }
        }

        public void Stop()
        {
            ExtractionState extractionStatePrev = extractionState;

            // If it is paused, resume it so it can end
            if (extractionState == ExtractionState.Paused)
                manualResetEvent.Set();

            extractionState = ExtractionState.Stopped;
            OnStateChanged(this, new EventArgsExtractionState(extractionState, extractionStatePrev));
        }

        private void ExtractFrames()
        {
            // Reset list
            frames = new List<FrameData>();
            double percentageComplete = 0.0D;

            new Thread(() =>
            {
                Thread.CurrentThread.Name = "FrameExtractionThread";
                Thread.CurrentThread.IsBackground = true;

                using (Engine engine = new Engine())
                {
                    engine.GetMetadata(Video);

                    // Calculate frame time after getting video metadata
                    if (FrameTime <= 0.0D)
                        FrameTime = 1.0D / Video.Metadata.VideoData.Fps;

                    double videoSeekMilliseconds = 0.0D;
                    double videoTotalMilliseconds = Video.Metadata.Duration.TotalMilliseconds;
                    int index = 0;

                    stopwatch.Start();

                    while (videoSeekMilliseconds < videoTotalMilliseconds)
                    {
                        ConversionOptions options = new ConversionOptions { Seek = TimeSpan.FromMilliseconds(videoSeekMilliseconds) };
                        MediaFile outputFile = new MediaFile(string.Format("{0}frame-{1}.jpeg", ExtractionPath, videoSeekMilliseconds.ToString("0.####")));
                        engine.GetThumbnail(Video, outputFile, options);

                        FrameData extractedFrame = new FrameData(outputFile.Filename, videoSeekMilliseconds);
                        frames.Add(extractedFrame);

                        percentageComplete = videoSeekMilliseconds / videoTotalMilliseconds * 100.0D;
                        OnExtraction(this, new EventArgsFrameDataPercent(extractedFrame, percentageComplete));
                        videoSeekMilliseconds += FrameTime * 1000.0D;
                        index++;

                        manualResetEvent.WaitOne();

                        if (extractionState == ExtractionState.Stopped)
                            break;
                    }

                    // Sometimes the seek time will be too close to the end and a frame won't be extracted
                    // This code removes the frame data object that has a false path
                    if (!File.Exists(frames[frames.Count - 1].OriginalPath))
                        frames.RemoveAt(frames.Count - 1);

                    stopwatch.Stop();
                }

                // percentageComplete might not be 100 even without stopping due to frameTime not fitting exactly
                OnCompleted(this, new EventArgsExtractionCompleted(frames, stopwatch.ElapsedMilliseconds, extractionState == ExtractionState.Stopped ? percentageComplete : 100.0D));

                ExtractionState extractionStatePrev = extractionState;
                extractionState = ExtractionState.Stopped;
                OnStateChanged(this, new EventArgsExtractionState(extractionState, extractionStatePrev));
            }).Start();
        }
    }

    public enum ExtractionState
    {
        Started,
        Paused,
        Stopped
    }
}