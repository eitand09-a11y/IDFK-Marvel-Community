using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IDFK.Models;
using IDFK.Data; // ???? ??? ????? ?-DbContext ???

namespace IDFK.Pages.Admin
{
    public class ManageActorsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageActorsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actor NewActor { get; set; } = new();
        public List<Movie> Movies { get; set; } = new();
        public List<Actor> ActorsList { get; set; } = new();

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies.ToListAsync();
            ActorsList = await _context.Actors.ToListAsync();
        }

        // ????? ???? ???
        public async Task<IActionResult> OnPostAddActorAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Actors.Add(NewActor);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        // ???? ???? ???? (???? ?????)
        public async Task<IActionResult> OnPostAssignActorAsync(int selectedMovieId, int selectedActorId)
        {
            var movie = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == selectedMovieId);

            var actor = await _context.Actors.FindAsync(selectedActorId);

            if (movie != null && actor != null)
            {
                if (!movie.Actors.Contains(actor))
                {
                    movie.Actors.Add(actor);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage();
        }



        // 1. Delete only ACTORS
        public async Task<IActionResult> OnPostResetActorsAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM ActorMovie");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Actors");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Actors'");
            return RedirectToPage();
        }

        // 2. Delete only MOVIES
        public async Task<IActionResult> OnPostResetMoviesAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM ActorMovie");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Movies");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Movies'");
            return RedirectToPage();
        }

        // 3. Restart ALL
        public async Task<IActionResult> OnPostResetAllAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM ActorMovie");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Actors");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Movies");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Actors' OR name='Movies'");
            return RedirectToPage();
        }


    }
}
