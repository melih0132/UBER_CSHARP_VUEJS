using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class CategorieProduitManager : IDataRepository<CategorieProduit>
    {
        readonly S221UberContext? s221UberContext;
        public CategorieProduitManager() { }
        public CategorieProduitManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<CategorieProduit>>> GetAllAsync()
        {
            return await s221UberContext.CategorieProduits.ToListAsync();
        }
        public async Task<ActionResult<CategorieProduit>> GetByIdAsync(int id)
        {
            return await s221UberContext.CategorieProduits.FirstOrDefaultAsync(u => u.IdCategorie == id);
        }
        public async Task<ActionResult<CategorieProduit>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.CategorieProduits.FirstOrDefaultAsync(u => u.NomCategorie.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(CategorieProduit entity)
        {
            s221UberContext.CategorieProduits.Add(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(CategorieProduit newCategorieProduit, CategorieProduit entity)
        {
            s221UberContext.Entry(newCategorieProduit).State = EntityState.Modified;
            newCategorieProduit.IdCategorie = entity.IdCategorie;
            newCategorieProduit.NomCategorie = entity.NomCategorie;

            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(CategorieProduit catPresta)
        {
            s221UberContext.CategorieProduits.Remove(catPresta);
            await s221UberContext.SaveChangesAsync();
        }
    }
}


