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
    /// <summary>
    /// Classe de tests pour le contrôleur CoursesController.
    /// </summary>

    [TestClass()]
    public class CoursesControllerTests
    {
        /// <summary>
        /// AVEC MOQ
        /// </summary>

        private S221UberContext _context;
        private CoursesController _controller;
        private ICourseRepository _coursesRepository;


        /// <summary>
        /// Initialise les composants nécessaires pour les tests.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _coursesRepository = new CourseManager(_context);
            _controller = new CoursesController(_coursesRepository);
        }


        /// <summary>
        /// Teste la récupération d'un cours par ID existant avec Moq.
        /// </summary>
        [TestMethod()]
        public void GetCourseById_ExistingIdPassed_AreEqual_AvecMoq()
        {
            Course course = new Course
            {
                IdCourse = 2,
                IdCoursier = 2,
                IdCb = 2,
                IdAdresse = 2,
                IdReservation = 2,
                AdrIdAdresse = 3,
                IdPrestation = 2,
                DateCourse = DateOnly.Parse("2025 - 03 - 14"),
                HeureCourse = TimeOnly.Parse("16:00:00"),
                PrixCourse = 15.75m,
                StatutCourse = "Terminée",
                NoteCourse = 4,
                CommentaireCourse = "Le trajet était un peu plus long que prévu.",
                Pourboire = 2,
                Distance = 8.2m,
                Temps = 20,
                AdrIdAdresseNavigation = null,
                IdAdresseNavigation = null,
                IdCbNavigation = null,
                IdCoursierNavigation = null,
                IdPrestationNavigation = null,
                IdReservationNavigation= null
            };
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(course);
            var controller = new CoursesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCourseAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(course, actionResult.Value as Course);
        }



        /// <summary>
        /// Teste la récupération d'un cours par ID inexistant avec Moq.
        /// </summary>
        [TestMethod()]
        public void GetCourseById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            Course course = new Course
            {
                IdCourse = 2,
                IdCoursier = 2,
                IdCb = 2,
                IdAdresse = 2,
                IdReservation = 2,
                AdrIdAdresse = 3,
                IdPrestation = 2,
                DateCourse = DateOnly.Parse("2025 - 03 - 14"),
                HeureCourse = TimeOnly.Parse("16:00:00"),
                PrixCourse = 15.75m,
                StatutCourse = "Terminée",
                NoteCourse = 4,
                CommentaireCourse = "Le trajet était un peu plus long que prévu.",
                Pourboire = 2,
                Distance = 8.2m,
                Temps = 20,
                AdrIdAdresseNavigation = null,
                IdAdresseNavigation = null,
                IdCbNavigation = null,
                IdCoursierNavigation = null,
                IdPrestationNavigation = null,
                IdReservationNavigation = null
            };
            var mockRepository = new Mock<ICourseRepository>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((Course)null);
            var controller = new CoursesController(mockRepository.Object);
            // Act
            var actionResult = controller.GetCourseAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        /// <summary>
        /// Teste l'ajout d'un cours avec Moq.
        /// </summary>
        [TestMethod]
        public void PostCourse_ValideIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            var course = new Course
            {
                IdCourse = 2,
                IdCoursier = 2,
                IdCb = 2,
                IdAdresse = 2,
                IdReservation = 2,
                AdrIdAdresse = 3,
                IdPrestation = 2,
                DateCourse = DateOnly.Parse("2025 - 03 - 14"),
                HeureCourse = TimeOnly.Parse("16:00:00"),
                PrixCourse = 15.75m,
                StatutCourse = "Terminée",
                NoteCourse = 4,
                CommentaireCourse = "Le trajet était un peu plus long que prévu.",
                Pourboire = 2,
                Distance = 8.2m,
                Temps = 20,
                AdrIdAdresseNavigation = null,
                IdAdresseNavigation = null,
                IdCbNavigation = null,
                IdCoursierNavigation = null,
                IdPrestationNavigation = null,
                IdReservationNavigation = null
            };

            var mockRepository = new Mock<ICourseRepository>();

            mockRepository.Setup(x => x.AddAsync(It.IsAny<Course>())).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == course.IdCourse)))
                           .ReturnsAsync(course);

            var controller = new CoursesController(mockRepository.Object);

            // Act
            var actionResult = controller.PostCourseAsync(course).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Course>));
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(result);
            var ress = result.Value as Course;
            Assert.IsNotNull(ress);
            Assert.AreEqual(course.IdCourse, ress.IdCourse);
            mockRepository.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Once);
        }


        /// <summary>
        /// Teste la mise à jour d'un cours avec Moq.
        /// </summary>
        [TestMethod]
        public void PutCourse_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var course = new Course
            {
                IdCourse = 2,
                IdCoursier = 2,
                IdCb = 2,
                IdAdresse = 2,
                IdReservation = 2,
                AdrIdAdresse = 3,
                IdPrestation = 2,
                DateCourse = DateOnly.Parse("2025 - 03 - 14"),
                HeureCourse = TimeOnly.Parse("16:00:00"),
                PrixCourse = 15.75m,
                StatutCourse = "Terminée",
                NoteCourse = 4,
                CommentaireCourse = "Le trajet était un peu plus long que prévu.",
                Pourboire = 2,
                Distance = 8.2m,
                Temps = 20,
                AdrIdAdresseNavigation = null,
                IdAdresseNavigation = null,
                IdCbNavigation = null,
                IdCoursierNavigation = null,
                IdPrestationNavigation = null,
                IdReservationNavigation = null
            };

            var courseUpdate = new Course
            {
                IdCourse = 2,
                IdCoursier = 2,
                IdCb = 2,
                IdAdresse = 2,
                IdReservation = 2,
                AdrIdAdresse = 3,
                IdPrestation = 2,
                DateCourse = DateOnly.Parse("2025 - 03 - 14"),
                HeureCourse = TimeOnly.Parse("16:00:00"),
                PrixCourse = 15.75m,
                StatutCourse = "Terminée",
                NoteCourse = 4,
                CommentaireCourse = "Le trajet était un peu plus long que prévu. Cordialement",
                Pourboire = 2,
                Distance = 8.2m,
                Temps = 20,
                AdrIdAdresseNavigation = null,
                IdAdresseNavigation = null,
                IdCbNavigation = null,
                IdCoursierNavigation = null,
                IdPrestationNavigation = null,
                IdReservationNavigation = null
            };

            var mockRepository = new Mock<ICourseRepository>();


            mockRepository.Setup(x => x.GetByIdAsync(course.IdCourse)).ReturnsAsync(courseUpdate);


            mockRepository.Setup(x => x.UpdateAsync(course, courseUpdate));

            var controller = new CoursesController(mockRepository.Object);

            // Act
            var actionResult = controller.PutCourseAsync(courseUpdate.IdCourse, courseUpdate).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Course>(c => c.IdCourse == courseUpdate.IdCourse), courseUpdate), Times.Once);
        }

        /// <summary>
        /// Teste la suppression d'un cours avec Moq.
        /// </summary>
        [TestMethod]
        public void DeleteCourse_ValideIdPassed_ReturnsNoContent_AvecMoq()
        {
            // Arrange : Création du coursier
            var course = new Course
            {
                IdCourse = 2,
                IdCoursier = 2,
                IdCb = 2,
                IdAdresse = 2,
                IdReservation = 2,
                AdrIdAdresse = 3,
                IdPrestation = 2,
                DateCourse = DateOnly.Parse("2025 - 03 - 14"),
                HeureCourse = TimeOnly.Parse("16:00:00"),
                PrixCourse = 15.75m,
                StatutCourse = "Terminée",
                NoteCourse = 4,
                CommentaireCourse = "Le trajet était un peu plus long que prévu. Cordialement",
                Pourboire = 2,
                Distance = 8.2m,
                Temps = 20,
                AdrIdAdresseNavigation = null,
                IdAdresseNavigation = null,
                IdCbNavigation = null,
                IdCoursierNavigation = null,
                IdPrestationNavigation = null,
                IdReservationNavigation = null
            };

            var mockRepository = new Mock<ICourseRepository>();


            mockRepository.Setup(x => x.GetByIdAsync(course.IdCourse))
                           .ReturnsAsync(course);


            mockRepository.Setup(x => x.DeleteAsync(course));

            var controller = new CoursesController(mockRepository.Object);

            // Act : Suppression
            var actionResult = controller.DeleteCourseAsync(course.IdCourse).Result;

            // Assert : Vérification de la réponse
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));


            mockRepository.Verify(x => x.GetByIdAsync(course.IdCourse), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.Is<Course>(c => c.IdCourse == course.IdCourse)), Times.Once);

        }


        /// <summary>
        /// Teste la suppression d'un cours avec un ID invalide avec Moq.
        /// </summary>
        [TestMethod]
        public void DeleteCourse_NotValideIdPassed_ReturnsNotFound_AvecMoq()
        {
            // Arrange : ID inexistant
            int idCourseInvalide = 19;

            var mockRepository = new Mock<ICourseRepository>();

            mockRepository.Setup(x => x.GetByIdAsync(idCourseInvalide))
                           .ReturnsAsync((Course)null);  

            var controller = new CoursesController(mockRepository.Object);

            var actionResult = controller.DeleteCourseAsync(idCourseInvalide).Result;


            Assert.IsNotNull(actionResult);


            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

            mockRepository.Verify(x => x.GetByIdAsync(idCourseInvalide), Times.Once);


            mockRepository.Verify(x => x.DeleteAsync(It.IsAny<Course>()), Times.Never);
        }



        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        /// <summary>
        /// Teste la récupération de tous les cours sans Moq.
        /// </summary>
        [TestMethod()]
        public void GetCoursiers_SansMoq()
        {

            List<Course> expected = _context.Courses.ToList();


            // Act
            var actionResult = _controller.GetCoursesAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }


        /// <summary>
        /// Teste la récupération d'un cours par ID existant ou non sans Moq.
        /// </summary>
        [TestMethod()]
        public void GetCourseById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.Courses.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetCourseAsync(expected.IdCourse).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }


        


    }
}