using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using BCrypt.Net;
using System.Collections.Generic;

namespace UberApi.Models.DataManager
{
    public class ClientManager : IDataRepository<Client>
    {
        readonly S221UberContext? s221UberContext;

        public ClientManager() { }

        public ClientManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<Client>>> GetAllAsync()
        {
            return await s221UberContext.Clients
                .Include(c => c.Factures)
                .Include(c => c.IdAdresseNavigation)
                    .ThenInclude(a => a.IdVilleNavigation)
                        .ThenInclude(v => v.IdPaysNavigation)
                .Include(c => c.IdEntrepriseNavigation)
                .Include(c => c.LieuFavoris)
                .Include(c => c.Otps)
                .Include(c => c.Paniers)
                .Include(c => c.Reservations)
                .Include(c => c.IdCbs)
                .ToListAsync();
        }

        public async Task<ActionResult<Client>> GetByIdAsync(int id)
        {
            return await s221UberContext.Clients
                .Include(c => c.Factures)
                .Include(c => c.IdAdresseNavigation)
                    .ThenInclude(a => a.IdVilleNavigation)
                        .ThenInclude(v => v.IdPaysNavigation)
                .Include(c => c.IdEntrepriseNavigation)
                .Include(c => c.LieuFavoris)
                .Include(c => c.Otps)
                .Include(c => c.Paniers)
                .Include(c => c.Reservations)
                .Include(c => c.IdCbs)
                .FirstOrDefaultAsync(u => u.IdClient == id);
        }

        public async Task<ActionResult<Client>> GetByStringAsync(string mail)
        {
            return await s221UberContext.Clients.FirstOrDefaultAsync(u => u.EmailUser.ToUpper() == mail.ToUpper());
        }

        //public async Task AddAsync(Client entity)
        //{
        //    entity.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
        //    await s221UberContext.Clients.AddAsync(entity);
        //    await s221UberContext.SaveChangesAsync();
        //}

        public async Task AddAsync(Client entity)
        {
            // Vérification de l'unicité de l'email avant insertion
            var existingClient = await s221UberContext.Clients
                .FirstOrDefaultAsync(u => u.EmailUser.ToUpper() == entity.EmailUser.ToUpper());


            if (existingClient != null)
            {
                throw new Exception("Cet email est déjà utilisé.");
            }

            // Hachage du mot de passe
            entity.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);

            // Ajout à la base de données
            await s221UberContext.Clients.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();

            var entityClient = await s221UberContext.Clients.FirstOrDefaultAsync(u => u.EmailUser.ToUpper() == entity.EmailUser.ToUpper());

            await s221UberContext.Database.ExecuteSqlRawAsync(
            "INSERT INTO public.t_e_panier_pnr(pnr_id, clt_id, pnr_prix) VALUES({0},{1},{2})",
            entityClient.IdClient, entity.IdClient, 0);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client newClient, Client entity)
        {
            s221UberContext.Entry(newClient).State = EntityState.Modified;

            newClient.IdClient = entity.IdClient;
            newClient.IdEntreprise = entity.IdEntreprise;
            newClient.IdAdresse = entity.IdAdresse;
            newClient.GenreUser = entity.GenreUser;
            newClient.NomUser = entity.NomUser;
            newClient.PrenomUser = entity.PrenomUser;
            newClient.DateNaissance = entity.DateNaissance;
            newClient.Telephone = entity.Telephone;
            newClient.EmailUser = entity.EmailUser;

            if (!BCrypt.Net.BCrypt.Verify(entity.MotDePasseUser, newClient.MotDePasseUser))
            {
                newClient.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
            }

            newClient.PhotoProfile = entity.PhotoProfile;
            newClient.SouhaiteRecevoirBonPlan = entity.SouhaiteRecevoirBonPlan;
            newClient.MfaActivee = entity.MfaActivee;
            newClient.TypeClient = entity.TypeClient;
            newClient.DemandeSuppression = entity.DemandeSuppression;

            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client utilisateur)
        {
            s221UberContext.Clients.Remove(utilisateur);
            await s221UberContext.SaveChangesAsync();
        }
    }
}
