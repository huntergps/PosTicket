using System.Windows;
using MaterialDesignThemes.Wpf;
using PosTicket.ViewModel;
using System.Windows.Controls;
using System;

namespace PosTicket
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 
    public partial class Login : Window
    {
        private static ReadLoginResponseViewModel readLoginResponseViewModels = new ReadLoginResponseViewModel();
        public Login()
        {
            InitializeComponent();
            this.DataContext = readLoginResponseViewModels;
            if (readLoginResponseViewModels.CloseAction == null)
                readLoginResponseViewModels.CloseAction = new Action(this.Close);
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }
        private void TextClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {
            Configuration cfg = new Configuration();
            this.Close();
            cfg.ShowDialog();
        }
        private void DialogHost_DialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            txtPassword.Clear();
        }
    }
}
