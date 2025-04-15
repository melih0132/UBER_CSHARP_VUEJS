using Microsoft.VisualStudio.TestTools.UnitTesting;
using UberApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using UberApi.Models.DataManager;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using System.Net.Sockets;

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class CarteBancairesControllerTests
    {
        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private CarteBancairesController _controller;
        private ICarteBancaireRepository _carteBancairesRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _carteBancairesRepository = new CarteBancaireManager(_context);
            _controller = new CarteBancairesController(_carteBancairesRepository);
        }

        [TestMethod()]
        public void GetCarteBancaireById_ExistingIdPassed_AreEqual_AvecMoq()
        {


            CarteBancaire carteBancaire = new CarteBancaire
            {
                IdCb = 1,
                NumeroCb =  "1234567812345678",
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme ="123",
                TypeCarte ="Crédit",
                TypeReseaux= "MasterCard",
                Courses= [],
                IdClients= []
            };
            var mockRepository = new Mock<ICarteBancaireRepository>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(carteBancaire);
            var controller = new CarteBancairesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCarteBancaireAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(carteBancaire, actionResult.Value as CarteBancaire);
        }

        [TestMethod()]
        public void GetCarteBancaireById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            CarteBancaire carteBancaire = new CarteBancaire
            {
                IdCb = 89,
                NumeroCb = "1234567812345679",
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };
            var mockRepository = new Mock<ICarteBancaireRepository>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((CarteBancaire)null);
            var controller = new CarteBancairesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCarteBancaireAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCarteBancaireByNumeroCarteVTC_ExistingIdPassed_AreEqual_AvecMoq()
        {
            CarteBancaire carteBancaire = new CarteBancaire
            {
                IdCb = 1,
                NumeroCb = "1234567812345678",
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };
            var mockRepository = new Mock<ICarteBancaireRepository>();
            mockRepository.Setup(x => x.GetByStringAsync(carteBancaire.NumeroCb).Result).Returns(carteBancaire);
            var controller = new CarteBancairesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCBByNumeroCbCarteBancaireAsync("1234567812345678").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(carteBancaire, actionResult.Value as CarteBancaire);
        }

        [TestMethod]
        public void GetCarteBancaireByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            CarteBancaire carteBancaire = new CarteBancaire
            {
                IdCb = 1,
                NumeroCb = "1234567812345679",
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };

            var mockRepository = new Mock<ICarteBancaireRepository>();
            mockRepository.Setup(x => x.GetByStringAsync(carteBancaire.NumeroCb).Result).Returns((CarteBancaire)null);
            var controller = new CarteBancairesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCBByNumeroCbCarteBancaireAsync("1234567812345679").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


      


        [TestMethod]
        public void DeleteCarteBancaire_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange : Création du carteBancaire
            var carteBancaire = new CarteBancaire
            {
                IdCb = 1,
                NumeroCb = "1234567812345679",
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };

            var mockRepository = new Mock<ICarteBancaireRepository>();


            mockRepository.Setup(x => x.GetByIdAsync(carteBancaire.IdCb))
                           .ReturnsAsync(carteBancaire);


            mockRepository.Setup(x => x.DeleteAsync(carteBancaire));

            var controller = new CarteBancairesController(mockRepository.Object);

            // Act : Suppression
            var actionResult = controller.DeleteCarteBancaireAsync(carteBancaire.IdCb).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));


            mockRepository.Verify(x => x.GetByIdAsync(carteBancaire.IdCb), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.Is<CarteBancaire>(c => c.IdCb == carteBancaire.IdCb)), Times.Once);
        }



        [TestMethod]
        public void DeleteCarteBancaire_NotValideIdPassed_ReturnsNotFound_AvecMoq()
        {
            // Arrange : ID inexistant
            int idCarteBancaireInvalide = 19;

            var mockRepository = new Mock<ICarteBancaireRepository>();

            mockRepository.Setup(x => x.GetByIdAsync(idCarteBancaireInvalide))
                           .ReturnsAsync((CarteBancaire)null);  // Retourner null pour simuler que le carteBancaire n'existe pas

            var controller = new CarteBancairesController(mockRepository.Object);

            var actionResult = controller.DeleteCarteBancaireAsync(idCarteBancaireInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            mockRepository.Verify(x => x.GetByIdAsync(idCarteBancaireInvalide), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<CarteBancaire>()), Times.Never);
        }


        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>



        [TestMethod()]
        public void GetCarteBancaireById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.CarteBancaires.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetCarteBancaireAsync(expected.IdCb).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }


        [TestMethod]
        public void GetCarteBancaireByNumeroCarteVTC_ExistingIdPassed_AreEqual_SansMoq()
        {
            var numeroCb = "1234567812345678";
            var expected = _context.CarteBancaires.FirstOrDefault(u => u.NumeroCb.ToUpper() == numeroCb.ToUpper());
            
            // Act
            var actionResult = _controller.GetCBByNumeroCbCarteBancaireAsync(numeroCb).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

        [TestMethod]
        public void GetCarteBancaireByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_SansMoq()
        {
            
            var numeroCb = "1255555555555579";
            var expected = _context.CarteBancaires.FirstOrDefault(u => u.NumeroCb.ToUpper() == numeroCb.ToUpper());
            // Act
            var actionResult = _controller.GetCBByNumeroCbCarteBancaireAsync(numeroCb).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }






        [TestMethod]
        public void DeleteCarteBancaire_WhenValidIdPassed_ShouldReturnNoContent()
        {

            Random rnd = new Random();
            string chiffreCarteNew = "";
            for (int i = 0; i < 16; i++)
            {
                chiffreCarteNew += rnd.Next(0, 10);
            }



            CarteBancaire cousierATester = new CarteBancaire()
            {

                NumeroCb = chiffreCarteNew,
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };
            var idClient = 1;
            var result = _controller.PostCarteBancaireAsync(cousierATester, idClient).Result;

            // Act : Suppression
            var actionResult = _controller.DeleteCarteBancaireAsync(cousierATester.IdCb).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));


        }



        [TestMethod]
        public void DeleteCarteBancaire_NotValideIdPassed_ReturnsNotFound_SansMoq()
        {
            // Arrange : ID inexistant

            int idCarteBancaireInvalide = 500;

            var actionResult = _controller.DeleteCarteBancaireAsync(idCarteBancaireInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }
    }
}