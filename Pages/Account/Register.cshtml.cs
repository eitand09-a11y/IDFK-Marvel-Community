using IDFK.Data;
using IDFK.Models;
using Microsoft.AspNetCore.Identity; // ???? ????? ???????
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace IDFK.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
            // ?? ?????? ????
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1. ????? ?? ??????? ????? ?????? ?????? ??????? ?-Model
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 2. ????? ?? ??????? ??? ???? ???? ???????
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == NewUser.Email);

            if (existingUser != null)
            {
                // ????? ????? ????? ?????? ??????
                ModelState.AddModelError("NewUser.Email", "This email is already registered.");
                return Page();
            }

            // 3. ????? ????? ?????? (???? ???? ??? ????)
            var passwordHasher = new PasswordHasher<User>();
            NewUser.Password = passwordHasher.HashPassword(NewUser, NewUser.Password);

            _context.Users.Add(NewUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Login");
        }

    }
}
