using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class VehiculeManager : IDataRepository<Vehicule>
    {
        readonly S221UberContext? s221UberContext;
        public VehiculeManager() { }
        public VehiculeManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Vehicule>>> GetAllAsync()
        {
            return await s221UberContext.Vehicules.ToListAsync();
        }
        public async Task<ActionResult<Vehicule>> GetByIdAsync(int id)
        {
            return await s221UberContext.Vehicules.FirstOrDefaultAsync(u => u.IdVehicule == id);
        }
        public async Task<ActionResult<Vehicule>> GetByStringAsync(string immatriculation)
        {
            return await s221UberContext.Vehicules.FirstOrDefaultAsync(u => u.Immatriculation.ToUpper() == immatriculation.ToUpper());
        }

        public async Task AddAsync(Vehicule entity)
        {
            await s221UberContext.Vehicules.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Vehicule newVehicule, Vehicule entity)
        {
            s221UberContext.Entry(newVehicule).State = EntityState.Modified;
            newVehicule.IdVehicule = entity.IdVehicule;
            newVehicule.IdCoursier = entity.IdCoursier;
            newVehicule.Immatriculation = entity.Immatriculation;
            newVehicule.Marque = entity.Marque;
            newVehicule.Modele = entity.Modele;
            newVehicule.Capacite = entity.Capacite;
            newVehicule.AccepteAnimaux = entity.AccepteAnimaux;
            newVehicule.EstElectrique = entity.EstElectrique;
            newVehicule.EstConfortable = entity.EstConfortable;
            newVehicule.EstLuxueux = entity.EstLuxueux;
            newVehicule.EstRecent = entity.EstRecent;
            newVehicule.Couleur = entity.Couleur;
            newVehicule.StatusProcessusLogistique = entity.StatusProcessusLogistique;
            newVehicule.DemandeModification = entity.DemandeModification;
            newVehicule.DemandeModificationEffectue = entity.DemandeModificationEffectue;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Vehicule vehicule)
        {
            s221UberContext.Vehicules.Remove(vehicule);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
