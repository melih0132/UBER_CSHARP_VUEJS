using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class CategoriePrestationManager : IDataRepository<CategoriePrestation>
    {
        readonly S221UberContext? s221UberContext;
        public CategoriePrestationManager() { }
        public CategoriePrestationManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<CategoriePrestation>>> GetAllAsync()
        {
            return await s221UberContext.CategoriePrestations.ToListAsync();
        }
        public async Task<ActionResult<CategoriePrestation>> GetByIdAsync(int id)
        {
            return await s221UberContext.CategoriePrestations.FirstOrDefaultAsync(u => u.IdCategoriePrestation == id);
        }
        public async Task<ActionResult<CategoriePrestation>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.CategoriePrestations.FirstOrDefaultAsync(u => u.LibelleCategoriePrestation.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(CategoriePrestation entity)
        {
            s221UberContext.CategoriePrestations.Add(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(CategoriePrestation newCategoriePrestation, CategoriePrestation entity)
        {
            s221UberContext.Entry(newCategoriePrestation).State = EntityState.Modified;
            newCategoriePrestation.IdCategoriePrestation = entity.IdCategoriePrestation;
            newCategoriePrestation.LibelleCategoriePrestation = entity.LibelleCategoriePrestation;
            newCategoriePrestation.DescriptionCategoriePrestation = entity.DescriptionCategoriePrestation;
            newCategoriePrestation.ImageCategoriePrestation = entity.ImageCategoriePrestation;

            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(CategoriePrestation cb)
        {
            s221UberContext.CategoriePrestations.Remove(cb);
            await s221UberContext.SaveChangesAsync();
        }
    }
}
