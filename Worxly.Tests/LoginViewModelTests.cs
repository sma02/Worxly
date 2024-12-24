using Xunit;
using Moq;
using Worxly.ViewModels;
using Worxly.Api;
using System.Threading.Tasks;

public class LoginViewModelTests
{
    private readonly LoginViewModel _viewModel;
    private readonly Mock<IUserApi> _userApiMock;

    public LoginViewModelTests()
    {
        _userApiMock = new Mock<IUserApi>();
        _viewModel = new LoginViewModel();
    }

    [Fact]
    public void Email_Property_Set_Get()
    {
        // Arrange
        string expectedEmail = "test@example.com";

        // Act
        _viewModel.Email = expectedEmail;

        // Assert
        Assert.Equal(expectedEmail, _viewModel.Email);
    }

    [Fact]
    public void Password_Property_Set_Get()
    {
        // Arrange
        string expectedPassword = "Password123";

        // Act
        _viewModel.Password = expectedPassword;

        // Assert
        Assert.Equal(expectedPassword, _viewModel.Password);
    }

    [Fact]
    public void RememberMe_Property_Set_Get()
    {
        // Arrange
        bool expected = true;

        // Act
        _viewModel.RememberMe = expected;

        // Assert
        Assert.Equal(expected, _viewModel.RememberMe);
    }

    [Fact]
    public async Task LoginCommand_Should_Call_Authenticate()
    {
        // Arrange
        string email = "test@example.com";
        string password = "Password123";
        _viewModel.Email = email;
        _viewModel.Password = password;

        _userApiMock.Setup(api => api.Authenticate(email, password))
            .ReturnsAsync(new ApiResponse<User>(new User(), System.Net.HttpStatusCode.OK, "OK"));

        // Act
        _viewModel.LoginCommand.Execute(null);

        // Assert
        _userApiMock.Verify(api => api.Authenticate(email, password), Times.Once);
    }
}
