using System;
using System.Collections.Generic;

namespace SevenSegmentVideoToGraph
{
    public class EventArgsExtractionCompleted : EventArgs
    {
        private List<FrameData> extractedFrames;
        private long elapsedMilliseconds;
        private double percentageCompleted;

        public EventArgsExtractionCompleted(List<FrameData> extractedFrames, long elapsedMilliseconds, double percentageCompleted)
        {
            this.extractedFrames = extractedFrames;
            this.elapsedMilliseconds = elapsedMilliseconds;
            this.percentageCompleted = percentageCompleted;
        }

        public List<FrameData> GetExtractedFrames()
        {
            return extractedFrames;
        }

        public long GetTimeTakenMilliseconds()
        {
            return elapsedMilliseconds;
        }

        public bool GetManuallyStopped()
        {
            return percentageCompleted < 100.0D;
        }

        public double GetPercentage()
        {
            return percentageCompleted;
        }
    }
}