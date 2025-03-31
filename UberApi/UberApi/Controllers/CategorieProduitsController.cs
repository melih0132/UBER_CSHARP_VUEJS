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
    public class CategorieProduitsController : ControllerBase
    {
        private readonly IDataRepository<CategorieProduit> dataRepository;

        public CategorieProduitsController(IDataRepository<CategorieProduit> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategorieProduit>>> GetCategorieProduitsAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategorieProduit>> GetCategorieProduitAsync(int id)
        {
            var categorieProduit = await dataRepository.GetByIdAsync(id);

            if (categorieProduit.Value == null)
            {
                return NotFound();
            }
            return categorieProduit;

        }


        [HttpGet]
        [Route("[action]/{nomCategorieProduit}")]
        [ActionName("GetByNomCategorieProduit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategorieProduit>> GetCategorieProduitByNomCategorieProduitAsync(string nomCategorieProduit)
        {
            var categorieProduit = await dataRepository.GetByStringAsync(nomCategorieProduit);
            if (categorieProduit.Value == null)
            {
                return NotFound();
            }
            return categorieProduit;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategorieProduitAsync(int id, CategorieProduit categorieProduit)
        {
            if (id != categorieProduit.IdCategorie)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, categorieProduit);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategorieProduit>> PostCategorieProduitAsync(CategorieProduit categorieProduit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(categorieProduit);
            return CreatedAtAction("GetById", new { id = categorieProduit.IdCategorie }, categorieProduit); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategorieProduitAsync(int id)
        {
            var categorieProduit = await dataRepository.GetByIdAsync(id);
            if (categorieProduit.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(categorieProduit.Value);
            return NoContent();
        }
    }
}
