using System.IO;

namespace SevenSegmentVideoToGraph
{
    public class FrameData
    {
        public string OriginalPath { get; private set; }
        public string ProcessedPath { get; set; }
        public double Timestamp { get; set; }
        public double? Value { get; set; }
        public double? OcrMin { get; set; }
        public double? OcrMax { get; set; }
        public bool Skip { get; set; }
        public float? OcrConfidence { get; set; }
        public string Filename { get; set; }

        public FrameData(string originalPath)
        {
            this.OriginalPath = originalPath;
            Filename = Path.GetFileName(originalPath);
        }

        public FrameData(string originalPath, double timestamp) : this(originalPath)
        {
            Timestamp = timestamp;
        }

        public bool IsOutsideRange()
        {
            if (OcrMin == null || OcrMax == null || GetOcrFailed())
                return false;
            else
                return Value > OcrMax || Value < OcrMin;
        }

        public bool GetOcrFailed()
        {
            return Value == null;
        }
    }
}