using System;
using System.Collections.Generic;
using Dna;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
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
using PosTicket.DataModels;
using PosTicket.Models;
using PosTicket.ViewModel;
using static Dna.FrameworkDI;
using static PosTicket.DI;

namespace PosTicket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.viewModel = new MainViewModel();
            this.DataContext = viewModel;
            viewModel.ViewLoaded();
            ViewModelApplication.GoToPage(ApplicationPage.MainPos);
            
        }
    }
}
