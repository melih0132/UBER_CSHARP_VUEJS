using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class AdresseManager : IDataRepository<Adresse>
    {
        readonly S221UberContext? s221UberContext;
        public AdresseManager() { }
        public AdresseManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAllAsync()
        {
            return await s221UberContext.Adresses.ToListAsync();
        }
        public async Task<ActionResult<Adresse>> GetByIdAsync(int id)
        {
            return await s221UberContext.Adresses.FirstOrDefaultAsync(u => u.IdAdresse == id);
        }
        public async Task<ActionResult<Adresse>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Adresses.FirstOrDefaultAsync(u => u.LibelleAdresse.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(Adresse entity)
        {
            await s221UberContext.Adresses.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Adresse newAdresse, Adresse entity)
        {
            s221UberContext.Entry(newAdresse).State = EntityState.Modified;
            newAdresse.IdAdresse = entity.IdAdresse;
            newAdresse.IdVille = entity.IdVille;
            newAdresse.LibelleAdresse = entity.LibelleAdresse;
            newAdresse.Latitude = entity.Latitude;
            newAdresse.Longitude = entity.Longitude;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Adresse adresse)
        {
            s221UberContext.Adresses.Remove(adresse);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
