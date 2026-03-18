using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IDFK.Data;
using IDFK.Models;

namespace IDFK.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Movie> MoviesList { get; set; }

        public async Task OnGetAsync()
        {
            // ????? ?? ?????? ?????
            MoviesList = await _context.Movies.ToListAsync();
        }
    }
}
