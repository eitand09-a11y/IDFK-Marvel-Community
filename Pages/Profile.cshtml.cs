using IDFK.Data;
using IDFK.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IDFK.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment; // ???? ??? ???? ??????? wwwroot

        public ProfileModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public User? CurrentUser { get; set; }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return RedirectToPage("/Account/Login");

            CurrentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userIdClaim));
            return Page();
        }

        [BindProperty]
        public string? NewPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(int.Parse(userIdClaim));

            if (user == null) return RedirectToPage("/Account/Login");

            // 1. ????? ?????? ????? (?? ???? ????)
            if (Upload != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Upload.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                user.ProfilePicturePath = "/uploads/" + fileName;
            }

            // 2. ????? ?????? ????? (?? ????? ????? ????)
            if (!string.IsNullOrEmpty(NewPassword))
            {
                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, NewPassword);
            }

            await _context.SaveChangesAsync();

            // 3. ????? ?-Cookie ???? ??? (????!)
            // ?? ???? ?-Navbar ??????? ??? ?????? ?????
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Username == "Admin" ? "Admin" : "User"),
                new Claim("ProfilePicture", user.ProfilePicturePath ?? "/images/default-avatar.png")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage();
        }

    }
}
