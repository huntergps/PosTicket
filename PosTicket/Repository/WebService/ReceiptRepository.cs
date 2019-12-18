using Newtonsoft.Json;
using PosTicket.Repository.Interface;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PosTicket.Repository.WebService
{
    public class ReceiptRepository : IReceiptRepository
    {
        public async Task<ReprintReceiptResponse> GetReceiptBySale(string sale)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/sale");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            request.AddParameter("name", sale);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            ReprintReceiptResponse ticketResponse = JsonConvert.DeserializeObject<ReprintReceiptResponse>(response.Content);
            return ticketResponse;
        }
    }
}
