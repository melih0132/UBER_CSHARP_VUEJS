using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePrestationsController : Controller
    {
        private readonly IDataRepository<TypePrestation> dataRepository;

        public TypePrestationsController(IDataRepository<TypePrestation> dataRepo)
        {
            dataRepository = dataRepo;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypePrestation>>> GetTypePrestationsAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypePrestation>> GetTypePrestationAsync(int id)
        {
            var typePrestation = await dataRepository.GetByIdAsync(id);

            if (typePrestation.Value == null)
            {
                return NotFound();
            }
            return typePrestation;

        }


    }
}
