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
    public class AdressesControllerTests
    {
        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private AdressesController _controller;
        private IDataRepository<Adresse> _adressesRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _adressesRepository = new AdresseManager(_context);
            _controller = new AdressesController(_adressesRepository);
        }

        [TestMethod()]
        public void GetAdresseById_ExistingIdPassed_AreEqual_AvecMoq()
        {


            Adresse adresse = new Adresse
            {
                IdAdresse = 1,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Poste",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers=  [],
                Entreprises=  [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };
            var mockRepository = new Mock<IDataRepository<Adresse>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(adresse);
            var controller = new AdressesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetAdresseAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(adresse, actionResult.Value as Adresse);
        }

        [TestMethod()]
        public void GetAdresseById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Adresse adresse = new Adresse
            {
                IdAdresse = 89,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Poste",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };
            var mockRepository = new Mock<IDataRepository<Adresse>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((Adresse)null);
            var controller = new AdressesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetAdresseAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAdresseByNumeroCarteVTC_ExistingIdPassed_AreEqual_AvecMoq()
        {
            Adresse adresse = new Adresse
            {
                IdAdresse = 1,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Poste",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };
            var mockRepository = new Mock<IDataRepository<Adresse>>();
            mockRepository.Setup(x => x.GetByStringAsync("13 Rue De La Poste").Result).Returns(adresse);
            var controller = new AdressesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetAdresseByLibelleAdresseAsync("13 Rue De La Poste").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(adresse, actionResult.Value as Adresse);
        }

        [TestMethod]
        public void GetAdresseByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Adresse adresse = new Adresse
            {
                IdAdresse = 1,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Posteeeeeeeee",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };

            var mockRepository = new Mock<IDataRepository<Adresse>>();
            mockRepository.Setup(x => x.GetByStringAsync("13 Rue De La Posteeeeeeeee").Result).Returns((Adresse)null);
            var controller = new AdressesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetAdresseByLibelleAdresseAsync("13 Rue De La Posteeeeeeeee").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostAdresse_ValideIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var adresse = new Adresse
            {
                IdAdresse = 10,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Poste",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };

            var mockRepository = new Mock<IDataRepository<Adresse>>();

            mockRepository.Setup(x => x.AddAsync(It.IsAny<Adresse>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == adresse.IdAdresse)))
                           .ReturnsAsync(adresse);

            var controller = new AdressesController(mockRepository.Object);

            // Act
            var actionResult = controller.PostAdresseAsync(adresse).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Adresse>));
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);
            var ress = result.Value as Adresse;
            Assert.IsNotNull(ress);
            Assert.AreEqual(adresse.IdAdresse, ress.IdAdresse);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<Adresse>()), Times.Once);
        }


        [TestMethod]
        public void PutAdresse_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var adresse = new Adresse
            {
                IdAdresse = 1,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Poste",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };

            var adresseUpdate = new Adresse
            {
                IdAdresse = 1,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Postedfdfdfdfdf",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };

            var mockRepository = new Mock<IDataRepository<Adresse>>();


            mockRepository.Setup(x => x.GetByIdAsync(adresse.IdAdresse)).ReturnsAsync(adresseUpdate);


            mockRepository.Setup(x => x.UpdateAsync(adresse, adresseUpdate));

            var controller = new AdressesController(mockRepository.Object);

            // Act
            var actionResult = controller.PutAdresseAsync(adresseUpdate.IdAdresse, adresseUpdate).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Adresse>(c => c.IdAdresse == adresseUpdate.IdAdresse), adresseUpdate), Times.Once);
        }

        [TestMethod]
        public void DeleteAdresse_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange : Création du adresse
            var adresse = new Adresse
            {
                IdAdresse = 1,
                IdVille = 6,
                LibelleAdresse = "13 Rue De La Poste",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };

            var mockRepository = new Mock<IDataRepository<Adresse>>();


            mockRepository.Setup(x => x.GetByIdAsync(adresse.IdAdresse))
                           .ReturnsAsync(adresse);


            mockRepository.Setup(x => x.DeleteAsync(adresse));

            var controller = new AdressesController(mockRepository.Object);

            // Act : Suppression
            var actionResult = controller.DeleteAdresseAsync(adresse.IdAdresse).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));


            mockRepository.Verify(x => x.GetByIdAsync(adresse.IdAdresse), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.Is<Adresse>(c => c.IdAdresse == adresse.IdAdresse)), Times.Once);
        }



        [TestMethod]
        public void DeleteAdresse_NotValideIdPassed_ReturnsNotFound_AvecMoq()
        {
            // Arrange : ID inexistant
            int idAdresseInvalide = 19;

            var mockRepository = new Mock<IDataRepository<Adresse>>();

            mockRepository.Setup(x => x.GetByIdAsync(idAdresseInvalide))
                           .ReturnsAsync((Adresse)null);  // Retourner null pour simuler que le adresse n'existe pas

            var controller = new AdressesController(mockRepository.Object);

            var actionResult = controller.DeleteAdresseAsync(idAdresseInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            mockRepository.Verify(x => x.GetByIdAsync(idAdresseInvalide), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Adresse>()), Times.Never);
        }


        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [TestMethod()]
        public void GetAdresses_SansMoq()
        {

            List<Adresse> expected = _context.Adresses.ToList();


            // Act
            var actionResult = _controller.GetAdressesAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }

        [TestMethod()]
        public void GetAdresseById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.Adresses.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetAdresseAsync(expected.IdAdresse).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }


        [TestMethod]
        public void GetAdresseByNumeroCarteVTC_ExistingIdPassed_AreEqual_SansMoq()
        {
            var libelleAdresse = "13 Rue De La Poste";
            var expected = _context.Adresses.FirstOrDefault(u => u.LibelleAdresse == libelleAdresse);

            // Act
            var actionResult = _controller.GetAdresseByLibelleAdresseAsync(libelleAdresse).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

        [TestMethod]
        public void GetAdresseByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_SansMoq()
        {

            var libelleAdresse = "13 Rue De La Posteeeeeeee";
            var expected = _context.Adresses.FirstOrDefault(u => u.LibelleAdresse == libelleAdresse);

            // Act
            var actionResult = _controller.GetAdresseByLibelleAdresseAsync(libelleAdresse).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostAdresse_ValideIdPassed_ReturnsRightItem_SansMoq()
        {
            // Arrange
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 100000);


            Adresse cousierATester = new Adresse()
            {

                IdVille = 6,
                LibelleAdresse = chiffre + " Rue De La TEST",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };
            // Act
            try
            {
                var result = _controller.PostAdresseAsync(cousierATester).Result;
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
            Adresse? userRecupere = _context.Adresses.Where(u => u.LibelleAdresse.ToUpper() ==
            cousierATester.LibelleAdresse.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            cousierATester.IdAdresse = userRecupere.IdAdresse;
            Assert.AreEqual(userRecupere, cousierATester, "Utilisateurs pas identiques");

        }


        [TestMethod]
        public void PutAdresse_ValideIdPassed_ReturnsNoContent_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 100000);

            // Étape 1 : Créer une adresse et l'ajouter en base
            Adresse cousierATester = new Adresse()
            {
                IdAdresse = 5,
                IdVille = 6,
                LibelleAdresse = chiffre +" Rue De La TEST",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };

            int adresseUpdate = cousierATester.IdAdresse;


            var actionResult = _controller.PutAdresseAsync(adresseUpdate, cousierATester).Result;

            // Assert
            Adresse? cousierRecupere = _context.Adresses
                .Where(u => u.LibelleAdresse.ToUpper() == cousierATester.LibelleAdresse.ToUpper())
                .FirstOrDefault();

            Assert.IsNotNull(cousierRecupere, "Le adresse n'a pas été trouvé en base après la mise à jour");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
            Assert.AreEqual(chiffre + " Rue De La TEST", cousierRecupere.LibelleAdresse, "Le numéro de rue n'a pas été mis à jour correctement.");

        }
        [TestMethod]
        public void DeleteAdresse_ValideIdPassed_ReturnsNoContent_SansMoq()
        {
            var adresse = _controller.GetAdressesAsync().Result.Value.Max(c => c.IdAdresse);
            int derniereId = adresse + 20;


            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Adresse cousierATester = new Adresse()
            {

                IdVille = 6,
                LibelleAdresse = derniereId + " Rue De La TEST",
                Latitude = "45.9014696",
                Longitude = "6.1226563",
                Clients = [],
                CourseAdrIdAdresseNavigations = [],
                CourseIdAdresseNavigations = [],
                Coursiers = [],
                Entreprises = [],
                Etablissements = [],
                IdVilleNavigation = null,
                LieuFavoris = [],
                Velos = []
            };

            var result = _controller.PostAdresseAsync(cousierATester).Result;


            // Act : Suppression
            var actionResult = _controller.DeleteAdresseAsync(cousierATester.IdAdresse).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));




        }



        [TestMethod]
        public void DeleteAdresse_NotValideIdPassed_ReturnsNotFound_SansMoq()
        {
            // Arrange : ID inexistant

            int idAdresseInvalide = 500;

            var actionResult = _controller.DeleteAdresseAsync(idAdresseInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }
    }
}