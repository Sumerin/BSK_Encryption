using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BSK_Encryption.ViewModels
{

    public abstract class DataViewModel : NotifyPropertyChanged
    {
        #region Field
        private string inputPath;
        private string outputPath;
        private double progress;
        private bool isNotRunning;
        #endregion

        #region Properties
        public string InputPath
        {
            get { return inputPath; }
            set
            {
                inputPath = value;
                OnPropertyChanged();
            }
        }
        public string OutputPath
        {
            get { return outputPath; }
            set
            {
                outputPath = value;
                OnPropertyChanged();
            }
        }
        public double Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }
        public bool IsNotRunning
        {
            get
            {
                return isNotRunning;
            }
            set
            {
                isNotRunning = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
