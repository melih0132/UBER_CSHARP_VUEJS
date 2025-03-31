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
    public class CodesPostauxController : ControllerBase
    {
        private readonly IDataRepository<CodePostal> dataRepository;

        public CodesPostauxController(IDataRepository<CodePostal> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodePostal>>> GetCodesPostauxAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CodePostal>> GetCodePostalAsync(int id)
        {
            var codePostal = await dataRepository.GetByIdAsync(id);

            if (codePostal.Value == null)
            {
                return NotFound();
            }
            return codePostal;

        }


        [HttpGet]
        [Route("[action]/{cP}")]
        [ActionName("GetByCodePostal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CodePostal>> GetCodePostalByCodePostalAsync(string cP)
        {
            var codePostal = await dataRepository.GetByStringAsync(cP);
            if (codePostal.Value == null)
            {
                return NotFound();
            }
            return codePostal;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCodePostalAsync(int id, CodePostal codePostal)
        {
            if (id != codePostal.IdCodePostal)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, codePostal);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CodePostal>> PostCodePostalAsync(CodePostal codePostal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(codePostal);
            return CreatedAtAction("GetById", new { id = codePostal.IdCodePostal }, codePostal); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCodePostalAsync(int id)
        {
            var codePostal = await dataRepository.GetByIdAsync(id);
            if (codePostal.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(codePostal.Value);
            return NoContent();
        }
    }
}
