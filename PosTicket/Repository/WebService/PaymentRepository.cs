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
        public async Task<PaymentTransactionResponse> PayTransactionAsync(PaymentTransactionRequest paymentTransactionRequest)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/sale");
            RestRequest request = WebServiceContext.GetRequestBody("post");

            request.AddParameter("application/json", JsonConvert.SerializeObject(paymentTransactionRequest), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            PaymentTransactionResponse paymentTransactionResponse = JsonConvert.DeserializeObject<PaymentTransactionResponse>(response.Content);
            return paymentTransactionResponse;
        }
    }
}