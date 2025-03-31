using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{


    public class CodePostalManager : IDataRepository<CodePostal>
    {
        readonly S221UberContext? s221UberContext;
        public CodePostalManager() { }
        public CodePostalManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<CodePostal>>> GetAllAsync()
        {
            return await s221UberContext.CodePostals.ToListAsync();
        }
        public async Task<ActionResult<CodePostal>> GetByIdAsync(int id)
        {
            return await s221UberContext.CodePostals.FirstOrDefaultAsync(u => u.IdCodePostal == id);
        }
        public async Task<ActionResult<CodePostal>> GetByStringAsync(string libelle)
        {
            return await s221UberContext.CodePostals.FirstOrDefaultAsync(u => u.CP.ToUpper() == libelle.ToUpper());
        }

        public async Task AddAsync(CodePostal entity)
        {
            await s221UberContext.CodePostals.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(CodePostal newCodePostal, CodePostal entity)
        {
            s221UberContext.Entry(newCodePostal).State = EntityState.Modified;
            newCodePostal.IdCodePostal = entity.IdCodePostal;
            newCodePostal.IdPays = entity.IdPays;
            newCodePostal.CP = entity.CP;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(CodePostal codePostal)
        {
            s221UberContext.CodePostals.Remove(codePostal);
            await s221UberContext.SaveChangesAsync();
        }
    }

}
