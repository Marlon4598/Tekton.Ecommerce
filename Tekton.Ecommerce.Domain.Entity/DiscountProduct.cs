using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Ecommerce.Domain.Entity
{
    public class DiscountProduct
    {
        public string? ProductId { get; set; }
        public byte Discount { get; set; }
    }
}
