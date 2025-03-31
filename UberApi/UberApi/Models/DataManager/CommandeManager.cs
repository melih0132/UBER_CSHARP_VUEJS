using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class CommandeManager : IDataRepository<Commande>
    {
        readonly S221UberContext? s221UberContext;
        public CommandeManager() { }
        public CommandeManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Commande>>> GetAllAsync()
        {
            return await s221UberContext.Commandes.ToListAsync();
        }
        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await s221UberContext.Commandes.FirstOrDefaultAsync(u => u.IdCommande == id);
        }
        public async Task<ActionResult<Commande>> GetByStringAsync(string statutCommande)
        {
            return await s221UberContext.Commandes.FirstOrDefaultAsync(u => u.StatutCommande.ToUpper() == statutCommande.ToUpper());
        }

        public async Task AddAsync(Commande entity)
        {
            await s221UberContext.Commandes.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Commande newCommande, Commande entity)
        {
            s221UberContext.Entry(newCommande).State = EntityState.Modified;
            newCommande.IdCommande = entity.IdCommande;
            newCommande.IdPanier = entity.IdPanier;
            newCommande.IdLivreur = entity.IdLivreur;
            newCommande.IdCb = entity.IdCb;
            newCommande.IdAdresse = entity.IdAdresse;
            newCommande.PrixCommande = entity.PrixCommande;
            newCommande.TempsCommande = entity.TempsCommande;
            newCommande.HeureCreation = entity.HeureCreation;
            newCommande.HeureCommande = entity.HeureCommande;
            newCommande.EstLivraison = entity.EstLivraison;
            newCommande.StatutCommande = entity.StatutCommande;
            newCommande.RefusDemandee = entity.RefusDemandee;
            newCommande.RemboursementEffectue = entity.RemboursementEffectue;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Commande commande)
        {
            s221UberContext.Commandes.Remove(commande);
            await s221UberContext.SaveChangesAsync();
        }
    }
}
