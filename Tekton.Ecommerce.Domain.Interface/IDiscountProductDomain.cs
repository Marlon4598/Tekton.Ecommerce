using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Ecommerce.Domain.Interface
{
    public interface IDiscountProductDomain
    {
        Task<int> GetDiscount(int id);
    }
}
