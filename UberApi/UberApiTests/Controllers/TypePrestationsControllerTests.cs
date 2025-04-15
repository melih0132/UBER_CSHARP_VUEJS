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
    public class TypePrestationsControllerTests
    {
        
        
     /// <summary>
     /// AVEC MOQ
     /// </summary>

        private S221UberContext _context;
        private TypePrestationsController _controller;
        private IDataRepository<TypePrestation> _typePrestationsRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _typePrestationsRepository = new TypePrestationManager(_context);
            _controller = new TypePrestationsController(_typePrestationsRepository);
        }


        [TestMethod()]
        public void GetTypePrestationById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            TypePrestation typePrestation = new TypePrestation
            {
                IdPrestation= 1,
                LibellePrestation= "UberX",
                DescriptionPrestation= "Un voyage standard et économique, parfait pour les trajets quotidiens et les déplacements en ville.",
                ImagePrestation= "UberX.jpg",
                Courses= [],
                IdVehicules= []
            };
            var mockRepository = new Mock<IDataRepository<TypePrestation>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typePrestation);
            var controller = new TypePrestationsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetTypePrestationAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(typePrestation, actionResult.Value as TypePrestation);
        }




        [TestMethod()]
        public void GetTypePrestationById_NotExistingIdPassed_ReturnsRightItem_AvecMoq()
        {

            var mockRepository = new Mock<IDataRepository<TypePrestation>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns((TypePrestation)null);
            var controller = new TypePrestationsController(mockRepository.Object);
            // Act
            var actionResult = controller.GetTypePrestationAsync(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(actionResult.Value);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }



        /// <summary>
        /// SANS MOQ /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [TestMethod()]
        public void GetTypePrestations_SansMoq()
        {

            List<TypePrestation> expected = _context.TypePrestations.ToList();


            // Act
            var actionResult = _controller.GetTypePrestationsAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }

        [TestMethod()]
        public void GetTypePrestationById_ExistingIdPassedOrNot_AreEqual_SansMoq()
        {

            var expected = _context.TypePrestations.FirstOrDefault();
            if (expected == null)
            {
                return;
            }

            // Act
            var actionResult = _controller.GetTypePrestationAsync(expected.IdPrestation).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(expected, actionResult.Value);
        }

    }
}