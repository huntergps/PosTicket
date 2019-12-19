using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PosTicket.Repository.Interface;
using System.Collections.Generic;

namespace PosTicket.Repository.WebService
{
    public class PartnerRepository : IPartnerRepository
    {
        public async Task<SalesPerson> GetSalesPersonAsync()
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/salesperson");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            IRestResponse response = await client.ExecuteTaskAsync(request);
            SalesPerson salesPersonResponse = JsonConvert.DeserializeObject<SalesPerson>(response.Content);
            return salesPersonResponse;
        }
        public async Task<Customer> GetCustomerAsync()
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/customer");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            IRestResponse response = await client.ExecuteTaskAsync(request);
            Customer customerResponse = JsonConvert.DeserializeObject<Customer>(response.Content);
            return customerResponse;
        }
    }
}