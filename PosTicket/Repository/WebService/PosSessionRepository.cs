using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using PosTicket.Repository.Interface;
using PosTicket.Models;

namespace PosTicket.Repository.WebService
{
    public class PosSessionRepository : IPosSessionRepository
    {
        public async Task<PosSession> GetPosSessionDataAsync(string ip_address)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/pos/session");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            request.AddParameter("ip", ip_address);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            PosSession posResponse = JsonConvert.DeserializeObject<PosSession>(response.Content);
            return posResponse;
        }
        public async Task<PosSessionClose> ClosePoseSessionAsync(PosSessionCloseRequest posSessionCloseRequest)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/pos/session/close");
            RestRequest request = WebServiceContext.GetRequestBody("post");

            request.AddParameter("application/json", JsonConvert.SerializeObject(posSessionCloseRequest), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            PosSessionClose posSessionCloseResponse = JsonConvert.DeserializeObject<PosSessionClose>(response.Content);
            return posSessionCloseResponse;
        }
    }
}
