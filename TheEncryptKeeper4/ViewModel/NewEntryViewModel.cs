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
        }

        public void SaveNewEntry(object parameter)
        {
            jsonService.SaveNewEntry(LoginModel);

            LoginModel.ClearValues();
        }

        public void ClearInputs(object parameter)
        {
            LoginModel.ClearValues();
        }
                
    }
}
