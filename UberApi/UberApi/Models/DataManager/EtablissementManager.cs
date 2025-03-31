using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class EtablissementManager : IDataRepository<Etablissement>
    {
        readonly S221UberContext? s221UberContext;
        public EtablissementManager() { }

        public EtablissementManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<Etablissement>>> GetAllAsync()
        {
            return await s221UberContext.Etablissements
                .Include(a => a.IdAdresseNavigation)
                    .ThenInclude(n => n.IdVilleNavigation)
                        .ThenInclude(i => i.IdCodePostalNavigation)
                            .ThenInclude(o => o.IdPaysNavigation)
                .Include(e => e.IdRestaurateurNavigation)
                .Include(e => e.GestionEtablissements)
                .Include(e => e.Horaires)
                .Include(e => e.IdCategoriePrestations)
                .Include(e => e.IdProduits)
                .ToListAsync();
        }

        public async Task<ActionResult<Etablissement>> GetByIdAsync(int id)
        {
            return await s221UberContext.Etablissements
                .Include(a => a.IdAdresseNavigation)
                    .ThenInclude(n => n.IdVilleNavigation)
                        .ThenInclude(i => i.IdCodePostalNavigation)
                            .ThenInclude(o => o.IdPaysNavigation)
                .Include(e => e.IdRestaurateurNavigation)
                .Include(e => e.GestionEtablissements)
                .Include(e => e.Horaires)
                .Include(e => e.IdCategoriePrestations)
                .Include(e => e.IdProduits)
                .FirstOrDefaultAsync(u => u.IdEtablissement == id);
        }

        public async Task<ActionResult<Etablissement>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Etablissements.FirstOrDefaultAsync(u => u.NomEtablissement.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(Etablissement entity)
        {
            await s221UberContext.Etablissements.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Etablissement newEtablissement, Etablissement entity)
        {
            s221UberContext.Entry(newEtablissement).State = EntityState.Modified;
            newEtablissement.IdEtablissement = entity.IdEtablissement;
            newEtablissement.IdRestaurateur = entity.IdRestaurateur;
            newEtablissement.TypeEtablissement = entity.TypeEtablissement;
            newEtablissement.IdAdresse = entity.IdAdresse;
            newEtablissement.NomEtablissement = entity.NomEtablissement;
            newEtablissement.Description = entity.Description;
            newEtablissement.ImageEtablissement = entity.ImageEtablissement;
            newEtablissement.Livraison = entity.Livraison;
            newEtablissement.AEmporter = entity.AEmporter;

            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Etablissement etablissement)
        {
            s221UberContext.Etablissements.Remove(etablissement);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
