namespace SevenSegmentVideoToGraph
{

    /// <summary>
    /// Class <c>ComboBoxItemValueAverage</c> is a child of <c>ComboBoxItemValue</c>
    /// that calculates the average value of the neighbouring frames during the OCR correction phase.
    /// </summary>
    public class ComboBoxItemValueAverage : ComboBoxItemValue
    {
        public ComboBoxItemValueAverage(UnprocessedFrameDisplayHelper frameDisplayHelper)
            : base(frameDisplayHelper, "comboBoxItemAverage", Properties.Resources.UnprocessedFrameAverage)
        {
            Calculate();
        }

        public override double? Calculate()
        {
            if (unprocessedFrameDisplayHelper.ImageIndex < unprocessedFrameDisplayHelper.Frames.Count - 1 && unprocessedFrameDisplayHelper.ImageIndex > 0)
            {
                double? previous = unprocessedFrameDisplayHelper.Frames[unprocessedFrameDisplayHelper.ImageIndex - 1].Value;
                double? next = unprocessedFrameDisplayHelper.Frames[unprocessedFrameDisplayHelper.ImageIndex + 1].Value;
                if (previous != null && next != null)
                {
                    IsEnabled = true;
                    return (previous + next) / 2.0D;
                }
            }

            IsEnabled = false;
            return base.Calculate();
        }
    }
}