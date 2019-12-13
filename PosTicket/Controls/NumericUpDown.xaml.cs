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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PosTicket.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        #region Public String NumValue

        // Dependency properties declaration
        public static readonly DependencyProperty NumValueProperty = DependencyProperty.Register(
            "NumValue",
            typeof(int),
            typeof(NumericUpDown),
            new PropertyMetadata(0, new PropertyChangedCallback(OnNumValueChanged)));

        public static void OnNumValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown iNumericUpDown = sender as NumericUpDown;
            if (iNumericUpDown.txtNum != null)
            {
                iNumericUpDown.txtNum.Text = e.NewValue.ToString();
            }
        }

        public int NumValue
        {
            get { return (int)GetValue(NumValueProperty); }
            set {
                if (value >= 0)
                    {
                        SetValue(NumValueProperty, value);
                    }
                }
        }

        #endregion

        private int _numValue = 0;
        public NumericUpDown()
        {
            InitializeComponent();
            txtNum.Text = _numValue.ToString();
        }
        private void CmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void CmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }

        private void TxtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out _numValue))
                txtNum.Text = _numValue.ToString();
        }
    }
}
