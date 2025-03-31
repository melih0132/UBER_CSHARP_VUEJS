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
    public class FacturesController : ControllerBase
    {
        private readonly IDataRepository<Facture> dataRepository;

        public FacturesController(IDataRepository<Facture> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facture>>> GetFacturesAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Facture>> GetFactureAsync(int id)
        {
            var facture = await dataRepository.GetByIdAsync(id);

            if (facture.Value == null)
            {
                return NotFound();
            }
            return facture;

        }


        [HttpGet]
        [Route("[action]/{dateFacture}")]
        [ActionName("GetByDateFacture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Facture>>> GetByDateFactureAsync(string dateFacture)
        {
            var facture = await ((FactureManager)dataRepository).GetByDateAsync(dateFacture);
            if (facture.Value == null)
            {
                return NotFound();
            }
            return facture;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutFactureAsync(int id, Facture facture)
        {
            if (id != facture.IdFacture)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, facture);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Facture>> PostFactureAsync(Facture facture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(facture);
            return CreatedAtAction("GetById", new { id = facture.IdFacture }, facture); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFactureAsync(int id)
        {
            var facture = await dataRepository.GetByIdAsync(id);
            if (facture.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(facture.Value);
            return NoContent();
        }
    }
}
