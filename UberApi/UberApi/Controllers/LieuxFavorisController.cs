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
    public class LieuxFavorisController : ControllerBase
    {
        private readonly IDataRepository<LieuFavori> dataRepository;

        public LieuxFavorisController(IDataRepository<LieuFavori> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<LieuFavori>>> GetLieuxFavorisAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LieuFavori>> GetLieuFavoriAsync(int id)
        {
            var lieuFavori = await dataRepository.GetByIdAsync(id);

            if (lieuFavori.Value == null)
            {
                return NotFound();
            }
            return lieuFavori;

        }


        [HttpGet]
        [Route("[action]/{nomLieu}")]
        [ActionName("GetByNomLieuFavori")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LieuFavori>> GetLieuFavoriByNomLieuFavoriAsync(string nomLieu)
        {
            var lieuFavori = await dataRepository.GetByStringAsync(nomLieu);
            if (lieuFavori.Value == null)
            {
                return NotFound();
            }
            return lieuFavori;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutLieuFavoriAsync(int id, LieuFavori lieuFavori)
        {
            if (id != lieuFavori.IdLieuFavori)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, lieuFavori);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LieuFavori>> PostLieuFavoriAsync(LieuFavori lieuFavori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(lieuFavori);
            return CreatedAtAction("GetById", new { id = lieuFavori.IdLieuFavori }, lieuFavori); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLieuFavoriAsync(int id)
        {
            var lieuFavori = await dataRepository.GetByIdAsync(id);
            if (lieuFavori.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(lieuFavori.Value);
            return NoContent();
        }
    }
}
