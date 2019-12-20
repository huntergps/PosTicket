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
            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);
                if(response.ErrorException != null)
                {
                    LoginResponse loginResponse = new LoginResponse { error = new ErrorMessage { code = 400, message = response.ErrorMessage} };
                    return loginResponse;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadGateway)
                {
                    LoginResponse loginResponse = new LoginResponse { error=new ErrorMessage {code =502,message= response.StatusDescription } };
                    return loginResponse;
                }
                else
                {
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
                    return loginResponse;
                }
            }
            catch(Exception e)
            {
                LoginResponse loginResponse = new LoginResponse { error = new ErrorMessage { code = 400, message = e.Message } };
                return loginResponse;
            }
        }
    }
}
