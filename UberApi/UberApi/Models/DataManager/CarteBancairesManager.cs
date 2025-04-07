using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class CarteBancaireManager : ICarteBancaireRepository
    {
        readonly S221UberContext? s221UberContext;

        public CarteBancaireManager() { }

        public CarteBancaireManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetAllAsync()
        {
            return await s221UberContext.CarteBancaires
                .Include(c => c.Courses)
                .Include(c => c.IdClients)
                .ToListAsync();
        }

        public async Task<ActionResult<CarteBancaire>> GetByIdAsync(int id)
        {
            return await s221UberContext.CarteBancaires
                .Include(c => c.Courses)
                .Include(c => c.IdClients)
                .FirstOrDefaultAsync(u => u.IdCb == id);
        }

        public async Task<ActionResult<CarteBancaire>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.CarteBancaires.Include(c => c.Courses)
                            .Include(c => c.IdClients).FirstOrDefaultAsync(u => u.NumeroCb.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(CarteBancaire entity)
        {
            await s221UberContext.CarteBancaires.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task AddClientsCBAsync(CarteBancaire entity, int clientId)
        {
            // Étape 1 : Vérifier si le client existe
            var client = await s221UberContext.Clients.FindAsync(clientId);
            if (client == null)
            {
                throw new Exception("Client introuvable !");
            }

            // Étape 2 : Ajouter la carte bancaire en base
            await s221UberContext.CarteBancaires.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();  // Sauvegarder pour générer l'ID de la carte

            // Étape 3 : Manipuler directement la table de jointure avec un dictionnaire
            // Ici, on ajoute une ligne dans la table de jointure sans avoir de modèle spécifique
            await s221UberContext.Database.ExecuteSqlRawAsync(
                "INSERT INTO t_j_clientcarte_clca (clt_id, cb_id) VALUES ({0}, {1})",
                clientId, entity.IdCb); // On utilise les ID du client et de la carte bancaire

            // Étape 4 : Sauvegarder la relation dans la base de données
            await s221UberContext.SaveChangesAsync();  // Optionnel si ExecuteSqlRawAsync fonctionne
        }




        public async Task UpdateAsync(CarteBancaire newCarteBancaire, CarteBancaire entity)
        {
            s221UberContext.Entry(newCarteBancaire).State = EntityState.Modified;
            newCarteBancaire.IdCb = entity.IdCb;
            newCarteBancaire.NumeroCb = entity.NumeroCb;
            newCarteBancaire.DateExpireCb = entity.DateExpireCb;
            newCarteBancaire.Cryptogramme = entity.Cryptogramme;
            newCarteBancaire.TypeCarte = entity.TypeCarte;
            newCarteBancaire.TypeReseaux = entity.TypeReseaux;

            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarteBancaire cb)
        {
            var carteBancaire = s221UberContext.CarteBancaires
                            .Include(c => c.Courses)
                            .Include(c => c.IdClients)
                            .FirstOrDefault(c => c.IdCb == cb.IdCb);
            s221UberContext.CarteBancaires.Remove(carteBancaire);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
