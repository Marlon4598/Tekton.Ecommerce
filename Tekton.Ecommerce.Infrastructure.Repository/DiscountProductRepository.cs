using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Tekton.Ecommerce.Domain.Entity;
using Tekton.Ecommerce.Infrastructure.Interface;

namespace Tekton.Ecommerce.Infrastructure.Repository
{
    public class DiscountProductRepository : IDiscountProductRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DiscountProductRepository(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<int> GetDiscount(int id)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "/product/" + id.ToString());
            var client = _httpClientFactory.CreateClient("DiscountApi");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            var responseDiscount = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DiscountProduct>(responseDiscount, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result == null)
                return 0;
            else
                return result.Discount;
        }
    }
}
