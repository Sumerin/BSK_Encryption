using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BSK_Encryption.ViewModels
{

    public abstract class DataViewModel : INotifyPropertyChanged
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

        #region Notify Interface
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
