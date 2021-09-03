using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SevenSegmentVideoToGraph
{
    public class BrowserDialog
    {
        public static string GetFolderPath()
        {
            return GetFolderPath(string.Empty);
        }

        public static string GetFolderPath(string defaultValue)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                return commonOpenFileDialog.FileName + @"\";
            else
                return defaultValue;
        }

        public static OpenFileDialog GetOpenFileDialog(string title, string defaultExtension, string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = title,

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = defaultExtension,
                Filter = filter,
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            return openFileDialog;
        }

        public static OpenFileDialog GetMP4File()
        {
            return GetOpenFileDialog(Properties.Resources.FileBrowseVideo, "mp4", "MP4 files (*.mp4)|*.mp4");
        }

        public static SaveFileDialog GetSaveFileDialog(string defaultName, string filter)
        {
            SaveFileDialog savefileDialog = new SaveFileDialog();
            savefileDialog.FileName = defaultName;
            savefileDialog.Filter = filter;

            return savefileDialog;
        }

        public static SaveFileDialog GetSaveFileDialogCsv()
        {
            return GetSaveFileDialog(Properties.Resources.FileSaveCsv, "CSV files (*.csv)|*.csv|All files (*.*)|*.*");
        }

        public static SaveFileDialog GetSaveFileDialogPng()
        {
            return GetSaveFileDialog(Properties.Resources.FileSaveGraph, "PNG files (*.png)|*.png|All files (*.*)|*.*");
        }
    }
}