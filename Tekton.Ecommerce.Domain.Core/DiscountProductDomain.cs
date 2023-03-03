using Tekton.Ecommerce.Infrastructure.Interface;
using Tekton.Ecommerce.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Ecommerce.Domain.Core
{
    public class DiscountProductDomain : IDiscountProductDomain
    {
        private readonly IDiscountProductRepository _discountProductRepository;
        public DiscountProductDomain(IDiscountProductRepository discountProductRepository)
        {
            _discountProductRepository = discountProductRepository;
        }

        public async Task<int> GetDiscount(int id)
        {
            return await _discountProductRepository.GetDiscount(id);
        }
    }
}
