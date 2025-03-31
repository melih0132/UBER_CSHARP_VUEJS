using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class TypePrestationManager : IDataRepository<TypePrestation>
    {

        readonly S221UberContext? s221UberContext;
        public TypePrestationManager() { }
        public TypePrestationManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypePrestation>>> GetAllAsync()
        {
            return await s221UberContext.TypePrestations.ToListAsync();
        }
        public async Task<ActionResult<TypePrestation>> GetByIdAsync(int id)
        {
            return await s221UberContext.TypePrestations.FirstOrDefaultAsync(u => u.IdPrestation == id);
        }
        public async Task<ActionResult<TypePrestation>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.TypePrestations.FirstOrDefaultAsync(u => u.LibellePrestation.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(TypePrestation entity)
        {
            await s221UberContext.TypePrestations.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypePrestation newTypePrestation, TypePrestation entity)
        {
            s221UberContext.Entry(newTypePrestation).State = EntityState.Modified;
            newTypePrestation.IdPrestation = entity.IdPrestation;
            newTypePrestation.LibellePrestation = entity.LibellePrestation;
            newTypePrestation.DescriptionPrestation = entity.DescriptionPrestation;
            newTypePrestation.ImagePrestation = entity.ImagePrestation;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypePrestation codePostal)
        {
            s221UberContext.TypePrestations.Remove(codePostal);
            await s221UberContext.SaveChangesAsync();
        }
    }
}
