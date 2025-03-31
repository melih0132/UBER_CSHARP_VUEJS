using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class LieuFavoriManager : IDataRepository<LieuFavori>
    {
        readonly S221UberContext? s221UberContext;
        public LieuFavoriManager() { }
        public LieuFavoriManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<LieuFavori>>> GetAllAsync()
        {
            return await s221UberContext.LieuFavoris.Include(a => a.IdAdresseNavigation).ThenInclude(n => n.IdVilleNavigation).ThenInclude(i => i.IdCodePostalNavigation).ThenInclude(o => o.IdPaysNavigation).ToListAsync();
        }
        public async Task<ActionResult<LieuFavori>> GetByIdAsync(int id)
        {
            return await s221UberContext.LieuFavoris.Include(a => a.IdAdresseNavigation).ThenInclude(n => n.IdVilleNavigation).ThenInclude(i => i.IdCodePostalNavigation).ThenInclude(o => o.IdPaysNavigation).FirstOrDefaultAsync(u => u.IdLieuFavori == id);
        }
        public async Task<ActionResult<LieuFavori>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.LieuFavoris.FirstOrDefaultAsync(u => u.NomLieu.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(LieuFavori entity)
        {
            await s221UberContext.LieuFavoris.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(LieuFavori newLieuFavori, LieuFavori entity)
        {
            s221UberContext.Entry(newLieuFavori).State = EntityState.Modified;
            newLieuFavori.IdLieuFavori = entity.IdLieuFavori;
            newLieuFavori.IdClient = entity.IdClient;
            newLieuFavori.IdAdresse = entity.IdAdresse;
            newLieuFavori.NomLieu = entity.NomLieu;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(LieuFavori lieuFavori)
        {
            s221UberContext.LieuFavoris.Remove(lieuFavori);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
