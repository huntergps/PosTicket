using System.Collections.Generic;

namespace PosTicket.Repository.Interface
{
    public class DepositCashDetail
    {
        public int amount_unit { get; set; }
        public int qty { get; set; }
    }
    public class DepositData
    {
        public string pos_ip { get; set; }
        public double opening_cash_balance { get; set; }
        public List<DepositCashDetail> cash_detail { get; set; }
    }
    public class Deposit
    {
        public DepositData data { get; set; }
    }

}
