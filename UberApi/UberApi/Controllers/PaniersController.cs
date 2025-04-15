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
    public class PaniersController : ControllerBase
    {
        private readonly IPanierRepository dataRepository;

        public PaniersController(IPanierRepository dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPaniersAsync()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Panier>> GetPanierAsync(int id)
        {
            var Panier = await dataRepository.GetByIdAsync(id);

            if (Panier.Value == null)
            {
                return NotFound();
            }
            return Panier;

        }

        [HttpGet]
        [Route("[action]/{prix}")]
        [ActionName("GetByLibellePrix")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Panier>> GetCBByNumeroCbPanierAsync(string prix)
        {
            var Panier = await dataRepository.GetByStringAsync(prix);
            if (Panier.Value == null)
            {
                return NotFound();
            }
            return Panier;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPanierProduitAsync(int id,int produitId, int etablissementId ,int quantite)
        {

                await dataRepository.UpdateProduitPanierQuantiteAsync(id,produitId, etablissementId,quantite);
                return NoContent();
            
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Panier>> PostPanierAsync(int id, int produitId, int etablissementId)
        {

            try
            {

                await dataRepository.AddProduitPanierAsync(id, produitId,etablissementId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePanierAsync(int id, int produitID,int etablissementID)
        {
            var Panier = await dataRepository.GetByIdAsync(id);

            if (Panier.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteProduitPanierAsync(id, produitID, etablissementID);
            return NoContent();
        }
    }
}
