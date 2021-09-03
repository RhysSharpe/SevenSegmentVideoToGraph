using System.Windows.Controls;
using System.Windows;

namespace SevenSegmentVideoToGraph
{
    /// <summary>
    /// Class <c>ComboBoxItemValue</c> is the <c>ComboBoxItem</c> that allows a user defined input for the OCR correction phase.
    /// </summary>
    public class ComboBoxItemValue : ComboBoxItem
    {
        protected UnprocessedFrameDisplayHelper unprocessedFrameDisplayHelper;

        public ComboBoxItemValue(string name, string content)
        {
            Name = name;
            FontSize = 14;
            Content = content;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
        }

        protected ComboBoxItemValue(UnprocessedFrameDisplayHelper unprocessedFrameDisplayHelper, string name, string content) : this(name, content)
        {
            this.unprocessedFrameDisplayHelper = unprocessedFrameDisplayHelper;
        }

        public virtual double? Calculate()
        {
            return null;
        }
    }
}