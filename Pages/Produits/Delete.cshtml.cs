using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Produits;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context) => _context = context;

    [BindProperty]
    public Produit Produit { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Produit = await _context.Produits
            .Include(p => p.Categorie)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Produit == null) return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var produit = await _context.Produits.FindAsync(id);

        if (produit != null)
        {
            Produit = produit;
            _context.Produits.Remove(Produit);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}