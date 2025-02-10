using Moq;
using Microsoft.AspNetCore.Mvc;
using Appliction.Interfaces;
using Domain.Authentication;
using WebAPI.Controllers;

public class AuthControllerTests
{
    [Fact]
    public async Task SignUp_ShouldReturnBadRequest_WhenUserIsNull()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var authController = new AuthController(mockUserService.Object);

        // Act
        var result = await authController.SignUp(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalied User..!", badRequestResult.Value);
    }

    [Fact]
    public async Task SignUp_ShouldReturnOk_WhenUserServiceReturnsTrue()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.SignUpAsync(It.IsAny<SignUp>()))
            .ReturnsAsync(true); // Simulate successful user registration

        var authController = new AuthController(mockUserService.Object);

        var user = new SignUp
        {
            UserName = "TestUser",
            Password = "TestPassword",
            confirmPassword = "TestPassword",
            Email = "test@example.com"
        };

        // Act
        var result = await authController.SignUp(user);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("User registered successfully.", okResult.Value);
    }

    [Fact]
    public async Task SignUp_ShouldReturnBadRequest_WhenUserServiceReturnsFalse()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.SignUpAsync(It.IsAny<SignUp>()))
            .ReturnsAsync(false); // Simulate failed user registration

        var authController = new AuthController(mockUserService.Object);

        var user = new SignUp
        {
            UserName = "TestUser",
            Password = "TestPassword",
            confirmPassword = "TestPassword",
            Email = "test@example.com"
        };

        // Act
        var result = await authController.SignUp(user);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("User registration failed.", badRequestResult.Value);
    }
}
