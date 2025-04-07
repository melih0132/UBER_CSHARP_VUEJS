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
        public void PostCarteBancaire_ValideIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var carteBancaire = new CarteBancaire
            {
                IdCb = 10,
                NumeroCb = "1234567812345679",
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };

            var mockRepository = new Mock<ICarteBancaireRepository>();

            mockRepository.Setup(x => x.AddAsync(It.IsAny<CarteBancaire>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == carteBancaire.IdCb)))
                           .ReturnsAsync(carteBancaire);

            var controller = new CarteBancairesController(mockRepository.Object);

            // Act
            var idClient = 20;
            var actionResult = controller.PostCarteBancaireAsync(carteBancaire, idClient).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CarteBancaire>));
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);
            var ress = result.Value as CarteBancaire;
            Assert.IsNotNull(ress);
            Assert.AreEqual(carteBancaire.IdCb, ress.IdCb);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<CarteBancaire>()), Times.Once);
        }


        [TestMethod]
        public void PutCarteBancaire_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange
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

            var carteBancaireUpdate = new CarteBancaire
            {
                IdCb = 1,
                NumeroCb = "1234567812341111",
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };

            var mockRepository = new Mock<ICarteBancaireRepository>();


            mockRepository.Setup(x => x.GetByIdAsync(carteBancaire.IdCb)).ReturnsAsync(carteBancaireUpdate);


            mockRepository.Setup(x => x.UpdateAsync(carteBancaire, carteBancaireUpdate));

            var controller = new CarteBancairesController(mockRepository.Object);

            // Act
            var actionResult = controller.PutCarteBancaireAsync(carteBancaireUpdate.IdCb, carteBancaireUpdate).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

            mockRepository.Verify(x => x.UpdateAsync(It.Is<CarteBancaire>(c => c.IdCb == carteBancaireUpdate.IdCb), carteBancaireUpdate), Times.Once);
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
        public void GetCarteBancaires_SansMoq()
        {

            List<CarteBancaire> expected = _context.CarteBancaires.ToList();


            // Act
            var actionResult = _controller.GetCarteBancairesAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }

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
        public void PostCarteBancaire_ValideIdPassed_ReturnsRightItem_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            string chiffreCarteNew = "";
            for (int i = 0; i < 16; i++)
            {
                chiffreCarteNew += rnd.Next(1, 9);
            }




            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
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
            // Act
            try
            {
                var idClient = 20;
                var result = _controller.PostCarteBancaireAsync(cousierATester, idClient).Result;
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    Console.WriteLine($"Inner Exception: {inner.Message}");
                    if (inner.InnerException != null)
                        Console.WriteLine($"Detailed Inner Exception: {inner.InnerException.Message}");
                }
                throw;
            } // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            CarteBancaire? userRecupere = _context.CarteBancaires.Where(u => u.NumeroCb.ToUpper() ==
            cousierATester.NumeroCb.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            cousierATester.IdCb = userRecupere.IdCb;
            Assert.AreEqual(userRecupere, cousierATester, "cb pas identiques");

        }


        [TestMethod]
        public void PutCarteBancaire_ValideIdPassed_ReturnsNoContent_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            string chiffreCarteNew = "";
            for (int i = 0; i < 16; i++)
            {
                chiffreCarteNew += rnd.Next(1, 9);
            }


            // Étape 1 : Créer un carteBancaire et l'ajouter en base
            CarteBancaire cousierATester = new CarteBancaire()
            {
                IdCb = 3,
                NumeroCb = chiffreCarteNew,
                DateExpireCb = DateOnly.Parse("2025-12-31"),
                Cryptogramme = "123",
                TypeCarte = "Crédit",
                TypeReseaux = "MasterCard",
                Courses = [],
                IdClients = []
            };

            int carteBancaireUpdate = cousierATester.IdCb;


            var actionResult = _controller.PutCarteBancaireAsync(carteBancaireUpdate, cousierATester).Result;

            // Assert
            CarteBancaire? cousierRecupere = _context.CarteBancaires
                .Where(u => u.NumeroCb.ToUpper() == cousierATester.NumeroCb.ToUpper())
                .FirstOrDefault();

            Assert.IsNotNull(cousierRecupere, "Le carteBancaire n'a pas été trouvé en base après la mise à jour");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
            Assert.AreEqual(chiffreCarteNew, cousierRecupere.NumeroCb, "Le numéro de carte n'a pas été mis à jour correctement.");

        }
        [TestMethod]
        public void DeleteCarteBancaire_ValideIdPassed_ReturnsNoContent_SansMoq()
        {

            Random rnd = new Random();
            string chiffreCarteNew = "";
            for (int i = 0; i < 16; i++)
            {
                chiffreCarteNew += rnd.Next(1, 9);
            }




            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
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
            var idClient = 20;
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