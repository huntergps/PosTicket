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
    /// Interaction logic for ViewPayment.xaml
    /// </summary>
    public partial class ViewPayment : Window
    {
        public ViewPayment()
        {
            InitializeComponent();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            PosMainViewModel CommandBindingsHapus = new PosMainViewModel();
            Button btn = ((Button)sender);
            btn.Command = CommandBindingsHapus.DelToPayCartCommand;
        }
        private void Reff_Click(object sender, RoutedEventArgs e)
        {
            PosMainViewModel CommandBindingsHapus = new PosMainViewModel();
            Button btn = ((Button)sender);
            btn.Command = CommandBindingsHapus.AddReffToPayCartCommand;
        }
    }
}
