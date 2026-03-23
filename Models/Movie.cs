using System.ComponentModel.DataAnnotations;

namespace IDFK.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "חובה להזין שם לסרט")]
        [Display(Name = "שם הסרט")]
        public string Title { get; set; } = null!;

        [Display(Name = "תיאור העלילה")]
        public string Description { get; set; } = null!;

        [Display(Name = "שנת יציאה")]
        public int ReleaseYear { get; set; }

        [Display(Name = "קישור לטריילר (YouTube)")]
        public string TrailerUrl { get; set; } = null!;

        [Display(Name = "נתיב לתמונת פוסטר")]
        public string? PosterPath { get; set; }
        public List<Actor> Actors { get; set; } = new List<Actor>();


        // קשר לצאט (נוסיף בהמשך)
        // public List<ChatMessage> Comments { get; set; }
    }
}
