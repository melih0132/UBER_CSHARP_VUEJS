using Microsoft.VisualStudio.TestTools.UnitTesting;
using UberApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class EntreprisesControllerTests
    {
        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private EntreprisesController _controller;
        private IDataRepository<Entreprise> _entreprisesRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _entreprisesRepository = new EntrepriseManager(_context);
            _controller = new EntreprisesController(_entreprisesRepository);
        }

        [TestMethod()]
        public void GetEntrepriseById_ExistingIdPassed_AreEqual_AvecMoq()
        {


            Entreprise entreprise = new Entreprise
            {
                IdEntreprise = 1,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

           
            var mockRepository = new Mock<IDataRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(entreprise);
            var controller = new EntreprisesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetEntrepriseAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(entreprise, actionResult.Value as Entreprise);
        }

        [TestMethod()]
        public void GetEntrepriseById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Entreprise entreprise = new Entreprise
            {
                IdEntreprise = 89,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };
            var mockRepository = new Mock<IDataRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((Entreprise)null);
            var controller = new EntreprisesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetEntrepriseAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetEntrepriseByNumeroCarteVTC_ExistingIdPassed_AreEqual_AvecMoq()
        {
            Entreprise entreprise = new Entreprise
            {
                IdEntreprise = 1,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };
            var mockRepository = new Mock<IDataRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetByStringAsync("TechCorp").Result).Returns(entreprise);
            var controller = new EntreprisesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetUtilisateurByLibelleEntrepriseAsync("TechCorp").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(entreprise, actionResult.Value as Entreprise);
        }

        [TestMethod]
        public void GetEntrepriseByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Entreprise entreprise = new Entreprise
            {
                IdEntreprise = 1,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

            var mockRepository = new Mock<IDataRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetByStringAsync("TechCorpopo").Result).Returns((Entreprise)null);
            var controller = new EntreprisesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetUtilisateurByLibelleEntrepriseAsync("TechCorpopo").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostEntreprise_ValideIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var entreprise = new Entreprise
            {
                IdEntreprise = 10,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

            var mockRepository = new Mock<IDataRepository<Entreprise>>();

            mockRepository.Setup(x => x.AddAsync(It.IsAny<Entreprise>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == entreprise.IdEntreprise)))
                           .ReturnsAsync(entreprise);

            var controller = new EntreprisesController(mockRepository.Object);

            // Act
            var actionResult = controller.PostEntrepriseAsync(entreprise).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Entreprise>));
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);
            var ress = result.Value as Entreprise;
            Assert.IsNotNull(ress);
            Assert.AreEqual(entreprise.IdEntreprise, ress.IdEntreprise);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<Entreprise>()), Times.Once);
        }


        [TestMethod]
        public void PutEntreprise_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var entreprise = new Entreprise
            {
                IdEntreprise = 1,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

            var entrepriseUpdate = new Entreprise
            {
                IdEntreprise = 1,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Petite",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

            var mockRepository = new Mock<IDataRepository<Entreprise>>();


            mockRepository.Setup(x => x.GetByIdAsync(entreprise.IdEntreprise)).ReturnsAsync(entrepriseUpdate);


            mockRepository.Setup(x => x.UpdateAsync(entreprise, entrepriseUpdate));

            var controller = new EntreprisesController(mockRepository.Object);

            // Act
            var actionResult = controller.PutEntrepriseAsync(entrepriseUpdate.IdEntreprise, entrepriseUpdate).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Entreprise>(c => c.IdEntreprise == entrepriseUpdate.IdEntreprise), entrepriseUpdate), Times.Once);
        }

        [TestMethod]
        public void DeleteEntreprise_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange : Création du entreprise
            var entreprise = new Entreprise
            {
                IdEntreprise = 1,
                IdAdresse = 1,
                SiretEntreprise = "12345678901234",
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

            var mockRepository = new Mock<IDataRepository<Entreprise>>();


            mockRepository.Setup(x => x.GetByIdAsync(entreprise.IdEntreprise))
                           .ReturnsAsync(entreprise);


            mockRepository.Setup(x => x.DeleteAsync(entreprise));

            var controller = new EntreprisesController(mockRepository.Object);

            // Act : Suppression
            var actionResult = controller.DeleteEntrepriseAsync(entreprise.IdEntreprise).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));


            mockRepository.Verify(x => x.GetByIdAsync(entreprise.IdEntreprise), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.Is<Entreprise>(c => c.IdEntreprise == entreprise.IdEntreprise)), Times.Once);
        }



        [TestMethod]
        public void DeleteEntreprise_NotValideIdPassed_ReturnsNotFound_AvecMoq()
        {
            // Arrange : ID inexistant
            int idEntrepriseInvalide = 50;

            var mockRepository = new Mock<IDataRepository<Entreprise>>();

            mockRepository.Setup(x => x.GetByIdAsync(idEntrepriseInvalide))
                           .ReturnsAsync((Entreprise)null);  // Retourner null pour simuler que le entreprise n'existe pas

            var controller = new EntreprisesController(mockRepository.Object);

            var actionResult = controller.DeleteEntrepriseAsync(idEntrepriseInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            mockRepository.Verify(x => x.GetByIdAsync(idEntrepriseInvalide), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Entreprise>()), Times.Never);
        }


        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [TestMethod()]
        public void GetEntreprises_SansMoq()
        {

            List<Entreprise> expected = _context.Entreprises.ToList();


            // Act
            var actionResult = _controller.GetEntreprisesAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }

        [TestMethod()]
        public void GetEntrepriseById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.Entreprises.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetEntrepriseAsync(expected.IdEntreprise).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }



        [TestMethod]
        public void GetEntrepriseByNumeroCarteVTC_ExistingIdPassed_AreEqual_SansMoq()
        {
            var LibelleEntreprise = "TechCorp";
            var expected = _context.Entreprises.FirstOrDefault(u => u.NomEntreprise.ToUpper() == LibelleEntreprise.ToUpper());

            // Act
            var actionResult = _controller.GetUtilisateurByLibelleEntrepriseAsync(LibelleEntreprise).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

        [TestMethod]
        public void GetEntrepriseByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_SansMoq()
        {

            var LibelleEntreprise = "TechCorpopo";
            var expected = _context.Entreprises.FirstOrDefault(u => u.NomEntreprise.ToUpper() == LibelleEntreprise.ToUpper());
            // Act
            var actionResult = _controller.GetUtilisateurByLibelleEntrepriseAsync(LibelleEntreprise).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostEntreprise_ValideIdPassed_ReturnsRightItem_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            var entreprise = _controller.GetEntreprisesAsync().Result.Value.Max(c => c.IdEntreprise);
            int derniereId = entreprise + 1;
            string chiffreSiretNew = "";
            for (int i = 0; i < 14; i++)
            {
                chiffreSiretNew += rnd.Next(1, 9);
            }




            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Entreprise cousierATester = new Entreprise()
            {

                IdAdresse = 1,
                SiretEntreprise = chiffreSiretNew,
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };
            // Act
            try
            {
                var result = _controller.PostEntrepriseAsync(cousierATester).Result;
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
            Entreprise? userRecupere = _context.Entreprises.Where(u => u.SiretEntreprise.ToUpper() ==
            cousierATester.SiretEntreprise.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            cousierATester.IdEntreprise = userRecupere.IdEntreprise;
            Assert.AreEqual(userRecupere, cousierATester, "Utilisateurs pas identiques");

        }


        [TestMethod]
        public void PutEntreprise_ValideIdPassed_ReturnsNoContent_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            var entreprise = _controller.GetEntreprisesAsync().Result.Value.Max(c => c.IdEntreprise);
            int derniereId = entreprise ;
            string chiffreSiretNew = "";
            for (int i = 0; i < 14; i++)
            {
                chiffreSiretNew += rnd.Next(1, 9);
            }

            // Étape 1 : Créer un entreprise et l'ajouter en base
            Entreprise cousierATester = new Entreprise()
            {
                IdEntreprise = derniereId,
                IdAdresse = 1,
                SiretEntreprise = chiffreSiretNew,
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

            int entrepriseUpdate = cousierATester.IdEntreprise;


            var actionResult = _controller.PutEntrepriseAsync(entrepriseUpdate, cousierATester).Result;

            // Assert
            Entreprise? cousierRecupere = _context.Entreprises
                .Where(u => u.SiretEntreprise.ToUpper() == cousierATester.SiretEntreprise.ToUpper())
                .FirstOrDefault();

            Assert.IsNotNull(cousierRecupere, "Le entreprise n'a pas été trouvé en base après la mise à jour");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
            Assert.AreEqual(chiffreSiretNew, cousierRecupere.SiretEntreprise, "Le numéro de siret n'a pas été mis à jour correctement.");

        }
        [TestMethod]
        public void DeleteEntreprise_ValideIdPassed_ReturnsNoContent_SansMoq()
        {

            Random rnd = new Random();
            var entreprise = _controller.GetEntreprisesAsync().Result.Value.Max(c => c.IdEntreprise);
            int derniereId = entreprise + 20;
            string chiffreSiretNew = "";
            for (int i = 0; i < 14; i++)
            {
                chiffreSiretNew += rnd.Next(1, 9);
            }

            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Entreprise cousierATester = new Entreprise()
            {

                IdEntreprise = derniereId,
                IdAdresse = 1,
                SiretEntreprise = chiffreSiretNew,
                NomEntreprise = "TechCorp",
                Taille = "Grande",
                Clients = [],
                Coursiers = [],
                IdAdresseNavigation = null
            };

            var result = _controller.PostEntrepriseAsync(cousierATester).Result;


            // Act : Suppression
            var actionResult = _controller.DeleteEntrepriseAsync(cousierATester.IdEntreprise).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));




        }



        [TestMethod]
        public void DeleteEntreprise_NotValideIdPassed_ReturnsNotFound_SansMoq()
        {
            // Arrange : ID inexistant

            int idEntrepriseInvalide = 500;

            var actionResult = _controller.DeleteEntrepriseAsync(idEntrepriseInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }


    }
}