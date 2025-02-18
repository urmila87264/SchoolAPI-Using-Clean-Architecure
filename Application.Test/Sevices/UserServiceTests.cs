using Appliction.Services;
using Domain.Authentication;
using Infrasture;
using Moq;

namespace Application.Test.Sevices
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
                _userRepositoryMock = new Mock<IUserRepository>();
            _userService=new UserService( _userRepositoryMock.Object );
        }

        [Fact]
        public async void SignUpAsync_ShouldReturnTrue_WhenUserIsAddedSuccessfully()
        {
            var testUser = new Registration
            {
                // Add necessary user properties for testing
                email = "u@gmail.com",
                Password = "123"
            };
            // Setup the mock to return true for AddUserAsync
            _userRepositoryMock.
                Setup(repo => repo.AddUserAsync(testUser))
                .ReturnsAsync(true);
            //Act
            var res = await _userService.SignUpAsync(testUser);
            // Assert
            Assert.True(res);
            _userRepositoryMock.Verify(repo=>repo.AddUserAsync(testUser),Times.Once());


        }
        [Fact]
        public async void SignUpAsync_ShouldReturnFalse_WhenUserIsNotAdded() {
            var testUser = new Registration
            {
                email = "u@gmail.com",
                Password = "123"
            };
           _userRepositoryMock.Setup(repo=>repo.AddUserAsync(testUser))
                .ReturnsAsync(false); 
            var res=await _userService.SignUpAsync(testUser);
            Assert.False(res);
            _userRepositoryMock.Verify(repo=>repo.AddUserAsync (testUser), Times.Once());   
            
        }
    }
}