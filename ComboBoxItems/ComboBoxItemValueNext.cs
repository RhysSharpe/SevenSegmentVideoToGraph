namespace SevenSegmentVideoToGraph
{
    /// <summary>
    /// Class <c>ComboBoxItemValueNext</c> is a child of <c>ComboBoxItemValue</c>
    /// that uses the next frames' value for the OCR correction phase.
    /// </summary>
    public class ComboBoxItemValueNext : ComboBoxItemValue
    {
        public ComboBoxItemValueNext(UnprocessedFrameDisplayHelper frameDisplayHelper)
            : base(frameDisplayHelper, "comboBoxItemNext", Properties.Resources.UnprocessedFrameNext)
        {
            Calculate();
        }

        public override double? Calculate()
        {
            if (unprocessedFrameDisplayHelper.ImageIndex < unprocessedFrameDisplayHelper.Frames.Count - 1)
            {
                if (unprocessedFrameDisplayHelper.Frames[unprocessedFrameDisplayHelper.ImageIndex + 1].Value != null)
                {
                    IsEnabled = true;
                    return unprocessedFrameDisplayHelper.Frames[unprocessedFrameDisplayHelper.ImageIndex + 1].Value;
                }
            }

            IsEnabled = false;
            return base.Calculate();
        }
    }
}