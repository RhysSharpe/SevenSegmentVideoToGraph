using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SevenSegmentVideoToGraph
{
    public class FrameDisplayHelper
    {
        protected static readonly Random random = new Random();

        public List<FrameData> Frames { get; private set; }
        protected int imageIndex;

        public FrameDisplayHelper()
        {
            Frames = new List<FrameData>();
        }

        public FrameDisplayHelper(List<FrameData> frames)
        {
            Frames = frames;
        }

        public virtual List<FrameData> FindFiles(string path)
        {
            string[] files = Directory.GetFiles(path, "*.jpeg", SearchOption.TopDirectoryOnly);
            Frames = new List<FrameData>();

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    string timestamp = Path.GetFileNameWithoutExtension(files[i]);
                    timestamp = Regex.Replace(timestamp, @"[^0-9.]", "");
                    Frames.Add(new FrameData(files[i], Convert.ToDouble(timestamp)));
                }
                catch (FormatException)
                {
                    return new List<FrameData>();
                }
            }

            Frames = Frames.OrderBy(p => p.Timestamp).ToList();
            return Frames;
        }

        public virtual void ToggleSkip()
        {
            if (Frames.Count > 0)
                Frames[imageIndex].Skip = !Frames[imageIndex].Skip;
        }

        public virtual FrameData GetNext()
        {
            if (Frames.Count > 0)
                return Frames[ImageIndex++];
            else
                return null;
        }

        public virtual FrameData GetPrevious()
        {
            if (Frames.Count > 0)
                return Frames[ImageIndex--];
            else
                return null;
        }

        public virtual FrameData GetRandom()
        {
            if (Frames.Count > 0)
            {
                ImageIndex = random.Next(0, Frames.Count);
                return Frames[imageIndex];
            }
            else
                return null;
        }

        public virtual FrameData GetCurrent()
        {
            if (Frames.Count > 0)
                return Frames[imageIndex];
            else
                return null;
        }

        public int ImageIndex
        {
            get
            {
                return imageIndex;
            }

            set
            {
                if (value >= Frames.Count)
                    imageIndex = 0;
                else if (value < 0)
                    imageIndex = Frames.Count - 1;
                else
                    imageIndex = value;
            }
        }
    }
}