using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PosTicket.Repository.Interface;
using System.Collections.Generic;

namespace PosTicket.Repository.WebService
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<PaymentMethod> GetPaymentMethodAsync()
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/payment_method");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            IRestResponse response = await client.ExecuteTaskAsync(request);
            PaymentMethod paymentMethodResponse = JsonConvert.DeserializeObject<PaymentMethod>(response.Content);
            return paymentMethodResponse;
        }
    }
}