using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using BCrypt.Net;

namespace UberApi.Models.DataManager
{
    public class CoursierManager : IDataRepository<Coursier>
    {
        readonly S221UberContext? s221UberContext;

        public CoursierManager() { }

        public CoursierManager(S221UberContext context)
        {
            s221UberContext = context;
        }

        public async Task<ActionResult<IEnumerable<Coursier>>> GetAllAsync()
        {
            return await s221UberContext.Coursiers.ToListAsync();
        }

        public async Task<ActionResult<Coursier>> GetByIdAsync(int id)
        {
            return await s221UberContext.Coursiers.FirstOrDefaultAsync(u => u.IdCoursier == id);
        }

        public async Task<ActionResult<Coursier>> GetByStringAsync(string numeroCarteVtc)
        {
            return await s221UberContext.Coursiers.FirstOrDefaultAsync(u => u.NumeroCarteVtc.ToUpper() == numeroCarteVtc.ToUpper());
        }

        public async Task AddAsync(Coursier entity)
        {
            entity.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
            await s221UberContext.Coursiers.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Coursier newCoursier, Coursier entity)
        {
            s221UberContext.Entry(newCoursier).State = EntityState.Modified;
            newCoursier.IdCoursier = entity.IdCoursier;
            newCoursier.IdEntreprise = entity.IdEntreprise;
            newCoursier.IdAdresse = entity.IdAdresse;
            newCoursier.GenreUser = entity.GenreUser;
            newCoursier.NomUser = entity.NomUser;
            newCoursier.PrenomUser = entity.PrenomUser;
            newCoursier.DateNaissance = entity.DateNaissance;
            newCoursier.Telephone = entity.Telephone;
            newCoursier.EmailUser = entity.EmailUser;

            if (!BCrypt.Net.BCrypt.Verify(entity.MotDePasseUser, newCoursier.MotDePasseUser))
            {
                newCoursier.MotDePasseUser = BCrypt.Net.BCrypt.HashPassword(entity.MotDePasseUser);
            }

            await s221UberContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Coursier utilisateur)
        {
            s221UberContext.Coursiers.Remove(utilisateur);
            await s221UberContext.SaveChangesAsync();
        }
    }
}
