using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogExample.Models
{
    [Table("Blogs")]
    public class Blog
    {
 
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }

}
