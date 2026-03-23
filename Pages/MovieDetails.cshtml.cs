using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IDFK.Data;
using IDFK.Models;

namespace IDFK.Pages
{
    public class MovieDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MovieDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public List<ChatMessage> ChatHistory { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == id);
            if (Movie == null) return NotFound();

            // ????? 50 ??????? ???????? ?? ???? ???
            ChatHistory = await _context.ChatMessages
                .Where(c => c.MovieId == id)
                .OrderBy(c => c.Timestamp)
                .Take(50)
                .ToListAsync();

            return Page();
        }

    }
}
