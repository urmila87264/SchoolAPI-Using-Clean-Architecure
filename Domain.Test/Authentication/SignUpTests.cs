using Domain.Authentication;
namespace Domain.Test.Authentication
{
    public class SignUpTests
    {
        [Fact]
        public async Task IsValid_ShouldReturnTrue_WhenAllFieldsAreValid()
        {
            // Arrange
            var signUp = new Registration
            {
                name = "TestUser",
                email = "test@example.com",
                Password = "password123",
                confirmPassword = "password123"
            };

            //// Act
            //var result = signUp.IsValid();

            //// Assert
            //Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenUserNameIsEmpty()
        {
            // Arrange
            var signUp = new Registration
            {
                name = string.Empty,
                email = "test@example.com",
                Password = "password123",
                confirmPassword = "password123"
            };

            // Act
            //var result = "";// signUp.IsValid();

            //// Assert
            //Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenEmailIsInvalid()
        {
            // Arrange
            var signUp = new Registration
            {
                name = "TestUser",
                email = "invalid-email",
                Password = "password123",
                confirmPassword = "password123"
            };

            // Act
            //var result = signUp.IsValid();

            //// Assert
            //Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenPasswordAndConfirmPasswordDoNotMatch()
        {
            // Arrange
            var signUp = new Registration
            {
                name = "TestUser",
                email = "test@example.com",
                Password = "password123",
                confirmPassword = "differentPassword"
            };

            //// Act
            //var result = signUp.IsValid();

            //// Assert
            //Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenPasswordIsTooShort()
        {
            // Arrange
            var signUp = new Registration
            {
                name = "TestUser",
                email = "test@example.com",
                Password = "123",
                confirmPassword = "123"
            };

            // Act
          //  var result = signUp.IsValid();

            // Assert
            //Assert.False(result);
        }
    }
}
