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
    public class LoginRepository : ILoginRepository
    {
        public async Task<LoginResponse> SendLoginDataAsync(LoginRequest loginRequest)
        {
            RestClient client = (RestClient)WebServiceContext.GetUrl("/api/v2/login");
            RestRequest request = (RestRequest)WebServiceContext.GetRequestBody("post");

            request.AddParameter("application/json", JsonConvert.SerializeObject(loginRequest), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            return loginResponse;
        }
    }
}
