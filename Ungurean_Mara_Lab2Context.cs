using Microsoft.EntityFrameworkCore;
using Ungurean_Mara_Lab2.Models;

public class Ungurean_Mara_Lab2Context(DbContextOptions<Ungurean_Mara_Lab2Context> options) : DbContext(options)
{
    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<Publisher> Publisher { get; set; } = default!;
    public DbSet<Author> Author { get; set; } = default!;
}