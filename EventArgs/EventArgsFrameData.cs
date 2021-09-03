using System;

namespace SevenSegmentVideoToGraph
{
    // Learning creating event
    // https://stackoverflow.com/questions/623451/how-can-i-make-my-own-event-in-c
    // This class describes the event to the class that receives it, it must always derive from System.EventArgs.
    public class EventArgsFrameData : EventArgs
    {
        protected FrameData extractedFrame;

        public EventArgsFrameData(FrameData extractedFrame)
        {
            this.extractedFrame = extractedFrame;
        }

        public FrameData GetExtractedFrame()
        {
            return extractedFrame;
        }
    }
}