using Microsoft.VisualStudio.TestTools.UnitTesting;
using UberApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.DataManager;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {


        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private ProduitsController _controller;
        private IDataRepository<Produit> _produitsRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _produitsRepository = new ProduitManager(_context);
            _controller = new ProduitsController(_produitsRepository);
        }


        [TestMethod()]
        public void GetProduitById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Produit produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "Menu Pepe Tenders",
                PrixProduit = 14.9m,
                ImageProduit = "https://tb-static.uber.com/prod/image-proc/processed_images/b9436aeeab6d9606426d9e2474a703e6/5954bcb006b10dbfd0bc160f6370faf3.jpeg",
                Description = "Grosse soirée qui s'annonce ?! On gères ça avec nos big tenders au choix + 1 accompagnement + ta sauce rien que pour toi.",
                Contient2s = [],
                IdCategories = [],
                IdEtablissements = [],
            };
        var mockRepository = new Mock<IDataRepository<Produit>>();
        mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(produit);
        var controller = new ProduitsController(mockRepository.Object);
        // Act
        var actionResult = controller.GetProduitAsync(1).Result;
        // Assert
        Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(produit, actionResult.Value as Produit);
        }




        [TestMethod()]
        public void GetProduitById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Produit produit = new Produit
            {
                
            };
            var mockRepository = new Mock<IDataRepository<Produit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((Produit)null);
            var controller = new ProduitsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetProduitAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }



        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        [TestMethod()]
        public void GetProduitById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.Produits.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetProduitAsync(expected.IdProduit).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

    }
}