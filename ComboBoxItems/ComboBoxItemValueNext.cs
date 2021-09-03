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
            double? returnValue = null;
            int index = unprocessedFrameDisplayHelper.ImageIndex;

            while (!returnValue.HasValue)
            {
                if (index < unprocessedFrameDisplayHelper.Frames.Count - 1)
                {
                    index++;
                    FrameData frame = unprocessedFrameDisplayHelper.Frames[index];
                    if (frame.Value != null && !frame.IsOutsideRange())
                    {
                        IsEnabled = true;
                        returnValue = frame.Value;
                    }
                }
                else
                {
                    IsEnabled = false;
                    return base.Calculate();
                }
            }

            return returnValue;
        }
    }
}