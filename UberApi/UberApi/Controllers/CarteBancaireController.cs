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
    public class CarteBancairesController : ControllerBase
    {
        private readonly IDataRepository<CarteBancaire> dataRepository;

        public CarteBancairesController(IDataRepository<CarteBancaire> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetCarteBancairesAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarteBancaire>> GetCarteBancaireAsync(int id)
        {
            var carteBancaire = await dataRepository.GetByIdAsync(id);

            if (carteBancaire.Value == null)
            {
                return NotFound();
            }
            return carteBancaire;

        }


        [HttpGet]
        [Route("[action]/{numeroCb}")]
        [ActionName("GetByLibelleCarteBancaire")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarteBancaire>> GetCBByNumeroCbCarteBancaireAsync(string numeroCb)
        {
            var carteBancaire = await dataRepository.GetByStringAsync(numeroCb);
            if (carteBancaire.Value == null)
            {
                return NotFound();
            }
            return carteBancaire;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCarteBancaireAsync(int id, CarteBancaire carteBancaire)
        {
            if (id != carteBancaire.IdCb)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, carteBancaire);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarteBancaire>> PostCarteBancaireAsync(CarteBancaire carteBancaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(carteBancaire);
            return CreatedAtAction("GetById", new { id = carteBancaire.IdCb }, carteBancaire); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCarteBancaireAsync(int id)
        {
            var carteBancaire = await dataRepository.GetByIdAsync(id);
            if (carteBancaire.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(carteBancaire.Value);
            return NoContent();
        }
    }
}
