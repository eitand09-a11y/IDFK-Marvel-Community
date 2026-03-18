using System.ComponentModel.DataAnnotations;

namespace IDFK.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")] // בודק פורמט אימייל
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")] // מינימום 6 תווים
        public string Password { get; set; }

        public string? ProfilePicturePath { get; set; }

    }
}
