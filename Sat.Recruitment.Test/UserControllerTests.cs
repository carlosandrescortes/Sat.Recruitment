using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UserControllerTests")]
    public class UserControllerTests
    {
        private readonly UsersController _controller;
        private readonly MockUserService _userServiceMock = new MockUserService();
        private readonly MockLogger<UsersController> _loggerMock = new MockLogger<UsersController>();

        public UserControllerTests()
        {
            _controller = new UsersController(_loggerMock, _userServiceMock);
        }

        [Fact]
        public async Task CreateUser_ReturnsConflict_WhenUserIsDuplicated()
        {
            // Arrange
            var duplicatedUser = new UserRequest { Name = "John", Email = "john@example.com", Phone = "1234567890", Address = "123 Main St", UserType = "Normal", Money = "50.00" };

            // Act
            var result = await _controller.CreateUser(duplicatedUser);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("The user is duplicated", conflictResult.Value);
        }

        [Fact]
        public async Task CreateUser_ReturnsOk_WhenUserIsCreated()
        {
            // Arrange
            var newUser = new UserRequest { Name = "Jane", Email = "jane@example.com", Phone = "0123456789", Address = "234 Main St", UserType = "Normal", Money = "50.00" };

            // Act
            var result = await _controller.CreateUser(newUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Created", okResult.Value);
        }
    }
}
