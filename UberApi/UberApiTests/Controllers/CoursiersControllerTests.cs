using Microsoft.VisualStudio.TestTools.UnitTesting;
using UberApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberApi.Models.Repository;
using UberApi.Models.EntityFramework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.DataManager;

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class CoursiersControllerTests
    {

        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private CoursiersController _controller;
        private IDataRepository<Coursier> _coursiersRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _coursiersRepository = new CoursierManager(_context);
            _controller = new CoursiersController(_coursiersRepository);
        }

        [TestMethod()]
        public void GetCoursierById_ExistingIdPassed_AreEqual_AvecMoq()
        {


            Coursier coursier = new Coursier
            {
                IdCoursier = 1,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789012",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };
            var mockRepository = new Mock<IDataRepository<Coursier>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(coursier);
            var controller = new CoursiersController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCoursierAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(coursier, actionResult.Value as Coursier);
        }

        [TestMethod()]
        public void GetCoursierById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Coursier coursier = new Coursier
            {
                IdCoursier = 89,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789012",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };
            var mockRepository = new Mock<IDataRepository<Coursier>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((Coursier)null);
            var controller = new CoursiersController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCoursierAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCoursierByNumeroCarteVTC_ExistingIdPassed_AreEqual_AvecMoq()
        {
            Coursier coursier = new Coursier
            {
                IdCoursier = 1,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789012",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };
            var mockRepository = new Mock<IDataRepository<Coursier>>();
            mockRepository.Setup(x => x.GetByStringAsync("123456789012").Result).Returns(coursier);
            var controller = new CoursiersController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCoursierByNumeroCarteVTCAsync("123456789012").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(coursier, actionResult.Value as Coursier);
        }

        [TestMethod]
        public void GetCoursierByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Coursier coursier = new Coursier
            {
                IdCoursier = 1,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789013",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };

            var mockRepository = new Mock<IDataRepository<Coursier>>();
            mockRepository.Setup(x => x.GetByStringAsync("123456789012").Result).Returns((Coursier)null);
            var controller = new CoursiersController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCoursierByNumeroCarteVTCAsync("123456789012").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostCoursier_ValideIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var coursier = new Coursier
            {
                IdCoursier = 10,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Amir",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789012",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };

            var mockRepository = new Mock<IDataRepository<Coursier>>();

            mockRepository.Setup(x => x.AddAsync(It.IsAny<Coursier>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == coursier.IdCoursier)))
                           .ReturnsAsync(coursier);

            var controller = new CoursiersController(mockRepository.Object);

            // Act
            var actionResult = controller.PostCoursierAsync(coursier).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Coursier>));
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);
            var ress = result.Value as Coursier;
            Assert.IsNotNull(ress);
            Assert.AreEqual(coursier.IdCoursier, ress.IdCoursier);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<Coursier>()), Times.Once);
        }


        [TestMethod]
        public void PutCoursier_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var coursier = new Coursier
            {
                IdCoursier = 1,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789012",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };

            var coursierUpdate = new Coursier
            {
                IdCoursier = 1,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Enzo", //new name to update
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789012",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };

            var mockRepository = new Mock<IDataRepository<Coursier>>();


            mockRepository.Setup(x => x.GetByIdAsync(coursier.IdCoursier)).ReturnsAsync(coursierUpdate);


            mockRepository.Setup(x => x.UpdateAsync(coursier, coursierUpdate));

            var controller = new CoursiersController(mockRepository.Object);

            // Act
            var actionResult = controller.PutCoursierAsync(coursierUpdate.IdCoursier, coursierUpdate).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Coursier>(c => c.IdCoursier == coursierUpdate.IdCoursier), coursierUpdate), Times.Once);
        }

        [TestMethod]
        public void DeleteCoursier_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange : Création du coursier
            var coursier = new Coursier
            {
                IdCoursier = 1,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010101",
                EmailUser = "julien.durant@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "123456789012",
                Iban = "FR7630006000011234567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };

            var mockRepository = new Mock<IDataRepository<Coursier>>();

            
            mockRepository.Setup(x => x.GetByIdAsync(coursier.IdCoursier))
                           .ReturnsAsync(coursier);

           
            mockRepository.Setup(x => x.DeleteAsync(coursier));

            var controller = new CoursiersController(mockRepository.Object);

            // Act : Suppression
            var actionResult = controller.DeleteCoursierAsync(coursier.IdCoursier).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

           
            mockRepository.Verify(x => x.GetByIdAsync(coursier.IdCoursier), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.Is<Coursier>(c => c.IdCoursier == coursier.IdCoursier)), Times.Once);
        }



        [TestMethod]
        public void DeleteCoursier_NotValideIdPassed_ReturnsNotFound_AvecMoq()
        {
            // Arrange : ID inexistant
            int idCoursierInvalide = 19;

            var mockRepository = new Mock<IDataRepository<Coursier>>();

            mockRepository.Setup(x => x.GetByIdAsync(idCoursierInvalide))
                           .ReturnsAsync((Coursier)null);  // Retourner null pour simuler que le coursier n'existe pas

            var controller = new CoursiersController(mockRepository.Object);

            var actionResult = controller.DeleteCoursierAsync(idCoursierInvalide).Result;


            Assert.IsNotNull(actionResult);

 
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));  

            mockRepository.Verify(x => x.GetByIdAsync(idCoursierInvalide), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Coursier>()), Times.Never);
        }


        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [TestMethod()]
        public void GetCoursiers_SansMoq()
        {

            List<Coursier> expected = _context.Coursiers.ToList();


            // Act
            var actionResult = _controller.GetCoursiersAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }

        [TestMethod()]
        public void GetCoursierById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.Coursiers.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetCoursierAsync(expected.IdCoursier).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }


        [TestMethod]
        public void GetCoursierByNumeroCarteVTC_ExistingIdPassed_AreEqual_SansMoq()
        {
            var numeroCarteVtc = "123456789012";
            var expected =  _context.Coursiers.FirstOrDefault(u => u.NumeroCarteVtc.ToUpper() == numeroCarteVtc.ToUpper());

            // Act
            var actionResult = _controller.GetCoursierByNumeroCarteVTCAsync(numeroCarteVtc).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

        [TestMethod]
        public void GetCoursierByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_SansMoq()
        {

            var numeroCarteVtc = "123456789013";
            var expected = _context.Coursiers.FirstOrDefault(u => u.NumeroCarteVtc.ToUpper() == numeroCarteVtc.ToUpper());
            // Act
            var actionResult = _controller.GetCoursierByNumeroCarteVTCAsync(numeroCarteVtc).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostCoursier_ValideIdPassed_ReturnsRightItem_SansMoq()
        { 
            // Arrange
            Random rnd = new Random();
            int chiffreNew = rnd.Next(1, 9);
            var coursier = _controller.GetCoursiersAsync().Result.Value.Max(c => c.IdCoursier);
            int derniereId = coursier + 1;
            string chiffreIban = "";
            string chiffreCarteNew = "";
            for (int i = 0; i < 12; i++) {
                chiffreCarteNew += rnd.Next(1, 9);
            }
            for (int i = 0; i < 23; i++)
            {
                chiffreIban += rnd.Next(1, 9);
            }



            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Coursier cousierATester = new Coursier()
            {

                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "06010101"+ chiffreNew + "1",
                EmailUser = "julien.durant" + derniereId + "@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = chiffreCarteNew,
                Iban = "FR" +chiffreIban,
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };
            // Act
            try
            {
                var result = _controller.PostCoursierAsync(cousierATester).Result;
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
            Coursier? userRecupere = _context.Coursiers.Where(u => u.EmailUser.ToUpper() ==
            cousierATester.EmailUser.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            cousierATester.IdCoursier = userRecupere.IdCoursier;
            Assert.AreEqual(userRecupere, cousierATester, "Utilisateurs pas identiques");

        }


        [TestMethod]
        public void PutCoursier_ValideIdPassed_ReturnsNoContent_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            // Étape 1 : Créer un coursier et l'ajouter en base
            Coursier cousierATester = new Coursier()
            {
                IdCoursier = 20,
                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "0601010181",
                EmailUser = "julien.durant160077837@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = "128456789812",
                Iban = "FR76380600001284567890189",
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };

            int coursierUpdate = cousierATester.IdCoursier;

            
            cousierATester.Telephone = "0601020304"; 
            var actionResult = _controller.PutCoursierAsync(coursierUpdate, cousierATester).Result;

            // Assert
            Coursier? cousierRecupere = _context.Coursiers
                .Where(u => u.EmailUser.ToUpper() == cousierATester.EmailUser.ToUpper())
                .FirstOrDefault();

            Assert.IsNotNull(cousierRecupere, "Le coursier n'a pas été trouvé en base après la mise à jour");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
            Assert.AreEqual("0601020304", cousierRecupere.Telephone, "Le numéro de téléphone n'a pas été mis à jour correctement.");

        }
        [TestMethod]
        public void DeleteCoursier_ValideIdPassed_ReturnsNoContent_SansMoq()
        {

            Random rnd = new Random();
            int chiffreNew = rnd.Next(1, 9);
            var coursier = _controller.GetCoursiersAsync().Result.Value.Max(c => c.IdCoursier);
            int derniereId = coursier + 20;
            string chiffreIban = "";
            string chiffreCarteNew = "";
            for (int i = 0; i < 12; i++)
            {
                chiffreCarteNew += rnd.Next(1, 9);
            }
            for (int i = 0; i < 23; i++)
            {
                chiffreIban += rnd.Next(1, 9);
            }

            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Coursier cousierATester = new Coursier()
            {

                IdEntreprise = 1,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Durant",
                PrenomUser = "Julien",
                DateNaissance = DateOnly.Parse("1988-04-25"),
                Telephone = "06010101" + chiffreNew + "1",
                EmailUser = "julien.durant" + derniereId + "@example.com",
                MotDePasseUser = "hasedpassword123",
                NumeroCarteVtc = chiffreCarteNew,
                Iban = "FR" + chiffreIban,
                DateDebutActivite = DateOnly.Parse("2023-01-15"),
                NoteMoyenne = 4.5m,
                Courses = [],
                Entretiens = [],
                Horaires = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                ReglementSalaires = [],
                Vehicules = []
            };

            var result = _controller.PostCoursierAsync(cousierATester).Result;


            // Act : Suppression
            var actionResult = _controller.DeleteCoursierAsync(cousierATester.IdCoursier).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));




        }



        [TestMethod]
        public void DeleteCoursier_NotValideIdPassed_ReturnsNotFound_SansMoq()
        {
            // Arrange : ID inexistant

            int idCoursierInvalide = 19;

            var actionResult = _controller.DeleteCoursierAsync(idCoursierInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }


    }

}
