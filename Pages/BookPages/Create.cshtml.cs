using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ungurean_Mara_Lab2.Models;

namespace Ungurean_Mara_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Ungurean_Mara_Lab2Context _context;

    public CreateModel(Ungurean_Mara_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");

        var book = new Book();
        book.BookCategories = new List<BookCategory>();
        PopulateAssignedCategoryData(_context, book);

        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var newBook = new Book();
        if (selectedCategories != null)
        {
            newBook.BookCategories = new List<BookCategory>();
            foreach (var cat in selectedCategories)
            {
                var catToAdd = new BookCategory
                {
                    CategoryID = int.Parse(cat)
                };
                newBook.BookCategories.Add(catToAdd);
            }
        }
        Book.BookCategories = newBook.BookCategories;

        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");
            PopulateAssignedCategoryData(_context, Book);
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}