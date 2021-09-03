namespace SevenSegmentVideoToGraph
{
    /// <summary>
    /// Class <c>ComboBoxItemValuePrevious</c> is a child of <c>ComboBoxItemValue</c>
    /// that uses the previous frames' value for the OCR correction phase.
    /// </summary>
    public class ComboBoxItemValuePrevious : ComboBoxItemValue
    {
        public ComboBoxItemValuePrevious(UnprocessedFrameDisplayHelper frameDisplayHelper)
            : base(frameDisplayHelper, "comboBoxItemPrevious", Properties.Resources.UnprocessedFramePrevious)
        {
            Calculate();
        }

        public override double? Calculate()
        {
            if (unprocessedFrameDisplayHelper.ImageIndex > 0)
            {
                // This shouldn't be null because the user should have entered a value for it if needed, but just in case
                if (unprocessedFrameDisplayHelper.Frames[unprocessedFrameDisplayHelper.ImageIndex - 1].Value != null)
                {
                    IsEnabled = true;
                    return unprocessedFrameDisplayHelper.Frames[unprocessedFrameDisplayHelper.ImageIndex - 1].Value;
                }
            }

            IsEnabled = false;
            return base.Calculate();
        }
    }
}