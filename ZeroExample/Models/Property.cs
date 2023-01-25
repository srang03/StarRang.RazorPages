using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroExample.Models
{
    [Table("Property")]
    public class Property
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
