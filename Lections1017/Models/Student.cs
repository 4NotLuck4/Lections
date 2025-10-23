using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lections1017.Models
{
    public partial class Student
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [MinLength(3)]
        public string FullName { get; set; }
        [DataType(DataType.Date)]

        public DateTime Birthday { get; set; }
        public int GroupId { get; set; }
        public string Comment { get; set; }
        [Range(2.0, 5)]
        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageMark { get; set; } = 4;
        [EmailAddress]
        public string? Email { get; set; } = null!;
        [Phone]
        public string? Phone { get; set; }
        public string? Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string? Confirm { get; set; }
        [ForeignKey("IdGroup")]
        public virtual Group Group { get; set; } = null!;
    }
}
