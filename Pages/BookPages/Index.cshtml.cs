using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ungurean_Mara_Lab2.Models;

namespace Ungurean_Mara_Lab2.Pages.BookPages
{
    public class IndexModel : PageModel
    {
        private readonly Ungurean_Mara_Lab2Context _context;

        public IndexModel(Ungurean_Mara_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book
                    .Include(b => b.Publisher)
                    .Include(b => b.Author)
                    .ToListAsync();
        }
    }
}