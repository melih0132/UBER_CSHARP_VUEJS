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

namespace UberApi.Controllers.Tests
{
    [TestClass()]
    public class VillesControllerTests
    {

        private S221UberContext _context;
        private VillesController _controller;
        private IDataRepository<Ville> _adressesRepository;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<S221UberContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=sae4_UberApiDB; uid=s221; password=VmFu4u;");
            _context = new S221UberContext(builder.Options);
            _adressesRepository = new VilleManager(_context);
            _controller = new VillesController(_adressesRepository);
        }

        [TestMethod()]
        public void GetCoursiers_SansMoq()
        {

            List<Ville> expected = _context.Villes.ToList();


            // Act
            var actionResult = _controller.GetVillesAsync().Result;
            // Assert
            CollectionAssert.AreEqual(expected, actionResult.Value.ToList(), "");
        }
    }
}