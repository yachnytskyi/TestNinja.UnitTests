using Microsoft.AspNetCore.Mvc;
using TestNinja.UnitTests.Controllers;
using TestNinja.UnitTests.Models;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestApp.Tests
{
    public class PhoneControllerTests
    {
        [Fact]
        public void IndexReturntsAViewResultWithListOfPhones()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestPhones());
            var controller = new PhoneController(mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Phone>>(viewResult.Model);
            Assert.Equal(GetTestPhones().Count, model.Count());
        }
        private List<Phone> GetTestPhones()
        {
            var phones = new List<Phone>
            {
                new Phone { Id=1, Name="iPhone 10", Company="Apple", Price=900},
                new Phone { Id=2, Name="Meizu 6 Pro", Company="Meizu", Price=300},
                new Phone { Id=3, Name="Mi 7", Company="Xiaomi", Price=400},
                new Phone { Id=4, Name="iPhone 10", Company="Apple", Price=900},
            };
            return phones;
        }
        
    }
}