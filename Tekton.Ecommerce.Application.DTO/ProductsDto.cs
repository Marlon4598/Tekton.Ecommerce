using System.ComponentModel.DataAnnotations;

namespace Tekton.Ecommerce.Application.DTO
{
    public class ProductsDto
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        [Required]
        public Boolean Status { get; set; }
        public string? StatusName { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}