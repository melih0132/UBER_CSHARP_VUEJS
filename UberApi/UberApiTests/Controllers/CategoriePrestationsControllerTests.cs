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

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class CategoriePrestationsControllerTests
    {
        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private CategoriePrestationsController _controller;
        private IDataRepository<CategoriePrestation> _categoriePrestationsRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _categoriePrestationsRepository = new CategoriePrestationManager(_context);
            _controller = new CategoriePrestationsController(_categoriePrestationsRepository);
        }

        [TestMethod()]
        public void GetCategoriePrestationById_ExistingIdPassed_AreEqual_AvecMoq()
        {


            CategoriePrestation categoriePrestation = new CategoriePrestation
            {
                IdCategoriePrestation = 1,
                LibelleCategoriePrestation = "Fast food",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements =  []
            };
            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(categoriePrestation);
            var controller = new CategoriePrestationsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCategoriePrestationAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(categoriePrestation, actionResult.Value as CategoriePrestation);
        }

        [TestMethod()]
        public void GetCategoriePrestationById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            CategoriePrestation categoriePrestation = new CategoriePrestation
            {
                IdCategoriePrestation = 89,
                LibelleCategoriePrestation = "Fast food",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };
            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((CategoriePrestation)null);
            var controller = new CategoriePrestationsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCategoriePrestationAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCategoriePrestationByNumeroCarteVTC_ExistingIdPassed_AreEqual_AvecMoq()
        {
            CategoriePrestation categoriePrestation = new CategoriePrestation
            {
                IdCategoriePrestation = 1,
                LibelleCategoriePrestation = "Fast food",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };
            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();
            mockRepository.Setup(x => x.GetByStringAsync("Fast food").Result).Returns(categoriePrestation);
            var controller = new CategoriePrestationsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCategoriePrestationByLibelleCategoriePrestationAsync("Fast food").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(categoriePrestation, actionResult.Value as CategoriePrestation);
        }

        [TestMethod]
        public void GetCategoriePrestationByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            CategoriePrestation categoriePrestation = new CategoriePrestation
            {
                IdCategoriePrestation = 1,
                LibelleCategoriePrestation = "Fast fodddddddod",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };

            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();
            mockRepository.Setup(x => x.GetByStringAsync("Fast fodddddddod").Result).Returns((CategoriePrestation)null);
            var controller = new CategoriePrestationsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCategoriePrestationByLibelleCategoriePrestationAsync("Fast fodddddddod").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostCategoriePrestation_ValideIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var categoriePrestation = new CategoriePrestation
            {
                IdCategoriePrestation = 10,
                LibelleCategoriePrestation = "Fast food",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };

            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();

            mockRepository.Setup(x => x.AddAsync(It.IsAny<CategoriePrestation>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == categoriePrestation.IdCategoriePrestation)))
                           .ReturnsAsync(categoriePrestation);

            var controller = new CategoriePrestationsController(mockRepository.Object);

            // Act
            var actionResult = controller.PostCategoriePrestationAsync(categoriePrestation).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CategoriePrestation>));
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);
            var ress = result.Value as CategoriePrestation;
            Assert.IsNotNull(ress);
            Assert.AreEqual(categoriePrestation.IdCategoriePrestation, ress.IdCategoriePrestation);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<CategoriePrestation>()), Times.Once);
        }


        [TestMethod]
        public void PutCategoriePrestation_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var categoriePrestation = new CategoriePrestation
            {
                IdCategoriePrestation = 1,
                LibelleCategoriePrestation = "Fast food",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };

            var categoriePrestationUpdate = new CategoriePrestation
            {
                IdCategoriePrestation = 1,
                LibelleCategoriePrestation = "Fast fodddddddod",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };

            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();


            mockRepository.Setup(x => x.GetByIdAsync(categoriePrestation.IdCategoriePrestation)).ReturnsAsync(categoriePrestationUpdate);


            mockRepository.Setup(x => x.UpdateAsync(categoriePrestation, categoriePrestationUpdate));

            var controller = new CategoriePrestationsController(mockRepository.Object);

            // Act
            var actionResult = controller.PutCategoriePrestationAsync(categoriePrestationUpdate.IdCategoriePrestation, categoriePrestationUpdate).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

            mockRepository.Verify(x => x.UpdateAsync(It.Is<CategoriePrestation>(c => c.IdCategoriePrestation == categoriePrestationUpdate.IdCategoriePrestation), categoriePrestationUpdate), Times.Once);
        }

        [TestMethod]
        public void DeleteCategoriePrestation_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange : Création du categoriePrestation
            var categoriePrestation = new CategoriePrestation
            {
                IdCategoriePrestation = 1,
                LibelleCategoriePrestation = "Fast food",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };

            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();


            mockRepository.Setup(x => x.GetByIdAsync(categoriePrestation.IdCategoriePrestation))
                           .ReturnsAsync(categoriePrestation);


            mockRepository.Setup(x => x.DeleteAsync(categoriePrestation));

            var controller = new CategoriePrestationsController(mockRepository.Object);

            // Act : Suppression
            var actionResult = controller.DeleteCategoriePrestationAsync(categoriePrestation.IdCategoriePrestation).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));


            mockRepository.Verify(x => x.GetByIdAsync(categoriePrestation.IdCategoriePrestation), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.Is<CategoriePrestation>(c => c.IdCategoriePrestation == categoriePrestation.IdCategoriePrestation)), Times.Once);
        }



        [TestMethod]
        public void DeleteCategoriePrestation_NotValideIdPassed_ReturnsNotFound_AvecMoq()
        {
            // Arrange : ID inexistant
            int idCategoriePrestationInvalide = 400;

            var mockRepository = new Mock<IDataRepository<CategoriePrestation>>();

            mockRepository.Setup(x => x.GetByIdAsync(idCategoriePrestationInvalide))
                           .ReturnsAsync((CategoriePrestation)null);  // Retourner null pour simuler que le categoriePrestation n'existe pas

            var controller = new CategoriePrestationsController(mockRepository.Object);

            var actionResult = controller.DeleteCategoriePrestationAsync(idCategoriePrestationInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            mockRepository.Verify(x => x.GetByIdAsync(idCategoriePrestationInvalide), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<CategoriePrestation>()), Times.Never);
        }


        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [TestMethod()]
        public void GetCategoriePrestations_SansMoq()
        {

            List<CategoriePrestation> expected = _context.CategoriePrestations.ToList();


            // Act
            var actionResult = _controller.GetCategoriePrestationsAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }

        [TestMethod()]
        public void GetCategoriePrestationById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.CategoriePrestations.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetCategoriePrestationAsync(expected.IdCategoriePrestation).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }


        [TestMethod]
        public void GetCategoriePrestationByNumeroCarteVTC_ExistingIdPassed_AreEqual_SansMoq()
        {
            
            var libelleCategoriePrestation = "Fast food";
            var expected = _context.CategoriePrestations.FirstOrDefault(u => u.LibelleCategoriePrestation.ToUpper() == libelleCategoriePrestation.ToUpper());

            // Act
            var actionResult = _controller.GetCategoriePrestationByLibelleCategoriePrestationAsync(libelleCategoriePrestation).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

        [TestMethod]
        public void GetCategoriePrestationByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_SansMoq()
        {

            var libelleCategoriePrestation = "Fast foodooo";
            var expected = _context.CategoriePrestations.FirstOrDefault(u => u.LibelleCategoriePrestation.ToUpper() == libelleCategoriePrestation.ToUpper());
            // Act
            var actionResult = _controller.GetCategoriePrestationByLibelleCategoriePrestationAsync(libelleCategoriePrestation).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostCategoriePrestation_ValideIdPassed_ReturnsRightItem_SansMoq()
        {
            Random rnd = new Random();
            string chiffreNew = "";
            for (int i = 0; i < 10; i++)
            {
                chiffreNew += rnd.Next(1, 9);
            }
            CategoriePrestation cousierATester = new CategoriePrestation()
            {

                LibelleCategoriePrestation = "Fast foodoo" + chiffreNew,
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };
            // Act
            try
            {
                var result = _controller.PostCategoriePrestationAsync(cousierATester).Result;
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
            CategoriePrestation? userRecupere = _context.CategoriePrestations.Where(u => u.LibelleCategoriePrestation.ToUpper() ==
            cousierATester.LibelleCategoriePrestation.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            cousierATester.IdCategoriePrestation = userRecupere.IdCategoriePrestation;
            Assert.AreEqual(userRecupere, cousierATester, "Utilisateurs pas identiques");

        }


        [TestMethod]
        public void PutCategoriePrestation_ValideIdPassed_ReturnsNoContent_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            // Étape 1 : Créer un categoriePrestation et l'ajouter en base
            CategoriePrestation cousierATester = new CategoriePrestation()
            {
                IdCategoriePrestation = 20,
                LibelleCategoriePrestation = "Fast food" + chiffre,
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };

            int categoriePrestationUpdate = cousierATester.IdCategoriePrestation;



            var actionResult = _controller.PutCategoriePrestationAsync(categoriePrestationUpdate, cousierATester).Result;

            // Assert
            CategoriePrestation? cousierRecupere = _context.CategoriePrestations
                .Where(u => u.LibelleCategoriePrestation.ToUpper() == cousierATester.LibelleCategoriePrestation.ToUpper())
                .FirstOrDefault();

            Assert.IsNotNull(cousierRecupere, "Le categoriePrestation n'a pas été trouvé en base après la mise à jour");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
            Assert.AreEqual("Fast food" + chiffre, cousierRecupere.LibelleCategoriePrestation, "Le libelle n'a pas été mis à jour correctement.");

        }
        [TestMethod]
        public void DeleteCategoriePrestation_ValideIdPassed_ReturnsNoContent_SansMoq()
        {

            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            CategoriePrestation cousierATester = new CategoriePrestation()
            {

                LibelleCategoriePrestation = "Fast food",
                DescriptionCategoriePrestation = null,
                ImageCategoriePrestation = null,
                IdEtablissements = []
            };

            var result = _controller.PostCategoriePrestationAsync(cousierATester).Result;


            // Act : Suppression
            var actionResult = _controller.DeleteCategoriePrestationAsync(cousierATester.IdCategoriePrestation).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));




        }



        [TestMethod]
        public void DeleteCategoriePrestation_NotValideIdPassed_ReturnsNotFound_SansMoq()
        {
            // Arrange : ID inexistant

            int idCategoriePrestationInvalide = 19;

            var actionResult = _controller.DeleteCategoriePrestationAsync(idCategoriePrestationInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }
    }
}