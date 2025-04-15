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
    public class ClientsControllerTests
    {

        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private ClientsController _controller;
        private IDataRepository<Client> _clientsRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _clientsRepository = new ClientManager(_context);
            _controller = new ClientsController(_clientsRepository);
        }

        [TestMethod()]
        public void GetClientById_ExistingIdPassed_AreEqual_AvecMoq()
        {


            Client client = new Client
            {

                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.com",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };
            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(client);
            var controller = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetClientAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(client, actionResult.Value as Client);
        }

        [TestMethod()]
        public void GetClientById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Client client = new Client
            {
                IdClient = 89,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.com",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };
            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((Client)null);
            var controller = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetClientAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetClientByEmail_ExistingIdPassed_AreEqual_AvecMoq()
        {
            Client client = new Client
            {
                IdClient = 1,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.com",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };
            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByStringAsync("pierre.dupont@example.com").Result).Returns(client);
            var controller = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetUtilisateurByEmailAsync("pierre.dupont@example.com").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(client, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Client client = new Client
            {
                IdClient = 1,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.coooom",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };

            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByStringAsync("pierre.dupont@example.coooom").Result).Returns((Client)null);
            var controller = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetUtilisateurByEmailAsync("pierre.dupont@example.coooom").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostClient_ValideIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var client = new Client
            {
                IdClient = 10,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.com",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };

            var mockRepository = new Mock<IDataRepository<Client>>();

            mockRepository.Setup(x => x.AddAsync(It.IsAny<Client>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == client.IdClient)))
                           .ReturnsAsync(client);

            var controller = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = controller.PostClientAsync(client).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Client>));
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);
            var ress = result.Value as Client;
            Assert.IsNotNull(ress);
            Assert.AreEqual(client.IdClient, ress.IdClient);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<Client>()), Times.Once);
        }


        [TestMethod]
        public void PutClient_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var client = new Client
            {
                IdClient = 1,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.com",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };

            var clientUpdate = new Client
            {
                IdClient = 1,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.com",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };

            var mockRepository = new Mock<IDataRepository<Client>>();


            mockRepository.Setup(x => x.GetByIdAsync(client.IdClient)).ReturnsAsync(clientUpdate);


            mockRepository.Setup(x => x.UpdateAsync(client, clientUpdate));

            var controller = new ClientsController(mockRepository.Object);

            // Act
            var actionResult = controller.PutClientAsync(clientUpdate.IdClient, clientUpdate).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Client>(c => c.IdClient == clientUpdate.IdClient), clientUpdate), Times.Once);
        }

        [TestMethod]
        public void DeleteClient_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange : Création du client
            var client = new Client
            {
                IdClient = 1,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = "pierre.dupont@example.com",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };

            var mockRepository = new Mock<IDataRepository<Client>>();


            mockRepository.Setup(x => x.GetByIdAsync(client.IdClient))
                           .ReturnsAsync(client);


            mockRepository.Setup(x => x.DeleteAsync(client));

            var controller = new ClientsController(mockRepository.Object);

            // Act : Suppression
            var actionResult = controller.DeleteClientAsync(client.IdClient).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));


            mockRepository.Verify(x => x.GetByIdAsync(client.IdClient), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.Is<Client>(c => c.IdClient == client.IdClient)), Times.Once);
        }



        [TestMethod]
        public void DeleteClient_NotValideIdPassed_ReturnsNotFound_AvecMoq()
        {
            // Arrange : ID inexistant
            int idClientInvalide = 500;

            var mockRepository = new Mock<IDataRepository<Client>>();

            mockRepository.Setup(x => x.GetByIdAsync(idClientInvalide))
                           .ReturnsAsync((Client)null);  // Retourner null pour simuler que le client n'existe pas

            var controller = new ClientsController(mockRepository.Object);

            var actionResult = controller.DeleteClientAsync(idClientInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            mockRepository.Verify(x => x.GetByIdAsync(idClientInvalide), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Client>()), Times.Never);
        }


        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        [TestMethod()]
        public void GetClientById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.Clients.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetClientAsync(expected.IdClient).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }


        [TestMethod]
        public void GetClientByNumeroCarteVTC_ExistingIdPassed_AreEqual_SansMoq()
        {
            var email = "marc.lemoine@example.com";
            var expected = _context.Clients.FirstOrDefault(u => u.EmailUser.ToUpper() == email.ToUpper());

            // Act
            var actionResult = _controller.GetUtilisateurByEmailAsync(email).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

        [TestMethod]
        public void GetClientByNumeroCarteVTC_NotExistingIdPassed_ReturnsRightItem_SansMoq()
        {

            var email = "pierre.dupont@example.coddddm";
            var expected = _context.Clients.FirstOrDefault(u => u.EmailUser.ToUpper() == email.ToUpper());
            // Act
            var actionResult = _controller.GetUtilisateurByEmailAsync(email).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostClient_ValideIdPassed_ReturnsRightItem_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            int chiffreNew = rnd.Next(1, 9);
            var client = _controller.GetClientsAsync().Result.Value.Max(c => c.IdClient);
            int derniereId = client + 1;
            string chiffreIban = "";
            string chiffreCarteNew = "";
            for (int i = 0; i < 12; i++)
            {
                chiffreCarteNew += rnd.Next(0, 10);
            }
            for (int i = 0; i < 23; i++)
            {
                chiffreIban += rnd.Next(1, 9);
            }



            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Client cousierATester = new Client()
            {

                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = chiffreIban + "pierre.dupont" + chiffreCarteNew + "@example.coooom",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };
            // Act
            try
            {
                var result = _controller.PostClientAsync(cousierATester).Result;
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
            Client? userRecupere = _context.Clients.Where(u => u.EmailUser.ToUpper() ==
            cousierATester.EmailUser.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            cousierATester.IdClient = userRecupere.IdClient;
            Assert.AreEqual(userRecupere, cousierATester, "Utilisateurs pas identiques");

        }


        [TestMethod]
        public void PutClient_ValideIdPassed_ReturnsNoContent_SansMoq()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            string chiffreIban = "";
            string chiffreCarteNew = "";
            for (int i = 0; i < 12; i++)
            {
                chiffreCarteNew += rnd.Next(0, 10);
            }
            for (int i = 0; i < 23; i++)
            {
                chiffreIban += rnd.Next(1, 9);
            }

            // Étape 1 : Créer un client et l'ajouter en base
            Client cousierATester = new Client()
            {
                IdClient = 1,
                IdEntreprise = null,
                IdAdresse = 1,
                GenreUser = "Homme",
                NomUser = "Dupont",
                PrenomUser = "Pierre",
                DateNaissance = DateOnly.Parse("1985 - 06 - 15"),
                Telephone = "0601020304",
                EmailUser = chiffreIban + "pierre.dupont" + chiffreCarteNew + "@example.coooom",
                MotDePasseUser = "$2a$10$zMHRG7CXOPiDs2UB78dnfOa.VZF57JaYsnHyzAvvOOXXtN3W5P86m",
                PhotoProfile = "profile_pic1.jpg",
                SouhaiteRecevoirBonPlan = true,
                MfaActivee = false,
                TypeClient = "Particulier",
                LastConnexion = DateTime.Parse("2025 - 03 - 12T00:00:00"),
                DemandeSuppression = false,
                Factures = [],
                IdAdresseNavigation = null,
                IdEntrepriseNavigation = null,
                LieuFavoris = [],
                Otps = [],
                Paniers = [],
                Reservations = [],
                IdCbs = []
            };

            int clientUpdate = cousierATester.IdClient;


            cousierATester.Telephone = "0601020304";
            var actionResult = _controller.PutClientAsync(clientUpdate, cousierATester).Result;

            // Assert
            Client? cousierRecupere = _context.Clients
                .Where(u => u.EmailUser.ToUpper() == cousierATester.EmailUser.ToUpper())
                .FirstOrDefault();

            Assert.IsNotNull(cousierRecupere, "Le client n'a pas été trouvé en base après la mise à jour");
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
            Assert.AreEqual("0601020304", cousierRecupere.Telephone, "Le numéro de téléphone n'a pas été mis à jour correctement.");

        }

    }

}
