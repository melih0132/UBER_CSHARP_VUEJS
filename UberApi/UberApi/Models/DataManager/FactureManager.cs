using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class FactureManager : IDataRepository<Facture>
    {
        readonly S221UberContext? s221UberContext;
        public FactureManager() { }
        public FactureManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Facture>>> GetAllAsync()
        {
            return await s221UberContext.Factures.ToListAsync();
        }
        public async Task<ActionResult<Facture>> GetByIdAsync(int id)
        {
            return await s221UberContext.Factures.FirstOrDefaultAsync(u => u.IdFacture == id);
        }

        public async Task<ActionResult<IEnumerable<Facture>>> GetByDateAsync(string libelle)
        {
            DateOnly dateParsee = DateOnly.Parse(libelle);
            return await s221UberContext.Factures.Where(u => u.DateFacture == dateParsee).ToListAsync();
        }
        public async Task<ActionResult<Facture>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Factures.FirstOrDefaultAsync(u => u.MontantReglement.ToString() == libelle.ToUpper());
        }
        public async Task AddAsync(Facture entity)
        {
            await s221UberContext.Factures.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Facture newFacture, Facture entity)
        {
            s221UberContext.Entry(newFacture).State = EntityState.Modified;
            newFacture.IdFacture = entity.IdFacture;
            newFacture.IdReservation = entity.IdReservation;
            newFacture.IdCommande = entity.IdCommande;
            newFacture.IdPays = entity.IdPays;
            newFacture.IdClient = entity.IdClient;
            newFacture.MontantReglement = entity.MontantReglement;
            newFacture.DateFacture = entity.DateFacture;
            newFacture.Quantite = entity.Quantite;


            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Facture facture)
        {
            s221UberContext.Factures.Remove(facture);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
