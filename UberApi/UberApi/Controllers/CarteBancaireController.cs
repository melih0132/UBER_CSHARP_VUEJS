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
        private readonly ICarteBancaireRepository dataRepository;


        public CarteBancairesController(ICarteBancaireRepository dataRepo)
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



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarteBancaire>> PostCarteBancaireAsync(CarteBancaire carteBancaire, int clientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await dataRepository.AddClientsCBAsync(carteBancaire, clientId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return CreatedAtAction("GetById", new { id = carteBancaire.IdCb }, carteBancaire);
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
