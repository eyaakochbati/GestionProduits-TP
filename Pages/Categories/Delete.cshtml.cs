using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context) => _context = context;

    [BindProperty]
    public Categorie Categorie { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Categorie = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (Categorie == null) return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var categorie = await _context.Categories.FindAsync(id);
        if (categorie != null)
        {
            Categorie = categorie;
            _context.Categories.Remove(Categorie);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}