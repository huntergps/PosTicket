using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PosTicket.Repository.Interface;
using System;

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
            PosSession sessionResponse = new PosSession();
            PosSessionErrorMessage errorMessage = new PosSessionErrorMessage();
            if (response.ErrorMessage != null)
            {
                errorMessage.message = response.ErrorMessage;
                errorMessage.code = 500;
                sessionResponse.error = errorMessage;
                return sessionResponse;
            }
            sessionResponse = JsonConvert.DeserializeObject<PosSession>(response.Content);
            return sessionResponse;
        }
    }
}