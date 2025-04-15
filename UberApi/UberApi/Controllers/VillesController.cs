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
    public class VillesController : ControllerBase
    {
        private readonly IDataRepository<Ville> dataRepository;

        public VillesController(IDataRepository<Ville> dataRepo)
        {
            dataRepository = dataRepo;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ville>>> GetVillesAsync()
        {
            return await dataRepository.GetAllAsync();
        }





       
    }
}
