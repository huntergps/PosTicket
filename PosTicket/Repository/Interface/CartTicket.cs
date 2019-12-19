using System;
using System.Collections.Generic;
using System.Text;
using PosTicket.ViewModel;

namespace PosTicket.Repository.Interface
{
    public class CartTicket : NavigableControlViewModel
    {
        public int id { get; set; }
        public string barcode { get; set; }
        public string product_name { get; set; }
        public string date_plan { get; set; }
        public string state { get; set; }
        private bool _flagprint;
        public bool flagprint {
            get 
            { 
                return _flagprint; 
            }
            set 
            {
                _flagprint = value;
                RaisePropertyChanged("flagprint");
            }
        }
    }
}
