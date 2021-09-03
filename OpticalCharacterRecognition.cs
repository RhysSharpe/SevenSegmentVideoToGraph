using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Tesseract;

namespace SevenSegmentVideoToGraph
{
    public delegate void OnOcrProcessed(object source, EventArgsFrameDataPercent eventArgs);
    public delegate void OnOcrCompleted(object source, EventArgsSuccessOcr eventArgs);

    public class OpticalCharacterRecognition
    {
        public OnOcrProcessed OnOcrProcessed;
        public OnOcrCompleted OnOcrCompleted;

        private FrameData[] frames;
        public double? Min, Max;
        private bool needsManualAssistance;

        // Credit
        // https://slavik.meltser.info/validate-number-with-regular-expression/
        // https://stackoverflow.com/questions/2811031/decimal-or-numeric-values-in-regular-expression-validation
        private static readonly Regex numberFilter = new Regex(@"/^-?(0|[1-9]\d*)(\.\d+)?$/");

        public OpticalCharacterRecognition(FrameData[] frames)
        {
            this.frames = frames;
        }

        public void Begin()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.Name = "OpticalCharacterRecognitionThread";
                Thread.CurrentThread.IsBackground = true;

                try
                {
                    using (var engine = new TesseractEngine(@"Tesseract\letsgodigital", "letsgodigital", EngineMode.Default))
                    {
                        for (int i = 0; i < frames.Length; i++)
                        {
                            using (Pix frame = Pix.LoadFromFile(frames[i].ProcessedPath))
                            {
                                using (Page page = engine.Process(frame))
                                {
                                    string interpreted = page.GetText();
                                    interpreted = interpreted.Replace(" ", "");

                                    //Debug.WriteLine(string.Format("Mean confidence: {0}", page.GetMeanConfidence()));
                                    //Debug.WriteLine(string.Format("Text (GetText): {0}", interpreted));

                                    Match match = numberFilter.Match(interpreted);
                                    interpreted = Regex.Replace(page.GetText(), "[^.0-9]", "");

                                    // Check number of '.' in string as multiple is not allowed and the filter doesn't catch it
                                    int dotCount = interpreted.Count(f => f == '.');

                                    if (dotCount > 1)
                                    {
                                        if (interpreted.EndsWith(".") && dotCount == 2)
                                            interpreted = interpreted.Substring(interpreted.Length - 2);
                                    }

                                    //Debug.WriteLine(string.Format("Text (GetText): {0}", interpreted));

                                    double output;
                                    if (double.TryParse(interpreted, out output))
                                    {
                                        frames[i].Value = output;
                                        frames[i].OcrConfidence = page.GetMeanConfidence();
                                    }

                                    // Save the Min and Max to the frame so that it uses the original range values
                                    // (in case the users changes the range after)
                                    frames[i].OcrMin = Min;
                                    frames[i].OcrMax = Max;

                                    double percentageDone = (i + 1.0D) / frames.Length * 100.0D;
                                    OnOcrProcessed(this, new EventArgsFrameDataPercent(frames[i], percentageDone));
                                }
                            }

                            if (frames[i].IsOutsideRange() || frames[i].Skip)
                                needsManualAssistance = true;
                        }
                    }

                    OnOcrCompleted(this, new EventArgsSuccessOcr(true, needsManualAssistance));
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    Console.WriteLine("Unexpected Error: " + ex.Message);
                    Console.WriteLine("Details: ");
                    Console.WriteLine(ex.ToString());
                }
            }).Start();
        }
    }
}