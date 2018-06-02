using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BSK_Encryption.ViewModels
{
    /// <summary>
    /// Context for Cryptography.
    /// </summary>
    public abstract class DataViewModel : NotifyPropertyChanged
    {
        #region Field
        private string inputPath;
        private string outputPath;
        private double progress;
        private bool isNotRunning;
        #endregion

        #region Properties
        /// <summary>
        /// Input file path.
        /// </summary>
        public string InputPath
        {
            get { return inputPath; }
            set
            {
                inputPath = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Output file path.
        /// </summary>
        public string OutputPath
        {
            get { return outputPath; }
            set
            {
                outputPath = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Progress of cryptography operation.
        /// </summary>
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
        /// <summary>
        /// Indicates if process is currently running.
        /// </summary>
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
