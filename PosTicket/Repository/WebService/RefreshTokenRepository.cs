using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using PosTicket.Repository.Interface;

namespace PosTicket.Repository.WebService
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public async Task<RefreshTokenResponse> SendTokenDataAsync(RefreshTokenRequest tokenRequest)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/login");
            RestRequest request = WebServiceContext.GetRequestBody("post");

            request.AddParameter("application/json", JsonConvert.SerializeObject(tokenRequest), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            RefreshTokenResponse refreshTokenResponse = JsonConvert.DeserializeObject<RefreshTokenResponse>(response.Content);
            return refreshTokenResponse;
        }
    }
}
