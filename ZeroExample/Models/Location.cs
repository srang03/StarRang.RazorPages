using System.ComponentModel.DataAnnotations;

namespace ZeroExample.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
        public string Property { get; set; } = string.Empty;
        [Display(Name = "건물")]
        public Property? PropertyRef { get; set; }
        [Display(Name= "건물")]
        public int PropertyId { get; set; }
    }
}
