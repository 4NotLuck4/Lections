using System.ComponentModel.DataAnnotations.Schema;

namespace Lections1007.Model
{
    [Table("Game")]
    public class Game
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public Int16 KeysAmount { get; set; }

        public Category? Category { get; set; }
    }
}
