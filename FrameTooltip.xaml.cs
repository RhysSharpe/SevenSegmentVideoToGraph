using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;

namespace SevenSegmentVideoToGraph
{
    public partial class FrameTooltip : IChartTooltip
    {
        private TooltipData data;
        public event PropertyChangedEventHandler PropertyChanged;

        public FrameTooltip()
        {
            InitializeComponent();
            DataContext = this;
        }

        public TooltipData Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged("Data");
            }
        }

        public TooltipSelectionMode? SelectionMode { get; set; }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}