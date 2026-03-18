using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IDFK.Data;
using IDFK.Models;

namespace IDFK.Pages.Admin
{
    // ?????: ?? ????? ?? Role ?? "Admin" ???? ??????
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // ????? ????? ?? ?? ???????? ????? ?????
        public List<User> UsersList { get; set; }

        public async Task OnGetAsync()
        {
            // ????? ?? ???????? ?????
            UsersList = await _context.Users.ToListAsync();
        }

        // ??????? ?????? ?????
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
