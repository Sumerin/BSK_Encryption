namespace BSK_Encryption.ViewModels
{
    public class DecrypteDataViewModel : DataViewModel
    {
        private string user;

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
