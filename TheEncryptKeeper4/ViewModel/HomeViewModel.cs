using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheEncryptKeeper4.Common;
using TheEncryptKeeper4.DataAccess.Entities;
using TheEncryptKeeper4.Models;
using TheEncryptKeeper4.Services;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

/// <summary>
/// This is the homeViewModel the home view will contain the metro tabs used to swap the views of new enty
/// and the browsing grid. so we will want to hold references to the view models
/// </summary>

namespace TheEncryptKeeper4.ViewModel
{
    public class HomeViewModel: BaseViewModel
    {
        private IDialogCoordinator controller;
        private IJSONService jsonService;

        private object selectedVM;
        public object SelectedVM
        {
            get => selectedVM;
            set
            {
                selectedVM = value;
                OnPropertyChanged(nameof(SelectedVM));
            }
        }


        private readonly NewEntryViewModel entryVM;       

        private readonly ManageLoginEntryViewModel manageVM;

        public ICommand SelectEntryViewCommand { get; private set; }
        public ICommand SelectManageViewCommand { get; private set; }


        public HomeViewModel(
            IDialogCoordinator controller,
            IJSONService jsonService,
            NewEntryViewModel eVM, 
            ManageLoginEntryViewModel mVM)
        {
            Console.WriteLine("HomeViewModel is starting up");
            this.controller = controller;
            this.jsonService = jsonService;

            entryVM = eVM;
            manageVM = mVM;

            SelectedVM = manageVM;

            SelectEntryViewCommand = new RelayCommand(SelectEntryView);
            SelectManageViewCommand = new RelayCommand(SelectManageView);
            
        }

        public async Task StartUpPinCheck(MetroWindow context)
        {
            Console.WriteLine("Start up pin");

            //use the json service to check for a made pin
            string savedPin = jsonService.LoadJSONPin();
            string pinInput = "";
            Console.WriteLine(savedPin);
            //if no pin exists then user's input will be new pin
            //if pin exists then loop til input is valid
            if (savedPin == string.Empty)
            {
                pinInput = await context.ShowInputAsync("User Pin", "Enter New Pin");
                jsonService.SaveJSONPin(pinInput);
            }
            else
            {
                var settings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Enter",
                    NegativeButtonText = "Close",
                    AnimateShow = true,
                    AnimateHide = false
                };

                while (pinInput != savedPin)
                {
                    var result = await context.ShowInputAsync("User Pin", "Enter Pin", settings);
                    
                    if (result == null)
                    {
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        Console.WriteLine(result);
                        pinInput = result;
                    }
                }
            }
        }

        public void SelectEntryView(object parameter)
        {
            SelectedVM = entryVM;
        }

        public void SelectManageView(object parameter)
        {
            SelectedVM = manageVM;
        }
    }
}
