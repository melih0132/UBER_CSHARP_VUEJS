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
    public class CategoriePrestationsController : ControllerBase
    {
        private readonly IDataRepository<CategoriePrestation> dataRepository;

        public CategoriePrestationsController(IDataRepository<CategoriePrestation> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriePrestation>>> GetCategoriePrestationsAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriePrestation>> GetCategoriePrestationAsync(int id)
        {
            var categoriePrestation = await dataRepository.GetByIdAsync(id);

            if (categoriePrestation.Value == null)
            {
                return NotFound();
            }
            return categoriePrestation;

        }


        [HttpGet]
        [Route("[action]/{libelleCategoriePrestation}")]
        [ActionName("GetByLibelleCategoriePrestation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriePrestation>> GetCategoriePrestationByLibelleCategoriePrestationAsync(string libelleCategoriePrestation)
        {
            var categoriePrestation = await dataRepository.GetByStringAsync(libelleCategoriePrestation);
            if (categoriePrestation.Value == null)
            {
                return NotFound();
            }
            return categoriePrestation;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategoriePrestationAsync(int id, CategoriePrestation categoriePrestation)
        {
            if (id != categoriePrestation.IdCategoriePrestation)
            {
                return BadRequest();
            }
            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, categoriePrestation);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriePrestation>> PostCategoriePrestationAsync(CategoriePrestation categoriePrestation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(categoriePrestation);
            return CreatedAtAction("GetById", new { id = categoriePrestation.IdCategoriePrestation }, categoriePrestation); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoriePrestationAsync(int id)
        {
            var categoriePrestation = await dataRepository.GetByIdAsync(id);
            if (categoriePrestation.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(categoriePrestation.Value);
            return NoContent();
        }
    }
}
