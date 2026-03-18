using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IDFK.Data;
using IDFK.Models;
using Microsoft.AspNetCore.Authorization;

namespace IDFK.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddMovieModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddMovieModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            // ????? ??????? ??? ??? ????? ?????? Null ?????? ???????
            NewMovie = new Movie();
        }

        [BindProperty]
        public Movie NewMovie { get; set; } // ????? ??? ????? ???? ??!

        [BindProperty]
        public IFormFile? PosterUpload { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ?? ?????? ?????
            if (PosterUpload != null)
            {
                var folderPath = Path.Combine(_environment.WebRootPath, "images", "posters");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PosterUpload.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PosterUpload.CopyToAsync(fileStream);
                }

                NewMovie.PosterPath = "/images/posters/" + fileName;
            }

            // ????? ????? ???????
            _context.Movies.Add(NewMovie);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
