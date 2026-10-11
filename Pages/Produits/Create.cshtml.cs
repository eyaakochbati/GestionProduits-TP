using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionProduits.Pages.Produits;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context) => _context = context;

    [BindProperty]
    public Produit Produit { get; set; } = new();

    public IActionResult OnGet()
    {
        // Remplir le ViewBag pour afficher les catégories dans le menu déroulant
        ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // S'il y a une erreur, on recharge le ViewBag pour ne pas perdre la liste lors du réaffichage
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom", Produit.CategorieId);
            return Page();
        }

        _context.Produits.Add(Produit);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}