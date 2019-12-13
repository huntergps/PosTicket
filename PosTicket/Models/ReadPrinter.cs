using PosTicket.Repository.Interface;
using PosTicket.Repository.PrinterData;
using System.Collections.Generic;

namespace PosTicket.Models
{
    sealed class ReadPrinter
    {
        public IPrinterRepository Repository { get; private set; }
        public List<Printer> GetAllPrinter()
        {
            Repository = new PrinterRepository();
            return (List<Printer>)Repository.GetPrinter();
        }
    }
}
