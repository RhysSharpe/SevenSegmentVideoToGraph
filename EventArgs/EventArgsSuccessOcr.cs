namespace SevenSegmentVideoToGraph
{
    public class EventArgsSuccessOcr : EventArgsSuccess
    {
        private bool needsManualAssistance;

        public EventArgsSuccessOcr(bool success, bool needsManualAssistance) : base(success)
        {
            this.needsManualAssistance = needsManualAssistance;
        }

        public bool GetNeedsManualAssistance()
        {
            return needsManualAssistance;
        }
    }
}