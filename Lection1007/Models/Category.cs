using System.ComponentModel.DataAnnotations.Schema;

namespace Lections1007.Model
{
    [Table("Category")]
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public IEnumerable<Game>? Games { get; set; }
    }
}
