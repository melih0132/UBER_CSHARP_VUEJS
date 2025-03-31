using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        readonly S221UberContext? s221UberContext;

        public ProduitManager() { }

        public ProduitManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllAsync()
        {
            return await s221UberContext.Produits
                .Include(p => p.IdEtablissements)
                    .ThenInclude(e => e.IdAdresseNavigation)
                        .ThenInclude(a => a.IdVilleNavigation)
                            .ThenInclude(v => v.IdCodePostalNavigation)
                                .ThenInclude(cp => cp.IdPaysNavigation)
                .Include(p => p.IdCategories)
                .ToListAsync();
        }

        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            return await s221UberContext.Produits
                .Include(p => p.IdEtablissements)
                    .ThenInclude(e => e.IdAdresseNavigation)
                        .ThenInclude(a => a.IdVilleNavigation)
                            .ThenInclude(v => v.IdCodePostalNavigation)
                                .ThenInclude(cp => cp.IdPaysNavigation)
                .Include(p => p.IdCategories)
                .FirstOrDefaultAsync(p => p.IdProduit == id);
        }

        public async Task<ActionResult<Produit>> GetByStringAsync(string nom)
        {
            return await s221UberContext.Produits.FirstOrDefaultAsync(u => u.NomProduit.ToUpper() == nom.ToUpper());
        }

        public async Task AddAsync(Produit entity)
        {
            s221UberContext.Produits.Add(entity);
            s221UberContext.SaveChanges();
        }

        public async Task UpdateAsync(Produit newProduit, Produit entity)
        {
            s221UberContext.Entry(newProduit).State = EntityState.Modified;
            newProduit.IdProduit = entity.IdProduit;
            newProduit.IdCategories = entity.IdCategories;
            newProduit.IdEtablissements = entity.IdEtablissements;
            newProduit.NomProduit = entity.NomProduit;
            newProduit.Description = entity.Description;
            newProduit.PrixProduit = entity.PrixProduit;
            newProduit.ImageProduit = entity.ImageProduit;
            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produit utilisateur)
        {
            s221UberContext.Produits.Remove(utilisateur);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
