using Newtonsoft.Json;
using PosTicket.Repository.Interface;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PosTicket.Repository.WebService
{
    public class TicketRepository : ITicketRepository
    {
        public async Task<ReprintTicketResponse> GetListTicketBySale(string sale)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/ticket");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            request.AddParameter("sale", sale);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            ReprintTicketResponse ticketResponse = JsonConvert.DeserializeObject<ReprintTicketResponse>(response.Content);
            return ticketResponse;
        }
        public async Task<PrintTicketResponse> PostListTicket(PrintTicketRequest printTicketRequest)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/ticket");
            RestRequest request = WebServiceContext.GetRequestBody("post");

            request.AddParameter("application/json", JsonConvert.SerializeObject(printTicketRequest), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            PrintTicketResponse ticketResponse = JsonConvert.DeserializeObject<PrintTicketResponse>(response.Content);
            return ticketResponse;
        }
    }
}
