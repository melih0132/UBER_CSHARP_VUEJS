using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class EntrepriseManager : IDataRepository<Entreprise>
    {
        readonly S221UberContext? s221UberContext;
        public EntrepriseManager() { }
        public EntrepriseManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetAllAsync()
        {
            return await s221UberContext.Entreprises.ToListAsync();
        }
        public async Task<ActionResult<Entreprise>> GetByIdAsync(int id)
        {
            return await s221UberContext.Entreprises.FirstOrDefaultAsync(u => u.IdEntreprise == id);
        }
        public async Task<ActionResult<Entreprise>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Entreprises.FirstOrDefaultAsync(u => u.NomEntreprise.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(Entreprise entity)
        {
            await s221UberContext.Entreprises.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Entreprise newEntreprise, Entreprise entity)
        {
            s221UberContext.Entry(newEntreprise).State = EntityState.Modified;
            newEntreprise.IdEntreprise = entity.IdEntreprise;
            newEntreprise.IdAdresse = entity.IdAdresse;
            newEntreprise.SiretEntreprise = entity.SiretEntreprise;
            newEntreprise.NomEntreprise = entity.NomEntreprise;
            newEntreprise.Taille = entity.Taille;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Entreprise entreprise)
        {
            s221UberContext.Entreprises.Remove(entreprise);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
