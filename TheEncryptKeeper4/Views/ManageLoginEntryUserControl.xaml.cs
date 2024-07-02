using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheEncryptKeeper4.ViewModel;

namespace TheEncryptKeeper4.Views
{
    /// <summary>
    /// Interaction logic for ManageLoginEntryUserControl.xaml
    /// </summary>
    public partial class ManageLoginEntryUserControl : UserControl
    {
        public ManageLoginEntryUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as ManageLoginEntryViewModel).GetDisplayedList();
        }
    }
}
