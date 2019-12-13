using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PosTicket.ViewModel;

namespace PosTicket.Views
{
    /// <summary>
    /// Interaction logic for ViewSetDeposit.xaml
    /// </summary>
    public partial class ViewSetDeposit : Window
    {
        private static SetDepositViewModel setDepositViewModel = new SetDepositViewModel();
        public ViewSetDeposit()
        {
            InitializeComponent();
            this.DataContext = setDepositViewModel;
            if (setDepositViewModel.CloseAction == null)
                setDepositViewModel.CloseAction = new Action(this.Hide);
            setDepositViewModel.ViewLoaded();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login lWindow = new Login();
            this.Hide();
            lWindow.ShowDialog();
        }
    }
}
