using Microsoft.VisualStudio.TestTools.UnitTesting;
using UberApi.Controllers;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.DataManager;
using UberApi.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UberApi.Models.Repository;

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class EtablissementsControllerTests
    {
        private S221UberContext _context;
        private EtablissementsController _controller;
        private IDataRepository<Etablissement> _etablissementsRepository;
        private IDataRepository<Adresse> _adresseRepository;
        private IDataRepository<Ville> _villeRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>()
                .UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);

            if (!_context.Villes.Any())
            {
                var ville = new Ville { IdVille = 1, NomVille = "TestVille" };
                _context.Villes.Add(ville);
                _context.SaveChanges();
            }

            if (!_context.Adresses.Any())
            {
                var adresse = new Adresse { IdAdresse = 1, IdVille = 1, LibelleAdresse = "TestAdresse" };
                _context.Adresses.Add(adresse);
                _context.SaveChanges();
            }

            if (!_context.Etablissements.Any())
            {
                var etablissement = new Etablissement { IdEtablissement = 1, IdAdresse = 1, NomEtablissement = "TestEtablissement" };
                _context.Etablissements.Add(etablissement);
                _context.SaveChanges();
            }

            _etablissementsRepository = new EtablissementManager(_context);
            _adresseRepository = new AdresseManager(_context);
            _villeRepository = new VilleManager(_context);

            _controller = new EtablissementsController(_etablissementsRepository, _adresseRepository, _villeRepository);
        }

        /// <summary>
        /// AVEC MOQ
        /// </summary>

        [TestMethod()]
        public void GetEtablissementById_ExistingId_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var etablissement = new Etablissement { IdEtablissement = 1 };
            var mockRepository = new Mock<IDataRepository<Etablissement>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(etablissement);

            var controller = new EtablissementsController(mockRepository.Object, _adresseRepository, _villeRepository);

            // Act
            var result = controller.GetEtablissementAsync(1).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(etablissement, result.Value);
        }

        [TestMethod()]
        public void GetEtablissementById_NotExistingId_ReturnsNotFound_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Etablissement>>();
            mockRepository.Setup(x => x.GetByIdAsync(999).Result).Returns((Etablissement)null);

            var controller = new EtablissementsController(mockRepository.Object, _adresseRepository, _villeRepository);

            // Act
            var result = controller.GetEtablissementAsync(999).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetEtablissementByNom_ExistingName_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var etablissement = new Etablissement { NomEtablissement = "Chez Paul" };
            var mockRepository = new Mock<IDataRepository<Etablissement>>();
            mockRepository.Setup(x => x.GetByStringAsync("Chez Paul").Result)
                        .Returns(new ActionResult<Etablissement>(etablissement));

            var controller = new EtablissementsController(mockRepository.Object, _adresseRepository, _villeRepository);

            // Act
            var result = controller.GetEtablissementByNomEtablissementAsync("Chez Paul").Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Chez Paul", result.Value.NomEtablissement);
        }

        [TestMethod()]
        public void GetEtablissementByNom_NotExistingName_ReturnsNotFound_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Etablissement>>();
            mockRepository.Setup(x => x.GetByStringAsync("Inconnu").Result)
                        .Returns(new ActionResult<Etablissement>((Etablissement)null));

            var controller = new EtablissementsController(mockRepository.Object, _adresseRepository, _villeRepository);

            // Act
            var result = controller.GetEtablissementByNomEtablissementAsync("Inconnu").Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAdresseByIdEtablissement_ValidId_ReturnsVilleName_AvecMoq()
        {
            // Arrange
            var mockEtabRepo = new Mock<IDataRepository<Etablissement>>();
            var mockAdresseRepo = new Mock<IDataRepository<Adresse>>();
            var mockVilleRepo = new Mock<IDataRepository<Ville>>();

            var etablissement = new Etablissement { IdAdresse = 1 };
            var adresse = new Adresse { IdVille = 1 };
            var ville = new Ville { NomVille = "Paris" };

            mockEtabRepo.Setup(x => x.GetByIdAsync(1).Result).Returns(etablissement);
            mockAdresseRepo.Setup(x => x.GetByIdAsync(1).Result).Returns(adresse);
            mockVilleRepo.Setup(x => x.GetByIdAsync(1).Result).Returns(ville);

            var controller = new EtablissementsController(mockEtabRepo.Object, mockAdresseRepo.Object, mockVilleRepo.Object);

            // Act
            var result = controller.GetAdresseByIdEtablissementAsync(1).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>));
        }

        [TestMethod()]
        public void GetAdresseByIdEtablissement_InvalidId_ReturnsNotFound_AvecMoq()
        {
            // Arrange
            var mockEtabRepo = new Mock<IDataRepository<Etablissement>>();
            var mockAdresseRepo = new Mock<IDataRepository<Adresse>>();
            var mockVilleRepo = new Mock<IDataRepository<Ville>>();

            // Configurez le mock pour retourner null pour un ID invalide
            mockEtabRepo.Setup(x => x.GetByIdAsync(999).Result).Returns((Etablissement)null);

            var controller = new EtablissementsController(mockEtabRepo.Object, mockAdresseRepo.Object, mockVilleRepo.Object);

            // Act
            var result = controller.GetAdresseByIdEtablissementAsync(999).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        /// <summary>
        /// SANS MOQ
        /// </summary>

        [TestMethod()]
        public void GetEtablissementById_ExistingId_ReturnsRightItem_SansMoq()
        {
            // Arrange
            var expected = _context.Etablissements.FirstOrDefault();
            if (expected == null) return;

            // Act
            var result = _controller.GetEtablissementAsync(expected.IdEtablissement).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod()]
        public void GetEtablissementById_NotExistingId_ReturnsNotFound_SansMoq()
        {
            // Act
            var result = _controller.GetEtablissementAsync(999).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetEtablissementByNom_ExistingName_ReturnsRightItem_SansMoq()
        {
            // Arrange
            var expected = _context.Etablissements.FirstOrDefault();
            if (expected == null || string.IsNullOrEmpty(expected.NomEtablissement)) return;

            // Act
            var result = _controller.GetEtablissementByNomEtablissementAsync(expected.NomEtablissement).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expected.NomEtablissement, result.Value.NomEtablissement);
        }

        [TestMethod()]
        public void GetEtablissementByNom_NotExistingName_ReturnsNotFound_SansMoq()
        {
            // Act
            var result = _controller.GetEtablissementByNomEtablissementAsync("NomInexistant").Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public async Task GetAdresseByIdEtablissement_ValidId_ReturnsVilleName_SansMoq()
        {
            // Arrange
            var etablissement = _context.Etablissements
                .Include(e => e.IdAdresseNavigation)
                .ThenInclude(a => a.IdVilleNavigation)
                .FirstOrDefault();

            Assert.IsNotNull(etablissement, "No etablissement found in the context.");
            Assert.IsNotNull(etablissement.IdAdresseNavigation, "No adresse found for the etablissement.");
            Assert.IsNotNull(etablissement.IdAdresseNavigation.IdVilleNavigation, "No ville found for the adresse.");

            // Act
            var result = await _controller.GetAdresseByIdEtablissementAsync(etablissement.IdEtablissement);

            // Assert
            Assert.IsNotNull(result, "The result is null.");
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "The result is not an OkObjectResult.");

            // Extract the value from the OkObjectResult
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "The OkObjectResult is null.");
            Assert.IsNotNull(okResult.Value, "The value of the OkObjectResult is null.");

            // Check the value is a string and not empty
            var villeName = okResult.Value as string;
            Assert.IsNotNull(villeName, "The value is not a string.");
            Assert.IsFalse(string.IsNullOrEmpty(villeName), "The ville name is null or empty.");
        }

        [TestMethod()]
        public void GetAdresseByIdEtablissement_InvalidId_ReturnsNotFound_SansMoq()
        {
            // Act
            var result = _controller.GetAdresseByIdEtablissementAsync(999).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod()]
        public void GetAllEtablissements_ReturnsList_SansMoq()
        {
            // Act
            var result = _controller.GetEtablissementsAsync().Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.Value.Any());
        }
    }
}