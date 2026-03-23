using IDFK.Models;
using System.ComponentModel.DataAnnotations;

namespace IDFK.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "חובה להזין שם שחקן")]
        [Display(Name = "שם השחקן")]
        public string Name { get; set; } = null!;

        [Display(Name = "שם הדמות")]
        public string CharacterName { get; set; } = null!;

        [Display(Name = "תמונת שחקן")]
        public string ImageUrl { get; set; } = "/images/default-actor.png"; // תמונת ברירת מחדל

        // קשר של "רבים לרבים" - שחקן יכול להופיע בהרבה סרטים
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
