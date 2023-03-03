namespace Tekton.Ecommerce.Domain.Entity
{
    public class Products
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public Boolean Status { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

    }
}