using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BSK_Encryption.ViewModels
{

    public abstract class DataViewModel : NotifyPropertyChanged
    {
        #region Field
        private string inputPath;
        private string outputPath;
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
        #endregion
    }
}
