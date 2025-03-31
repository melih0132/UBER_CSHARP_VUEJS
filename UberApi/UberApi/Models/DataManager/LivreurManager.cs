using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using BCrypt.Net;

namespace UberApi.Models.DataManager
{


    public class LivreurManager : IDataRepository<Livreur>
    {
        readonly S221UberContext? s221UberContext;

        public LivreurManager() { }

        public LivreurManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<Livreur>>> GetAllAsync()
        {
            return await s221UberContext.Livreurs.ToListAsync();
        }

        public async Task<ActionResult<Livreur>> GetByIdAsync(int id)
        {
            return await s221UberContext.Livreurs.FirstOrDefaultAsync(u => u.IdLivreur == id);
        }

        public async Task<ActionResult<Livreur>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.Livreurs.FirstOrDefaultAsync(u => u.EmailUser.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(Livreur entity)
        {
            entity.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
            await s221UberContext.Livreurs.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Livreur newLivreur, Livreur entity)
        {
            s221UberContext.Entry(newLivreur).State = EntityState.Modified;
            newLivreur.IdLivreur = entity.IdLivreur;
            newLivreur.IdEntreprise = entity.IdEntreprise;
            newLivreur.IdAdresse = entity.IdAdresse;
            newLivreur.GenreUser = entity.GenreUser;
            newLivreur.NomUser = entity.NomUser;
            newLivreur.PrenomUser = entity.PrenomUser;
            newLivreur.DateNaissance = entity.DateNaissance;
            newLivreur.Telephone = entity.Telephone;
            newLivreur.EmailUser = entity.EmailUser;

            if (!BCrypt.Net.BCrypt.Verify(entity.MotDePasseUser, newLivreur.MotDePasseUser))
            {
                newLivreur.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
            }

            newLivreur.Iban = entity.Iban;
            newLivreur.DateDebutActivite = entity.DateDebutActivite;
            newLivreur.NoteMoyenne = entity.NoteMoyenne;

            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Livreur utilisateur)
        {
            s221UberContext.Livreurs.Remove(utilisateur);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
