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
    public class VillesController : ControllerBase
    {
        private readonly IDataRepository<Ville> dataRepository;

        public VillesController(IDataRepository<Ville> dataRepo)
        {
            dataRepository = dataRepo;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ville>>> GetVillesAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ville>> GetVilleAsync(int id)
        {
            var ville = await dataRepository.GetByIdAsync(id);

            if (ville.Value == null)
            {
                return NotFound();
            }
            return ville;

        }


        [HttpGet]
        [Route("[action]/{nomVille}")]
        [ActionName("GetByNomVille")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ville>> GetVilleByNomVilleAsync(string nomVille)
        {
            var ville = await dataRepository.GetByStringAsync(nomVille);
            if (ville.Value == null)
            {
                return NotFound();
            }
            return ville;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVilleAsync(int id, Ville ville)
        {
            if (id != ville.IdVille)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, ville);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ville>> PostVilleAsync(Ville ville)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(ville);
            return CreatedAtAction("GetById", new { id = ville.IdVille }, ville); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilleAsync(int id)
        {
            var ville = await dataRepository.GetByIdAsync(id);
            if (ville.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(ville.Value);
            return NoContent();
        }
    }
}
