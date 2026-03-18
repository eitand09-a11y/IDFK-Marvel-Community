using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization; // 1. הוסף את ה-Using הזה

namespace IDFK.Pages
{
    [Authorize] // 2. זהו! השורה הזו נועלת את הדף למשתמשים רשומים בלבד
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
