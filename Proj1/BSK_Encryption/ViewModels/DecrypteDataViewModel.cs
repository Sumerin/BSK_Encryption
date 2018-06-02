namespace BSK_Encryption.ViewModels
{
    /// <summary>
    /// Decrypt context model.
    /// </summary>
    public class DecrypteDataViewModel : DataViewModel
    {
        private string user;

        /// <summary>
        /// Username given from gui.
        /// </summary>
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }
    }
}
