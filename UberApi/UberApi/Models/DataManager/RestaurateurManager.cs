using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using BCrypt.Net;

namespace UberApi.Models.DataManager
{


    public class RestaurateurManager : IDataRepository<Restaurateur>
    {
        readonly S221UberContext? s221UberContext;

        public RestaurateurManager() { }

        public RestaurateurManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<Restaurateur>>> GetAllAsync()
        {
            return await s221UberContext.Restaurateurs.ToListAsync();
        }

        public async Task<ActionResult<Restaurateur>> GetByIdAsync(int id)
        {
            return await s221UberContext.Restaurateurs.FirstOrDefaultAsync(u => u.IdRestaurateur == id);
        }

        public async Task<ActionResult<Restaurateur>> GetByStringAsync(string mail)
        {
            return await s221UberContext.Restaurateurs.FirstOrDefaultAsync(u => u.EmailUser.ToUpper() == mail.ToUpper());
        }

        public async Task AddAsync(Restaurateur entity)
        {
            entity.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
            await s221UberContext.Restaurateurs.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurateur newRestaurateur, Restaurateur entity)
        {
            s221UberContext.Entry(newRestaurateur).State = EntityState.Modified;
            newRestaurateur.IdRestaurateur = entity.IdRestaurateur;
            newRestaurateur.NomUser = entity.NomUser;
            newRestaurateur.PrenomUser = entity.PrenomUser;
            newRestaurateur.Telephone = entity.Telephone;
            newRestaurateur.EmailUser = entity.EmailUser;

            if (!BCrypt.Net.BCrypt.Verify(entity.MotDePasseUser, newRestaurateur.MotDePasseUser))
            {
                newRestaurateur.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
            }

            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Restaurateur restaurateur)
        {
            s221UberContext.Restaurateurs.Remove(restaurateur);
            await s221UberContext.SaveChangesAsync();
        }
    }
}
