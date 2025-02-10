using Moq;
using System.Data.SqlClient;
using Appliction.Procedure;
using Business;
using DBHelper;
using Domain.Authentication;

public class UserRepositoryTests
{
    [Fact]
    public async Task AddUserAsync_ShouldReturnTrue_WhenExecuteNonQueryReturnsPositiveValue()
    {
        // Arrange
        var mockSqlHelper = new Mock<ISQLHelper>();
        mockSqlHelper
            .Setup(helper => helper.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
            .Returns(1); // Simulate successful execution

        var userRepository = new UserRepository(mockSqlHelper.Object);

        var user = new SignUp
        {
            UserName = "TestUser",
            Password = "TestPassword",
            Email = "test@example.com"
        };

        // Act
        var result = await userRepository.AddUserAsync(user);

        // Assert
        Assert.True(result);
        mockSqlHelper.Verify(helper => helper.ExecuteNonQuery(SecurityProcedure.SignUp, It.IsAny<SqlParameter[]>()), Times.Once);
    }

    [Fact]
    public async Task AddUserAsync_ShouldReturnFalse_WhenExecuteNonQueryReturnsZero()
    {
        // Arrange
        var mockSqlHelper = new Mock<ISQLHelper>();
        mockSqlHelper
            .Setup(helper => helper.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
            .Returns(0); // Simulate failed execution

        var userRepository = new UserRepository(mockSqlHelper.Object);

        var user = new SignUp
        {
            UserName = "TestUser",
            Password = "TestPassword",
            Email = "test@example.com"
        };

        // Act
        var result = await userRepository.AddUserAsync(user);

        // Assert
        Assert.False(result);
        mockSqlHelper.Verify(helper => helper.ExecuteNonQuery(SecurityProcedure.SignUp, It.IsAny<SqlParameter[]>()), Times.Once);
    }
}
