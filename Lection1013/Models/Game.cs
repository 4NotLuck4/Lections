namespace Lection1013.Models;

public partial class Game
{
    public int GameId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public short KeysAmount { get; set; }

    public virtual Category Category { get; set; } = null!;

    public decimal OldPrice { get; set; } = 0;

}
