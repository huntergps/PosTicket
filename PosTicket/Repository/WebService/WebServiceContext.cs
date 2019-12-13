using RestSharp;
using PosTicket.Models;
using System.Collections.Generic;
using PosTicket.Repository.Interface;

namespace PosTicket.Repository.WebService
{
    class WebServiceContext
    {
        public static RestClient GetUrl(string endpoint)
        {
            ReadConfig readConfig = new ReadConfig();
            List<Config> _configList = readConfig.GetAllConfigs();
            string endurl = _configList[0].server_url+endpoint;
            RestClient url =
                 new RestClient(endurl);
            return url;
        }
        public static RestRequest GetRequestBody(string method)
        {
            ReadSession readSession = new ReadSession();
            Session _sessionData = readSession.GetSession();
            string token = _sessionData.token;
            if (method == "post")
            {
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                if(token != null)
                {
                    request.AddHeader("Authorization", "Bearer "+ token);
                }
                return request;
            }
            else
            {
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                if (token != null)
                {
                    request.AddHeader("Authorization", "Bearer " + token);
                }
                return request;
            }
        }
    }
}
