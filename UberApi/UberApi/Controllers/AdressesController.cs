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
    public class AdressesController : ControllerBase
    {
        private readonly IDataRepository<Adresse> dataRepository;

        public AdressesController(IDataRepository<Adresse> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdressesAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Adresse>> GetAdresseAsync(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);

            if (adresse.Value == null)
            {
                return NotFound();
            }
            return adresse;

        }


        [HttpGet]
        [Route("[action]/{libelleAdresse}")]
        [ActionName("GetByLibelleAdresse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Adresse>> GetAdresseByLibelleAdresseAsync(string libelleAdresse)
        {
            var adresse = await dataRepository.GetByStringAsync(libelleAdresse);
            if (adresse.Value == null)
            {
                return NotFound();
            }
            return adresse;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Adresse>> PostAdresseAsync(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(adresse);
            return CreatedAtAction("GetById", new { id = adresse.IdAdresse }, adresse); // GetById : nom de l’action
        }


    }
}
