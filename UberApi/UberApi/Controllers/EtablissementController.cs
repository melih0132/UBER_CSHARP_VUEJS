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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEtablissementAsync(int id, Etablissement etablissement)
        {
            if (id != etablissement.IdEtablissement)
            {
                return BadRequest();
            }
            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, etablissement);
                return NoContent();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Etablissement>> PostEtablissementAsync(Etablissement etablissement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(etablissement);
            return CreatedAtAction("GetById", new { id = etablissement.IdEtablissement }, etablissement);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEtablissementAsync(int id)
        {
            var etablissement = await dataRepository.GetByIdAsync(id);
            if (etablissement.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(etablissement.Value);
            return NoContent();
        }


        [HttpGet]
        [Route("[action]/{idEtablissement}")]
        [ActionName("GetAdresseByIdEtablissement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Adresse>> GetAdresseByIdEtablissementAsync(int idEtablissement)
        {
            var etablissement = await dataRepository.GetByIdAsync(idEtablissement);

            if (etablissement.Value == null)
            {
                return NotFound();
            }

            var idadresse = etablissement.Value.IdAdresse;

            var adresse = await dataRepositoryAdresse.GetByIdAsync(idadresse);

            if (adresse.Value == null)
            {
                return NotFound();
            }

            var idville = adresse.Value.IdVille;

            if (!idville.HasValue)
            {
                return NotFound(); 
            }

            var ville = await dataRepositoryVille.GetByIdAsync(idville.Value);

            if (ville.Value == null)
            {
                return NotFound();
            }

            return Ok(ville.Value.NomVille);
        }

    }
}
