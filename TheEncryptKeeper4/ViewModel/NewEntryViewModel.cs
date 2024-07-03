namespace TheEncryptKeeper4.ViewModel
{
    using System.Windows.Input;
    using TheEncryptKeeper4.Common;
    using TheEncryptKeeper4.Models;
    using TheEncryptKeeper4.Services;

    public class NewEntryViewModel : BaseViewModel
    {
        private readonly IJSONService jsonService;

        private const string FileName = @"PWSheet.JSON";

        public LoginEntry LoginModel { get; set; } = new LoginEntry();

        private string resultMessage;
        public string ResultMessage
        {
            get => resultMessage;
            set
            {
                resultMessage = value;
                OnPropertyChanged(nameof(ResultMessage));
            }
        }
        
        public ICommand SaveNewEntryCommand { get; private set; }
        public ICommand ClearInputCommand { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        public NewEntryViewModel(IJSONService jsonService)
        {
            this.jsonService = jsonService;

            SaveNewEntryCommand = new RelayCommand(SaveNewEntry);
            ClearInputCommand = new RelayCommand(ClearInputs);

            ClearMessage();
        }

        public void SaveNewEntry(object parameter)
        {
            bool result = jsonService.SaveNewEntry(LoginModel);
            if (result)
            {
                ResultMessage = "New Entry Saved";
                LoginModel.ClearValues();
            }
            else
            {
                ResultMessage = "An Error occurred when saving the new entry";
            }

        }

        public void ClearInputs(object parameter)
        {
            LoginModel.ClearValues();
            ResultMessage = "Input cleared";
        }

        public void ClearMessage()
        {
            ResultMessage = string.Empty;
        }
    }
}