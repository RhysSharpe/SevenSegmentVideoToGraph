using System.Collections.Generic;

namespace SevenSegmentVideoToGraph
{
    /// <summary>
    /// <c>UnprocessedFrameDisplayHelper</c> is an extension of <c>FrameDisplayHelper</c>
    /// that only handles frames without an OCR value or frames that are out of bounds.
    /// </summary>
    public class UnprocessedFrameDisplayHelper : FrameDisplayHelper
    {
        public int[] UnprocessedFramesIndex { get; private set; }
        private int unprocessedIndex;

        public UnprocessedFrameDisplayHelper(List<FrameData> frames, int[] unprocessedFramesIndex) : base(frames)
        {
            UnprocessedFramesIndex = unprocessedFramesIndex;
            ImageIndex = unprocessedFramesIndex[UnprocessedIndex];
        }

        public override FrameData GetNext()
        {
            if (Frames.Count > 0 && UnprocessedFramesIndex.Length > 0)
            {
                UnprocessedIndex++;
                ImageIndex = UnprocessedFramesIndex[UnprocessedIndex];
                return Frames[ImageIndex];
            }
            else
                return null;
        }

        public override FrameData GetPrevious()
        {
            if (Frames.Count > 0 && UnprocessedFramesIndex.Length > 0)
            {
                UnprocessedIndex--;
                ImageIndex = UnprocessedFramesIndex[UnprocessedIndex];
                return Frames[ImageIndex];
            }
            else
                return null;
        }

        public override FrameData GetRandom()
        {
            if (Frames.Count > 0 && UnprocessedFramesIndex.Length > 0)
            {
                UnprocessedIndex = random.Next(0, UnprocessedFramesIndex.Length);
                ImageIndex = UnprocessedFramesIndex[UnprocessedIndex];
                return Frames[ImageIndex];
            }
            else
                return null;
        }

        public override FrameData GetCurrent()
        {
            if (Frames.Count > 0 && UnprocessedFramesIndex.Length > 0)
            {
                imageIndex = UnprocessedFramesIndex[UnprocessedIndex];
                return Frames[imageIndex];
            }
            else
                return null;
        }

        public int UnprocessedIndex
        {
            get
            {
                return unprocessedIndex;
            }

            set
            {
                if (value >= UnprocessedFramesIndex.Length)
                    unprocessedIndex = 0;
                else if (value < 0)
                    unprocessedIndex = UnprocessedFramesIndex.Length - 1;
                else
                    unprocessedIndex = value;
            }
        }
    }
}