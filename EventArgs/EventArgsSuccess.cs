using System;

namespace SevenSegmentVideoToGraph
{
    public class EventArgsSuccess : EventArgs
    {
        protected bool success;

        public EventArgsSuccess(bool success)
        {
            this.success = success;
        }

        public bool GetSuccessful()
        {
            return success;
        }
    }
}