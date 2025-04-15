using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.DataManager;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using UberApi.Models.Repository;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtablissementsController : ControllerBase
    {
        private readonly IDataRepository<Etablissement> dataRepository;
        private readonly IDataRepository<Adresse> dataRepositoryAdresse;
        private readonly IDataRepository<Ville> dataRepositoryVille;

        public EtablissementsController(IDataRepository<Etablissement> dataRepo, IDataRepository<Adresse> data, IDataRepository<Ville> dataVille)
        {
            dataRepository = dataRepo;
            dataRepositoryAdresse = data;
            dataRepositoryVille = dataVille;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etablissement>>> GetEtablissementsAsync()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Etablissement>> GetEtablissementAsync(int id)
        {
            var etablissement = await dataRepository.GetByIdAsync(id);

            if (etablissement.Value == null)
            {
                return NotFound();
            }
            return etablissement;

        }

        [HttpGet]
        [Route("[action]/{nomEtablissement}")]
        [ActionName("GetByNomEtablissement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Etablissement>> GetEtablissementByNomEtablissementAsync(string nomEtablissement)
        {
            var etablissement = await dataRepository.GetByStringAsync(nomEtablissement);
            if (etablissement.Value == null)
            {
                return NotFound();
            }
            return etablissement;
        }


        [HttpGet]
        [Route("[action]/{idEtablissement}")]
        [ActionName("GetAdresseByIdEtablissement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Adresse>> GetAdresseByIdEtablissementAsync(int idEtablissement)
        {
            // Obtenez l'établissement
            var etablissement = await dataRepository.GetByIdAsync(idEtablissement);
            if (etablissement == null || etablissement.Value == null)
            {
                Console.WriteLine($"Etablissement not found for ID: {idEtablissement}");
                return NotFound("Etablissement not found.");
            }

            // Obtenez l'adresse associée à l'établissement
            var idAdresse = etablissement.Value.IdAdresse;
            var adresse = await dataRepositoryAdresse.GetByIdAsync(idAdresse);
            if (adresse == null || adresse.Value == null)
            {
                Console.WriteLine($"Adresse not found for ID: {idAdresse}");
                return NotFound("Adresse not found.");
            }

            // Vérifiez si l'adresse a une ville associée
            var idVille = adresse.Value.IdVille;
            if (!idVille.HasValue)
            {
                Console.WriteLine($"Ville ID not found for address ID: {idAdresse}");
                return NotFound("Ville ID not found for the address.");
            }

            // Obtenez la ville associée à l'adresse
            var ville = await dataRepositoryVille.GetByIdAsync(idVille.Value);
            if (ville == null || ville.Value == null)
            {
                Console.WriteLine($"Ville not found for ID: {idVille.Value}");
                return NotFound("Ville not found.");
            }

            // Retournez le nom de la ville
            return Ok(ville.Value.NomVille);
        }
    }
}
