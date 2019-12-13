using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PosTicket.ViewModel;
namespace PosTicket
{
    /// <summary>
    /// Interaction logic for Config.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        private static ReadConfigViewModel readConfigViewModel = new ReadConfigViewModel();
        public Configuration()
        {
            InitializeComponent();
            this.DataContext = readConfigViewModel;
            if (readConfigViewModel.CloseAction == null)
                readConfigViewModel.CloseAction = new Action(this.Close);
            readConfigViewModel.ViewLoaded();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Login lWindow = new Login();
            this.Hide();
            lWindow.ShowDialog();
        }

        private void ApiKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).ApiKeyValue = ((PasswordBox)sender).Password; }
        }
    }
}
