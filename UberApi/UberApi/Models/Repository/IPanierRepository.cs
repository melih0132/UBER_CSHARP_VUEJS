using UberApi.Models.EntityFramework;

namespace UberApi.Models.Repository
{

    public interface IPanierRepository : IDataRepository<Panier>
    {
        Task AddProduitPanierAsync(int panierId, int produitId, int etablissementId);

        Task UpdateProduitPanierQuantiteAsync(int panierId, int produitId, int etablissementId, int quantite);

    }

}
