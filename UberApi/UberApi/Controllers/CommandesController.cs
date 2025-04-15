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
    public class CommandesController : Controller
    {
        private readonly IDataRepository<Commande> dataRepository;

        public CommandesController(IDataRepository<Commande> dataRepo)
        {
            dataRepository = dataRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandesAsync()
        {
            return await dataRepository.GetAllAsync();
        }



        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Commande>> GetCommandeAsync(int id)
        {
            var commande = await dataRepository.GetByIdAsync(id);

            if (commande.Value == null)
            {
                return NotFound();
            }
            return commande;

        }


        [HttpGet]
        [Route("[action]/{statut}")]
        [ActionName("GetByStatut")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Commande>> GetUtilisateurByEmailAsync(string statut)
        {
            var utilisateur = await dataRepository.GetByStringAsync(statut);
            if (utilisateur.Value == null)
            {




                return NotFound();
            }
            return utilisateur;
        }


    }
}
