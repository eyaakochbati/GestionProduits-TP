using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Produits;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context) => _context = context;

    public List<Produit> Produits { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public int TotalStock { get; set; }
    public decimal PrixMoyen { get; set; }

    public async Task OnGetAsync()
    {
        // 1. Déclarer la requête en premier
        var produitsQuery = _context.Produits
            .Include(p => p.Categorie)
            .AsQueryable();

        // 2. Appliquer le filtre de recherche si présent
        if (!string.IsNullOrEmpty(SearchString))
        {
            produitsQuery = produitsQuery.Where(p => p.Nom.Contains(SearchString));
        }

        // 3. Calculer les statistiques sur la requête (filtrée ou non)
        TotalStock = await produitsQuery.SumAsync(p => p.Stock);
        PrixMoyen = await produitsQuery.AnyAsync() ? await produitsQuery.AverageAsync(p => p.Prix) : 0;

        // 4. Récupérer la liste finale des produits
        Produits = await produitsQuery.OrderBy(p => p.Nom).ToListAsync();
    }
}