using GestionProduits.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categorie> Categories { get; set; }
    public DbSet<Produit> Produits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        // 1. Configuration de la relation avec restriction de suppression
        modelBuilder.Entity<Produit>()
            .HasOne(p => p.Categorie)
            .WithMany()
            .HasForeignKey(p => p.CategorieId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. Seeding des données (catégories par défaut)
        modelBuilder.Entity<Categorie>().HasData(
            new Categorie { Id = 1, Nom = "Informatique" },
            new Categorie { Id = 2, Nom = "Bureau" }
        );
    }
}