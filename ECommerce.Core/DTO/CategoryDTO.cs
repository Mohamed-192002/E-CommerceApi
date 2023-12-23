using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.DTO
{
    public class CategoryDTO
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryDTOById : CategoryDTO
    {
        public int Id { get; set; }
    }
}
