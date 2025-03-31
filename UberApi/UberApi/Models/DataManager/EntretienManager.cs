using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class EntretienManager : IDataRepository<Entretien>
    {
        readonly S221UberContext? s221UberContext;
        public EntretienManager() { }
        public EntretienManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Entretien>>> GetAllAsync()
        {
            return await s221UberContext.Entretiens.ToListAsync();
        }
        public async Task<ActionResult<Entretien>> GetByIdAsync(int id)
        {
            return await s221UberContext.Entretiens.FirstOrDefaultAsync(u => u.IdEntretien == id);
        }
        public async Task<ActionResult<Entretien>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Entretiens.FirstOrDefaultAsync(u => u.IdCoursier.ToString().ToUpper() == libelle.ToString().ToUpper() );
        }

        public async Task AddAsync(Entretien entity)
        {
            await s221UberContext.Entretiens.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Entretien newEntretien, Entretien entity)
        {
            s221UberContext.Entry(newEntretien).State = EntityState.Modified;
            newEntretien.IdEntretien = entity.IdEntretien;
            newEntretien.IdCoursier = entity.IdCoursier;
            newEntretien.DateEntretien = entity.DateEntretien;
            newEntretien.Status = entity.Status;
            newEntretien.Resultat = entity.Resultat;
            newEntretien.RdvLogistiqueDate = entity.RdvLogistiqueDate;
            newEntretien.RdvLogistiqueLieu = entity.RdvLogistiqueLieu;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Entretien entretien)
        {
            s221UberContext.Entretiens.Remove(entretien);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
