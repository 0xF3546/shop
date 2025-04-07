using shop.Core.Product;
using System.ComponentModel.DataAnnotations;

namespace shop.DataAccess.Product
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public ProductDto GetDto()
        {
            return new()
            {
                Description = Description,
                Price = Price,
                ImageUrl = "", //TODO
                Name = Name,
            };
        }
    }
}
