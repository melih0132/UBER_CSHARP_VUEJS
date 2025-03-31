using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class VilleManager : IDataRepository<Ville>
    {
        readonly S221UberContext? s221UberContext;
        public VilleManager() { }
        public VilleManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Ville>>> GetAllAsync()
        {
            return await s221UberContext.Villes.ToListAsync();
        }
        public async Task<ActionResult<Ville>> GetByIdAsync(int id)
        {
            return await s221UberContext.Villes.FirstOrDefaultAsync(u => u.IdVille == id);
        }
        public async Task<ActionResult<Ville>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Villes.FirstOrDefaultAsync(u => u.NomVille.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(Ville entity)
        {
            await s221UberContext.Villes.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Ville newVille, Ville entity)
        {
            s221UberContext.Entry(newVille).State = EntityState.Modified;
            newVille.IdVille = entity.IdVille;
            newVille.IdPays = entity.IdPays;
            newVille.IdCodePostal = entity.IdCodePostal;
            newVille.NomVille = entity.NomVille;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Ville ville)
        {
            s221UberContext.Villes.Remove(ville);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
