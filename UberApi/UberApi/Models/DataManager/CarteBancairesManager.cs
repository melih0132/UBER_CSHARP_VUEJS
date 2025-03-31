using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class CarteBancaireManager : IDataRepository<CarteBancaire>
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
            return await s221UberContext.CarteBancaires.FirstOrDefaultAsync(u => u.NumeroCb.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(CarteBancaire entity)
        {
            await s221UberContext.CarteBancaires.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
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
            s221UberContext.CarteBancaires.Remove(cb);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
