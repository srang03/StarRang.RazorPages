using System.ComponentModel.DataAnnotations;

namespace VisualAcademy.Models
{
    /// <summary>
    /// 국물 
    /// </summary>
    public class Broth
    {
        public int Id { get; set; }

        [Display(Name = "이름")]
        public string Name { get; set; } = string.Empty;

        public bool IsVegan { get; set; } = false;
    }
}
