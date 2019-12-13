using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public interface IPrinterRepository
    {
        IEnumerable<Printer> GetPrinter();
    }
}
