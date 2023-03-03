using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Ecommerce.Domain.Entity;
using Tekton.Ecommerce.Infrastructure.Interface;

namespace Tekton.Ecommerce.Infrastructure.Interface
{
    public interface IProductsRepository : IGeneralRepository<Products>
    {
    }
}

