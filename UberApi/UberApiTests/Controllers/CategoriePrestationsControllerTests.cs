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

    }
}