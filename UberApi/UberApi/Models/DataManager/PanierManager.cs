using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using BCrypt.Net;

namespace UberApi.Models.DataManager
{
    public class PanierManager : IPanierRepository
    {
        readonly S221UberContext? s221UberContext;

        public PanierManager() { }

        public PanierManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<Panier>>> GetAllAsync()
        {
            return await s221UberContext.Paniers.Include(a=>a.Contient2s).ThenInclude(b=>b.IdProduitNavigation)
                .ToListAsync();
        }

        public async Task<ActionResult<Panier>> GetByIdAsync(int id)
        {
            return await s221UberContext.Paniers.Include(a => a.Contient2s).ThenInclude(b => b.IdProduitNavigation)
                .FirstOrDefaultAsync(u => u.IdPanier == id);
        }

        public async Task<ActionResult<Panier>> GetByStringAsync(string prix)
        {
            return await s221UberContext.Paniers.FirstOrDefaultAsync(u => u.Prix == decimal.Parse(prix));
        }

        public async Task AddAsync(Panier entity)
        {
            await s221UberContext.Paniers.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task AddProduitPanierAsync(int panierId, int produitId, int etablissementId)
        {
            var panier = await s221UberContext.Paniers.FindAsync(panierId);
            if (panier == null)
            {
                throw new Exception("Panier introuvable !");
            }

            var produit = await s221UberContext.Produits.FindAsync(produitId);
            if (produit == null)
            {
                throw new Exception("Produit introuvable !");
            }

            var etablissement = await s221UberContext.Etablissements.FindAsync(etablissementId);
            if (etablissement == null)
            {
                throw new Exception("Établissement introuvable !");
            }

            // Récupérer le prix du produit depuis la table t_e_produit_pdt
            var prixProduit = produit.PrixProduit;  // Assure-toi que c'est bien dans cette table

            // Ajouter le produit à la table de jointure
            await s221UberContext.Database.ExecuteSqlRawAsync(
                "INSERT INTO t_j_contient2_c2 (pnr_id, pdt_id, etb_id, c2_quantite) VALUES ({0}, {1}, {2}, {3})",
                panierId, produitId, etablissementId, 1
            );

            // Recalculer et mettre à jour le prix du panier
            await s221UberContext.Database.ExecuteSqlRawAsync(
                "UPDATE t_e_panier_pnr SET pnr_prix = (SELECT SUM(p.pdt_prix * c2.c2_quantite) " +
                "FROM t_j_contient2_c2 c2 JOIN t_e_produit_pdt p ON c2.pdt_id = p.pdt_id " +
                "WHERE c2.pnr_id = {0}) WHERE pnr_id = {0}",
                panierId
            );

            await s221UberContext.SaveChangesAsync();
        }




        public async Task UpdateProduitPanierQuantiteAsync(int panierId, int produitId, int etablissementId, int quantite)
        {
            var panier = await s221UberContext.Paniers.FindAsync(panierId);
            if (panier == null)
            {
                throw new Exception("Panier introuvable !");
            }

            var produit = await s221UberContext.Produits.FindAsync(produitId);
            if (produit == null)
            {
                throw new Exception("Produit introuvable !");
            }

            var etablissement = await s221UberContext.Etablissements.FindAsync(etablissementId);
            if (etablissement == null)
            {
                throw new Exception("Établissement introuvable !");
            }

            // Récupérer le prix du produit depuis la table t_e_produit_pdt
            var prixProduit = produit.PrixProduit;  // Assure-toi que c'est bien dans cette table

            // Mettre à jour la quantité dans la table de jointure
            await s221UberContext.Database.ExecuteSqlRawAsync(
                "UPDATE t_j_contient2_c2 SET c2_quantite = {3} WHERE pnr_id = {0} AND pdt_id = {1} AND etb_id = {2}",
                panierId, produitId, etablissementId, quantite
            );

            // Recalculer et mettre à jour le prix du panier
            await s221UberContext.Database.ExecuteSqlRawAsync(
                "UPDATE t_e_panier_pnr SET pnr_prix = (SELECT SUM(p.pdt_prix * c2.c2_quantite) " +
                "FROM t_j_contient2_c2 c2 JOIN t_e_produit_pdt p ON c2.pdt_id = p.pdt_id " +
                "WHERE c2.pnr_id = {0}) WHERE pnr_id = {0}",
                panierId
            );

            await s221UberContext.SaveChangesAsync();
        }




        public async Task UpdateAsync(Panier newPanier, Panier entity)
        {
            s221UberContext.Entry(newPanier).State = EntityState.Modified;

            newPanier.IdPanier = entity.IdPanier;
            newPanier.IdClient = entity.IdClient;
            newPanier.Prix = entity.Prix;
            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Panier utilisateur)
        {
            s221UberContext.Paniers.Remove(utilisateur);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteProduitPanierAsync(int panierId, int produitId, int etablissementId)
        {
            var panier = await s221UberContext.Paniers.FindAsync(panierId);
            if (panier == null)
            {
                throw new Exception("Panier introuvable !");
            }

            var produit = await s221UberContext.Produits.FindAsync(produitId);
            if (produit == null)
            {
                throw new Exception("Produit introuvable !");
            }

            var etablissement = await s221UberContext.Etablissements.FindAsync(etablissementId);
            if (etablissement == null)
            {
                throw new Exception("Établissement introuvable !");
            }

            // Supprimer le produit du panier
            await s221UberContext.Database.ExecuteSqlRawAsync(
                "DELETE FROM t_j_contient2_c2 WHERE pnr_id = {0} AND pdt_id = {1} AND etb_id = {2}",
                panierId, produitId, etablissementId
            );

            // Recalculer et mettre à jour le prix du panier après suppression
            await s221UberContext.Database.ExecuteSqlRawAsync(
                "UPDATE t_e_panier_pnr SET pnr_prix = COALESCE(" +
                "(SELECT SUM(p.pdt_prix * c2.c2_quantite) " +
                "FROM t_j_contient2_c2 c2 JOIN t_e_produit_pdt p ON c2.pdt_id = p.pdt_id " +
                "WHERE c2.pnr_id = {0}), 0) WHERE pnr_id = {0}",
                panierId
            );


            await s221UberContext.SaveChangesAsync();
        }




    }
}
