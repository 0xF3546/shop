using shop.Core.Category;
using System.ComponentModel.DataAnnotations;

namespace shop.DataAccess.Category
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Category(string name)
        {
            Name = name;
        }

        public CategoryDto GetDto()
        {
            return new()
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
