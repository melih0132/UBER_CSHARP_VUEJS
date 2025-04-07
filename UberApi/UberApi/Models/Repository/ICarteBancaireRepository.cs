using Microsoft.AspNetCore.Mvc;
using UberApi.Models.EntityFramework;

namespace UberApi.Models.Repository
{
    public interface ICarteBancaireRepository : IDataRepository<CarteBancaire>
    {
        Task AddClientsCBAsync(CarteBancaire carteBancaire, int clientId);
    }
}
