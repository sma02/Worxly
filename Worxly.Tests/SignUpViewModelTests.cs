using Xunit;
using Moq;
using Worxly.Desktop.ViewModels;
using Worxly.Api;
using Worxly.DTOs;
using System.Threading.Tasks;

public class SignUpViewModelTests
{
    private readonly SignUpViewModel _viewModel;
    private readonly Mock<IUserApi> _userApiMock;

    public SignUpViewModelTests()
    {
        _userApiMock = new Mock<IUserApi>();
        _viewModel = new SignUpViewModel();
    }

    [Fact]
    public void FirstName_Property_Set_Get()
    {
        // Arrange
        string expectedFirstName = "John";

        // Act
        _viewModel.FirstName = expectedFirstName;

        // Assert
        Assert.Equal(expectedFirstName, _viewModel.FirstName);
    }

    [Fact]
    public void LastName_Property_Set_Get()
    {
        // Arrange
        string expectedLastName = "Doe";

        // Act
        _viewModel.LastName = expectedLastName;

        // Assert
        Assert.Equal(expectedLastName, _viewModel.LastName);
    }

    [Fact]
    public void Email_Property_Set_Get()
    {
        // Arrange
        string expectedEmail = "john.doe@example.com";

        // Act
        _viewModel.Email = expectedEmail;

        // Assert
        Assert.Equal(expectedEmail, _viewModel.Email);
    }

    [Fact]
    public async Task SignUpCommand_Should_Create_User()
    {
        // Arrange
        var newUser = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "johndoe",
            Email = "john.doe@example.com",
            Password = "Password123",
            UserTypeVal = "User"
        };

        _viewModel.FirstName = newUser.FirstName;
        _viewModel.LastName = newUser.LastName;
        _viewModel.Username = newUser.Username;
        _viewModel.Email = newUser.Email;
        _viewModel.Password = newUser.Password;
        _viewModel.IsCustomer = true;

        _userApiMock.Setup(api => api.PostUserAccount(newUser))
            .ReturnsAsync(newUser);

        // Act
        _viewModel.SignUpCommand.Execute(null);

        // Assert
        _userApiMock.Verify(api => api.PostUserAccount(It.IsAny<User>()), Times.Once);
    }
}
