using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Ecommerce.Infrastructure.Interface
{
    public interface IDiscountProductRepository
    {
        Task<int> GetDiscount(int id);
    }
}
