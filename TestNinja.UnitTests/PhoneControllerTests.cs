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
        [Fact]
        public void AddPhoneReturnsViewResultWithPhone()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new PhoneController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Phone newPhone = new Phone();

            // Act
            var result = controller.AddPhone(newPhone);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newPhone, viewResult?.Model);
        }

        [Fact]
        public void AddPhoneReturnsARedirectAndAddsPhone()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new PhoneController(mock.Object);
            var newPhone = new Phone()
            {
                Name = "Sony Xperia XZ"
            };

            // Act
            var result = controller.AddPhone(newPhone);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newPhone));
        }

        [Fact]
        public void GetPhoneReturnsBadRequestResultWhenIdIsNull()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new PhoneController(mock.Object);

            // Act
            var result = controller.GetPhone(null);

            // Arrange
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetPhoneReturnsNotFoundResultWhenPhoneNotFound()
        {
            // Arrange
            int testPhoneId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testPhoneId))
                .Returns(null as Phone);
            var controller = new PhoneController(mock.Object);

            // Act
            var result = controller.GetPhone(testPhoneId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPhoneReturnsViewResultWithPhone()
        {
            // Arrange
            int testPhoneId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testPhoneId))
                .Returns(GetTestPhones().FirstOrDefault(p => p.Id == testPhoneId));
            var controller = new PhoneController(mock.Object);


            // Act
            var result = controller.GetPhone(testPhoneId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Phone>(viewResult.ViewData.Model);
            Assert.Equal("iPhone 10", model.Name);
            Assert.Equal(900, model.Price);
            Assert.Equal("Apple", model.Company);
            Assert.Equal(testPhoneId, model.Id);
        }
        
    }
}