using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadPartnerResponse
    {
        public IPartnerRepository Repository { get; private set; }
        public async Task<SalesPerson> GetSalesPersonAsync()
        {
            Repository = new PartnerRepository();
            return await Repository.GetSalesPersonAsync();
        }
        public async Task<Customer> GetCustomerAsync()
        {
            Repository = new PartnerRepository();
            return await Repository.GetCustomerAsync();
        }
    }
}
