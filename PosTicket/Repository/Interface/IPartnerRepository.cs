using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosTicket.Repository.Interface
{
    public interface IPartnerRepository
    {
        Task<SalesPerson> GetSalesPersonAsync();
        Task<Customer> GetCustomerAsync();
    }
}
