using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PosTicket.Repository.Interface;

namespace PosTicket.Repository.WebService
{
    public class DepositRepository : IDepositRepository
    {
        public async Task<PosSession> SendDepositAsync(Deposit depositRequest)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/pos/session");
            RestRequest request = WebServiceContext.GetRequestBody("post");

            request.AddParameter("application/json", JsonConvert.SerializeObject(depositRequest), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            PosSession sessionResponse = JsonConvert.DeserializeObject<PosSession>(response.Content);
            return sessionResponse;
        }
    }
}