using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class PaysManager : IDataRepository<Pays>
    {
        readonly S221UberContext? s221UberContext;
        public PaysManager() { }
        public PaysManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Pays>>> GetAllAsync()
        {
            return await s221UberContext.Pays.ToListAsync();
        }
        public async Task<ActionResult<Pays>> GetByIdAsync(int id)
        {
            return await s221UberContext.Pays.FirstOrDefaultAsync(u => u.IdPays == id);
        }
        public async Task<ActionResult<Pays>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Pays.FirstOrDefaultAsync(u => u.NomPays.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(Pays entity)
        {
            await s221UberContext.Pays.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Pays newPays, Pays entity)
        {
            s221UberContext.Entry(newPays).State = EntityState.Modified;
            newPays.IdPays = entity.IdPays;
            newPays.NomPays = entity.NomPays;
            newPays.PourcentageTva = entity.PourcentageTva;

            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Pays adresse)
        {
            s221UberContext.Pays.Remove(adresse);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
