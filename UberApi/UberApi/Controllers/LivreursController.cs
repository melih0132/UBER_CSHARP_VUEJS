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
using Microsoft.AspNetCore.Authorization;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LivreursController : ControllerBase
    {
        private readonly IDataRepository<Livreur> dataRepository;

        public LivreursController(IDataRepository<Livreur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livreur>>> GetLivreursAsync()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Livreur>> GetLivreurAsync(int id)
        {
            var livreur = await dataRepository.GetByIdAsync(id);

            if (livreur.Value == null)
            {
                return NotFound();
            }
            return livreur;

        }

        [HttpGet]
        [Route("[action]/{libelleLivreur}")]
        [ActionName("GetByLibelleLivreur")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Livreur>> GetLivreurByEmailLivreurAsync(string libelleLivreur)
        {
            var livreur = await dataRepository.GetByStringAsync(libelleLivreur);
            if (livreur.Value == null)
            {
                return NotFound();
            }
            return livreur;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutLivreurAsync(int id, Livreur livreur)
        {
            if (id != livreur.IdLivreur)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, livreur);
                return NoContent();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Livreur>> PostLivreurAsync(Livreur livreur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(livreur);
            return CreatedAtAction("GetById", new { id = livreur.IdLivreur }, livreur);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLivreurAsync(int id)
        {
            var livreur = await dataRepository.GetByIdAsync(id);
            if (livreur.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(livreur.Value);
            return NoContent();
        }
    }
}
