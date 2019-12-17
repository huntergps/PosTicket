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
using PosTicket.Repository.UserSession;

namespace PosTicket.Views
{
    /// <summary>
    /// Interaction logic for ViewPosMain.xaml
    /// </summary>
    public partial class ViewPosMain : Window
    {
        private static PosMainViewModel posMainViewModel = new PosMainViewModel();
        public ViewPosMain()
        {
            InitializeComponent();
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            WindowState = WindowState.Maximized;
            cardPad.Height = (Height*47)/100;
            cardShop.Height = (Height*40)/100;
            cardProduct.Height = (Height*87)/100;
            DataContext = posMainViewModel;
            if (posMainViewModel.CloseAction == null)
                posMainViewModel.CloseAction = new Action(this.Close);
            posMainViewModel.ViewLoaded();
        }
    }
}
