using System.Text.RegularExpressions;

namespace SevenSegmentVideoToGraph
{
    public class InputHelper
    {
        private static Regex regexNumbers = new Regex("[^0-9.-]+");
        private static Regex regexNumbersPos = new Regex("[^0-9.]+");

        public static bool IsInputNumbersOnly(string text)
        {
            return regexNumbers.IsMatch(text);
        }
        public static bool IsInputPositiveNumbersOnly(string text)
        {
            return regexNumbersPos.IsMatch(text);
        }
    }
}