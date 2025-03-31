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
    public class EntreprisesController : ControllerBase
    {
        private readonly IDataRepository<Entreprise> dataRepository;

        public EntreprisesController(IDataRepository<Entreprise> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetEntreprisesAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Entreprise>> GetEntrepriseAsync(int id)
        {
            var entreprise = await dataRepository.GetByIdAsync(id);

            if (entreprise.Value == null)
            {
                return NotFound();
            }
            return entreprise;

        }


        [HttpGet]
        [Route("[action]/{nomEntreprise}")]
        [ActionName("GetByNomEntreprise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Entreprise>> GetUtilisateurByLibelleEntrepriseAsync(string nomEntreprise)
        {
            var entreprise = await dataRepository.GetByStringAsync(nomEntreprise);
            if (entreprise.Value == null)
            {
                return NotFound();
            }
            return entreprise;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEntrepriseAsync(int id, Entreprise entreprise)
        {
            if (id != entreprise.IdEntreprise)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, entreprise);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Entreprise>> PostEntrepriseAsync(Entreprise entreprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(entreprise);
            return CreatedAtAction("GetById", new { id = entreprise.IdEntreprise }, entreprise); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEntrepriseAsync(int id)
        {
            var entreprise = await dataRepository.GetByIdAsync(id);
            if (entreprise.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(entreprise.Value);
            return NoContent();
        }
    }
}
