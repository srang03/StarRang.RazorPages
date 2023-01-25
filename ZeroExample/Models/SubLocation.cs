using System.ComponentModel.DataAnnotations;

namespace ZeroExample.Models
{
    public class SubLocation
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }

        [Display(Name = "Location")]
        public Location? LocationRef { get; set; }
        public int? LocationId { get; set; }
    }
}
