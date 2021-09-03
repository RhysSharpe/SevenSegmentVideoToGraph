namespace SevenSegmentVideoToGraph
{
    public class EventArgsFrameDataPercent : EventArgsFrameData
    {
        private double overallPercentageComplete;
        
        public EventArgsFrameDataPercent(FrameData extractedFrame, double overallPercentageComplete) : base(extractedFrame)
        {
            this.overallPercentageComplete = overallPercentageComplete;
        }

        public double GetOverallPercentageComplete()
        {
            return overallPercentageComplete;
        }
    }
}