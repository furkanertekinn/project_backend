using System.ComponentModel.DataAnnotations.Schema;

namespace product_backend.Entities
{
    public class Product
    {
        public int id { get; set; }
        public string productName { get; set; } = string.Empty;
        public decimal productUnitPrice { get; set; }
        public int productUnitInStock { get; set; }

        [NotMapped]
        public decimal total { get; set; }
    }
}
