using System;

namespace SevenSegmentVideoToGraph
{
    public class EventArgsExtractionState : EventArgs
    {
        private ExtractionState extractionStatePrevious;
        private ExtractionState extractionStateCurrent;

        public EventArgsExtractionState(ExtractionState extractionStateCurrent, ExtractionState extractionStatePrevious)
        {
            this.extractionStateCurrent = extractionStateCurrent;
            this.extractionStatePrevious = extractionStatePrevious;
        }

        public ExtractionState GetCurrentState()
        {
            return extractionStateCurrent;
        }
        public ExtractionState GetPreviousState()
        {
            return extractionStatePrevious;
        }
    }
}