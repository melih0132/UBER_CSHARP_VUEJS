using Microsoft.VisualStudio.TestTools.UnitTesting;
using UberApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using Moq;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.DataManager;

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class CommandesControllerTests
    {
        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private CommandesController _controller;
        private IDataRepository<Commande> _commandesRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _commandesRepository = new CommandeManager(_context);
            _controller = new CommandesController(_commandesRepository);
        }

        [TestMethod()]
        public void GetCommandeById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Commande commande = new Commande
            {
                IdCommande = 1,
                IdPanier= 1,
                IdLivreur = 1,
                IdCb = 1,
                IdAdresse = 1,
                PrixCommande = 30.5m,
                TempsCommande = 25,
                HeureCreation = DateTime.Parse("2025-03-17T00:00:00"),
                HeureCommande = DateTime.Parse("2025-03-13T00:00:00"),
                EstLivraison = false,
                StatutCommande = "En attente",
                RefusDemandee = false,
                RemboursementEffectue = false,
                Factures = [],
                IdLivreurNavigation = null,
                IdPanierNavigation = null
            };
            var mockRepository = new Mock<IDataRepository<Commande>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(commande);
            var controller = new CommandesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCommandeAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(commande, actionResult.Value as Commande);
        }




        [TestMethod()]
        public void GetCommandeById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Commande commande = new Commande
            {
                IdCommande = 89,
                IdPanier= 1,
                IdLivreur = 1,
                IdCb = 1,
                IdAdresse = 1,
                PrixCommande = 30.5m,
                TempsCommande = 25,
                HeureCreation = DateTime.Parse("2025-03-17T00:00:00"),
                HeureCommande = DateTime.Parse("2025-03-13T00:00:00"),
                EstLivraison = false,
                StatutCommande = "En attente",
                RefusDemandee = false,
                RemboursementEffectue = false,
                Factures = [],
                IdLivreurNavigation = null,
                IdPanierNavigation = null
            };
            var mockRepository = new Mock<IDataRepository<Commande>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((Commande)null);
            var controller = new CommandesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCommandeAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCommandeByStatut_ExistingIdPassed_AreEqual_AvecMoq()
        {
            Commande commande = new Commande
            {
                IdCommande = 1,
                IdPanier = 1,
                IdLivreur = 1,
                IdCb = 1,
                IdAdresse = 1,
                PrixCommande = 30.5m,
                TempsCommande = 25,
                HeureCreation = DateTime.Parse("2025-03-17T00:00:00"),
                HeureCommande = DateTime.Parse("2025-03-13T00:00:00"),
                EstLivraison = false,
                StatutCommande = "En attente",
                RefusDemandee = false,
                RemboursementEffectue = false,
                Factures = [],
                IdLivreurNavigation = null,
                IdPanierNavigation = null
            };
            var mockRepository = new Mock<IDataRepository<Commande>>();
            mockRepository.Setup(x => x.GetByStringAsync("En attente").Result).Returns(commande);
            var controller = new CommandesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetUtilisateurByEmailAsync("En attente").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(commande, actionResult.Value as Commande);
        }

        [TestMethod]
        public void GetCommandeByStatut_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Commande commande = new Commande
            {
                IdCommande = 1,
                IdPanier = 1,
                IdLivreur = 1,
                IdCb = 1,
                IdAdresse = 1,
                PrixCommande = 30.5m,
                TempsCommande = 25,
                HeureCreation = DateTime.Parse("2025-03-17T00:00:00"),
                HeureCommande = DateTime.Parse("2025-03-13T00:00:00"),
                EstLivraison = false,
                StatutCommande = "En attenfdfdfdfdte",
                RefusDemandee = false,
                RemboursementEffectue = false,
                Factures = [],
                IdLivreurNavigation = null,
                IdPanierNavigation = null
            };

            var mockRepository = new Mock<IDataRepository<Commande>>();
            mockRepository.Setup(x => x.GetByStringAsync("En attenfdfdfdfdte").Result).Returns((Commande)null);
            var controller = new CommandesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetUtilisateurByEmailAsync("En attenfdfdfdfdte").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }



        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [TestMethod()]
        public void GetCommandes_SansMoq()
        {

            List<Commande> expected = _context.Commandes.ToList();


            // Act
            var actionResult = _controller.GetCommandesAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }

        [TestMethod()]
        public void GetCommandeById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.Commandes.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetCommandeAsync(expected.IdCommande).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }


        [TestMethod]
        public void GetCommandeByNumeroCarteVTC_ExistingIdPassed_AreEqual_SansMoq()
        {
            var statut = "En attente";
            var expected = _context.Commandes.FirstOrDefault(u => u.StatutCommande.ToUpper() == statut.ToUpper());

            // Act
            var actionResult = _controller.GetUtilisateurByEmailAsync(statut).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

        [TestMethod]
        public void GetCommandeByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_SansMoq()
        {

            var statut = "En attenfdfdfdfdte";
            var expected = _context.Commandes.FirstOrDefault(u => u.StatutCommande.ToUpper() == statut.ToUpper());
            // Act
            var actionResult = _controller.GetUtilisateurByEmailAsync(statut).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

    }
}