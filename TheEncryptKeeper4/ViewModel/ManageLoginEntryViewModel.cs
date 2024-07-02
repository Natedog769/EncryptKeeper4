using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheEncryptKeeper4.Common;
using TheEncryptKeeper4.Models;
using TheEncryptKeeper4.Services;

namespace TheEncryptKeeper4.ViewModel
{
    public class ManageLoginEntryViewModel : BaseViewModel
    {
        private IJSONService jsonService;

        private IList<LoginEntry> logins;
        public IList<LoginEntry> LoginList
        {
            get => logins;
            set
            {
                logins = value;
                OnPropertyChanged(nameof(LoginList));
            }
        }

        private IList<LoginEntry> displayedLogins;
        public IList<LoginEntry> DisplayedLogins
        {
            get => displayedLogins;
            set
            {
                displayedLogins = value;
                OnPropertyChanged(nameof(displayedLogins));
            }
        }

        private string textBoxSearchInput;
        public string TextBoxSearchInput
        {
            get => textBoxSearchInput;
            set
            {
                textBoxSearchInput = value;
                OnPropertyChanged(nameof(TextBoxSearchInput));
                GetDisplayedList();
            }
        }

        public ICommand LoadLoginListCommand { get; private set; }
        public ICommand DeleteEntryCommand { get; private set; }

        public ManageLoginEntryViewModel(IJSONService jsonService)
        {
            this.jsonService = jsonService;

            TextBoxSearchInput = string.Empty;
            DisplayedLogins = new List<LoginEntry>();

            GetDisplayedList();

            LoadLoginListCommand = new RelayCommand(LoadList);
            DeleteEntryCommand = new RelayCommand(DeleteEntries);
        }

        public void LoadList(object parameter = null)
        {
            LoginList = jsonService.GetLoginList();
        }

        public void GetDisplayedList()
        {
            LoadList();
            if (TextBoxSearchInput.Length == 0)
            {
                DisplayedLogins = LoginList;
            }
            else
            {
                DisplayedLogins = LoginList.Where(login => 
                login.Website.ToUpper().Contains(TextBoxSearchInput.ToUpper())).ToList();
            }
        }

        public void DeleteEntries(object parameter = null)
        {
            List<LoginEntry> indexForDelete = new List<LoginEntry>();

            for (int i = 0; i < LoginList.Count; i++)
            {
                if (LoginList[i].IsSelected)
                {
                    indexForDelete.Add(LoginList[i]);
                }
            }

            foreach (LoginEntry i in indexForDelete)
            {
                LoginList.Remove(i);
            }

            jsonService.SaveChangedList(LoginList);
            LoadList();
        }

    }
}
