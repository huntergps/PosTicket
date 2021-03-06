﻿using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using PosTicket.Repository.Interface;
using System.Collections.Generic;

namespace PosTicket.Repository.WebService
{
    public class ProductRepository : IProductRepository
    {
        public async Task<ProductCategory> GetProductCategoryAsync()
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/category");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            IRestResponse response = await client.ExecuteTaskAsync(request);
            ProductCategory categoryResponse = JsonConvert.DeserializeObject<ProductCategory>(response.Content);
            return categoryResponse;
        }
        public async Task<Product> GetProductAsync()
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/product");
            RestRequest request = WebServiceContext.GetRequestBody("get");

            IRestResponse response = await client.ExecuteTaskAsync(request);
            Product productResponse = JsonConvert.DeserializeObject<Product>(response.Content);
            return productResponse;
        }
        public async Task<ProductPrice> GetProductPrice(ProductPriceRequest productPriceRequest)
        {
            RestClient client = WebServiceContext.GetUrl("/api/v2/product/price");
            RestRequest request = WebServiceContext.GetRequestBody("post");

            request.AddParameter("application/json", JsonConvert.SerializeObject(productPriceRequest), ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            ProductPrice productPriceResponse = new ProductPrice();
            ProductPriceData resultdata = new ProductPriceData();
            ErrorMessage errorMessage = new ErrorMessage();
            if (response.ErrorMessage != null)
            {
                errorMessage.message = response.ErrorMessage;
                errorMessage.code = 500;
                resultdata.error = errorMessage;
                productPriceResponse.result = resultdata;
                return productPriceResponse;
            }
            productPriceResponse = JsonConvert.DeserializeObject<ProductPrice>(response.Content);
            return productPriceResponse;
        }
    }
}